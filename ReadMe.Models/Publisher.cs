using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Publisher : IDeletable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public string LogoUrl { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
