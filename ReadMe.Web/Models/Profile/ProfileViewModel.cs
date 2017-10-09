using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Profile
{
    public class ProfileViewModel
    {
        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        
        public ICollection<BookShortViewModel> WishlistBooks { get; set; }

        public ICollection<BookShortViewModel> CurrentlyReadingBooks { get; set; }

        public ICollection<BookShortViewModel> ReadBooks { get; set; }

        public ICollection<ReviewViewModel> ReviewsModels { get; set; }
    }
}