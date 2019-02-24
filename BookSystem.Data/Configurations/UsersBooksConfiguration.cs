using BookSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSystem.Data.Configurations
{
    internal class UsersBooksConfiguration : IEntityTypeConfiguration<UsersBooks>
    {
        public void Configure(EntityTypeBuilder<UsersBooks> builder)
        {
            builder.HasKey(ub => new { ub.UserId, ub.BookId });

            builder.HasOne<User>(ub => ub.User)
                .WithMany(u => u.UsersBooks)
                .HasForeignKey(ub => ub.UserId);
            
            builder.HasOne<Book>(ub => ub.Book)
                .WithMany(b => b.UsersBooks)
                .HasForeignKey(ub => ub.BookId);
        }
    }
}
