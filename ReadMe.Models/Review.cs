﻿using ReadMe.Models.Contracts;
using System;

namespace ReadMe.Models
{
    public class Review : IDeletable
    {
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
