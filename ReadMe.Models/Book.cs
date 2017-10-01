using ReadMe.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Models
{
    public class Book : IDeletable
    {
        public Book()
        {
            this.Genres = new HashSet<Genre>();
            this.UserBooks = new HashSet<UserBook>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime Published { get; set; }

        public string ISBN { get; set; }

        public Guid? AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public string Summary { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public string PhotoUrl { get; set; }

        public string Language { get; set; }

        public Guid? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
