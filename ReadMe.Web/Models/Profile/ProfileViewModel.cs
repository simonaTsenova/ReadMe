using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Profile
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }

        public ProfileViewModel(UserDetailsViewModel userDetailsViewModel,
            ICollection<BookShortViewModel> wishlistViewModels,
            ICollection<BookShortViewModel> currentlyReadingViewModels, 
            ICollection<BookShortViewModel> readViewModels, 
            ICollection<ReviewViewModel> reviewViewModels)
        {
            this.UserDetailsViewModel = userDetailsViewModel;
            this.WishlistBooks = wishlistViewModels;
            this.CurrentlyReadingBooks = currentlyReadingViewModels;
            this.ReadBooks = readViewModels;
            this.ReviewsModels = reviewViewModels;
        }

        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        
        public ICollection<BookShortViewModel> WishlistBooks { get; set; }

        public ICollection<BookShortViewModel> CurrentlyReadingBooks { get; set; }

        public ICollection<BookShortViewModel> ReadBooks { get; set; }

        public ICollection<ReviewViewModel> ReviewsModels { get; set; }
    }
}