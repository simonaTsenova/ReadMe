using ReadMe.Web.Models.Books;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Profile
{
    public class ProfileViewModel
    {
        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        
        public ICollection<BookShortViewModel> WishlistBooks { get; set; }

        public ICollection<BookShortViewModel> CurrentlyReadingBooks { get; set; }
    }
}