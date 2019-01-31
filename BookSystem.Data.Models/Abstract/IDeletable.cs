using System;

namespace BookSystem.Data.Models.Abstract
{
    public interface IDeletable
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
