using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Publisher : IDeletable
    {
        public Publisher(string name, string owner, string phone, string city, string address, string country, string website)
        {
            this.Name = name;
            this.Owner = owner;
            this.PhoneNumber = phone;
            this.City = city;
            this.Address = address;
            this.Country = country;
            this.Website = website;
        }

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
