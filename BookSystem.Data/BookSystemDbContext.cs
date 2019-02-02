using BookSystem.Data.Configurations;
using BookSystem.Data.Models;
using BookSystem.Data.Models.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookSystem.Data
{
    public class BookSystemDbContext : IdentityDbContext<User>
    {
        public BookSystemDbContext(DbContextOptions<BookSystemDbContext> options)
            : base(options)
        {

        }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UsersBooks> UsersBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UsersBooksConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());


            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            this.ApplyDeletionRules();
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

            foreach (var entry in entitiesForDeletion)
            {
                var entity = (IDeletable)entry.Entity;
                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
