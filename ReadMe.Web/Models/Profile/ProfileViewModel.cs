using ReadMe.Web.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadMe.Web.Models.Profile
{
    public class ProfileViewModel
    {
        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        
        public ICollection<BookViewModel> WishlistBooks { get; set; }

        public ICollection<BookViewModel> CurrentlyReadingBooks { get; set; }
    }
}