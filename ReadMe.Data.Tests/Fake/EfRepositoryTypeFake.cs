using System;
using ReadMe.Models.Contracts;

namespace ReadMe.Data.Tests.Fake
{
    public class EfRepositoryTypeFake : IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
