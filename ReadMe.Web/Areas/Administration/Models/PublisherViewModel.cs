using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class PublisherViewModel : IMapFrom<Publisher>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Name must be between 4 and 40 characters long", MinimumLength = 4)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Owner name must be between 4 and 40 characters long", MinimumLength = 4)]
        public string Owner { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        [StringLength(40, ErrorMessage = "Owner name must be maximum 40 characters long")]
        public string Address { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public string LogoUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}