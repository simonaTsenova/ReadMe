using ReadMe.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMe.Models
{
    public class Genre : IDeletable
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}