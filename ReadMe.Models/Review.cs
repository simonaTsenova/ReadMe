using System;

namespace ReadMe.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid? BookId { get; set; }

        public virtual Book Book { get; set; }

        public Guid? UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public DateTime? PostedOn { get; set; }
    }
}
