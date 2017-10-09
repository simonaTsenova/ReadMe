using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class GenreViewModel : IMapFrom<Genre>
    {
        public GenreViewModel()
        {
        }

        public Guid Id { get; set; }

        [Required]
        [Remote("CheckNameExists", "Genres")]
        [StringLength(10, ErrorMessage = "Name should be between 3 and 10 symbols", MinimumLength = 3)]
        public string Name { get; set; }
    }
}