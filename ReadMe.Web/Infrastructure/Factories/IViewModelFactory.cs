using ReadMe.Models;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Models;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Profile;
using ReadMe.Web.Models.Reviews;
using ReadMe.Web.Models.Search;
using System.Collections.Generic;

namespace ReadMe.Web.Infrastructure.Factories
{
    public interface IViewModelFactory
    {
        UserDetailsViewModel CreateUserProfileViewModel(User user, bool isOwner);

        ProfileViewModel CreateProfileViewModel(UserDetailsViewModel userDetailsViewModel, ICollection<BookShortViewModel> wishlistViewModels,
            ICollection<BookShortViewModel> currentlyReadingViewModels, ICollection<BookShortViewModel> readViewModels, ICollection<ReviewViewModel> reviewViewModels);

        SearchViewModel CreateSearchViewModel(ICollection<Genre> genres);

        ReviewViewModel CreateReviewViewModel();

        BookDetailsViewModel CreateBookDetailsViewModel(BookInfoViewModel bookInfoViewModel, ICollection<ReviewViewModel> reviewViewModels,
            ReviewViewModel formReviewViewModel);

        UserViewModel CreateUserViewModel(User user, bool isAdmin);
    }
}
