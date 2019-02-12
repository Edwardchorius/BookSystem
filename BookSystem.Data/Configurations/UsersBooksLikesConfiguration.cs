using BookSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSystem.Data.Configurations
{
    internal class UsersBooksLikesConfiguration : IEntityTypeConfiguration<UsersBooksLikes>
    {
        public void Configure(EntityTypeBuilder<UsersBooksLikes> builder)
        {
            builder.HasKey(ub => new { ub.UserId, ub.BookId });

            builder.HasOne<User>(ub => ub.User)
                .WithMany(u => u.UsersBooksLikes)
                .HasForeignKey(ub => ub.UserId);

            builder.HasOne<Book>(ub => ub.Book)
                .WithMany(b => b.UsersBooksLikes)
                .HasForeignKey(ub => ub.BookId);
        }
    }
}
