using ReadMe.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMe.Models
{
    public class Author : IPerson, IDeletable
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string Biography { get; set; }

        public string Website { get; set; }

        public string PhotoUrl { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}