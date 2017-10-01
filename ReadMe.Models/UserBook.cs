using ReadMe.Models.Contracts;
using ReadMe.Models.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMe.Models
{
    public class UserBook : IDeletable
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public Guid? BookId { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }

        public ReadStatus ReadStatus { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
