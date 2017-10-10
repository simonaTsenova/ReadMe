using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class AddPublisherViewModel : PublisherViewModel, IMapFrom<Publisher>
    {
        [Required]
        [StringLength(40, ErrorMessage = "Name must be between 4 and 40 characters long", MinimumLength = 4)]
        [Remote("CheckPublisherExists", "Publishers")]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}