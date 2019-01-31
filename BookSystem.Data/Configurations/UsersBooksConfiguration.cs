using BookSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSystem.Data.Configurations
{
    internal class UsersBooksConfiguration : IEntityTypeConfiguration<UsersBooks>
    {
        public void Configure(EntityTypeBuilder<UsersBooks> builder)
        {
            builder.HasKey(ub => new { ub.BookId, ub.UserId });
        }
    }
}
