using BookSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.Data.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(book => book.Comments)
                .WithOne(comment => comment.Book)
                .HasForeignKey(book => book.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
