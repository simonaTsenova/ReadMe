using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Web.Models.Reviews
{
    public class ReviewViewModel : IMapFrom<Review>, ICustomMapping
    {
        public ReviewViewModel()
        {
        }

        //public ReviewViewModel(string userId, Guid bookId)
        //{
        //    this.UserId = userId;
        //    this.BookId = bookId;
        //}

        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}