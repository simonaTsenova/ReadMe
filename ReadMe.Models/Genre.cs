using ReadMe.Models.Contracts;
using System;
using System.Collections.Generic;

namespace ReadMe.Models
{
    public class Genre : IDeletable
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
        }

        public Genre(string name)
            : this()
        {
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}