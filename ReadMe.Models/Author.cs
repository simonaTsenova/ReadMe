using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Author : IDeletable
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string Biography { get; set; }

        public string Website { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}