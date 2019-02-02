using BookSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.Data.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasMany(review => review.Comments)
                .WithOne(comment => comment.Review)
                .HasForeignKey(review => review.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
