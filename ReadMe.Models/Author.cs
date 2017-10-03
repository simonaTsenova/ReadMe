using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Author : IDeletable
    {
        public Author(string firstName, string lastName, string nationality, int age, string biography, string website)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nationality = nationality;
            this.Age = age;
            this.Biography = biography;
            this.Website = website;
            this.PhotoUrl = "https://www.haikudeck.com/static/img/hd-avatar.png";
        }

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