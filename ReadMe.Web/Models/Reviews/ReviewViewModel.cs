using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ReadMe.Web.Models.Reviews
{
    public class ReviewViewModel : IMapFrom<Review>, ICustomMapping
    {
        public Guid Id { get; set; }

        public Book Book { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public DateTime? PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}