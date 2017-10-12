using ReadMe.Models;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Profile;
using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;

namespace ReadMe.Web.Infrastructure.Factories
{
    public interface IViewModelFactory
    {
        UserDetailsViewModel CreateUserProfileViewModel(User user, bool isOwner);

        ProfileViewModel CreateProfileViewModel(UserDetailsViewModel userDetailsViewModel, ICollection<BookShortViewModel> wishlistViewModels,
            ICollection<BookShortViewModel> currentlyReadingViewModels, ICollection<BookShortViewModel> readViewModels, ICollection<ReviewViewModel> reviewViewModels);
    }
}
