using ReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(User user, bool isAdmin)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.UserName = user.UserName;
            this.PhotoUrl = user.PhotoUrl;
            this.IsDeleted = user.IsDeleted;
            this.IsAdmin = isAdmin;
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdmin { get; set; }
    }
}