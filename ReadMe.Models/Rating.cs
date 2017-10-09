using ReadMe.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Models
{
    public class Rating : IDeletable
    {
        public Rating()
        {
        }

        public Rating(Guid bookId, string userId, int stars, DateTime ratedOn)
        {
            this.BookId = bookId;
            this.UserId = userId;
            this.Stars = stars;
            this.RatedOn = ratedOn;
        }

        public Guid Id { get; set; }

        public Guid? BookId { get; set; }

        public virtual Book Book { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        public DateTime? RatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
