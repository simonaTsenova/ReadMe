using ReadMe.Models;
using ReadMe.Models.Enumerations;
using ReadMe.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadMe.Web.Models
{
    public class UserBookViewModel : IMapFrom<UserBook>
    {
        public string UserId { get; set; }

        public Guid BookId { get; set; }

        public ReadStatus readStatus { get; set; }
    }
}