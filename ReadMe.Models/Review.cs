using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Review : IDeletable
    {
        public Review()
        {

        }

        public Review(string userId, Guid bookId, string content, DateTime postedOn)
        {
            this.UserId = userId;
            this.BookId = bookId;
            this.Content = content;
            this.PostedOn = postedOn;
        }

        public Guid Id { get; set; }

        public Guid? BookId { get; set; }

        public virtual Book Book { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public DateTime? PostedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
