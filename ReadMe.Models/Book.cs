using ReadMe.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMe.Models
{
    public class Book : IDeletable
    {
        public Book()
        {

        }

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? Published { get; set; }

        [Required]
        public string ISBN { get; set; }

        public Guid? AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public Guid? GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public string Summary { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        public string PhotoUrl { get; set; }

        public string Language { get; set; }

        public Guid? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public ICollection<Genre> Genres { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
