using ReadMe.Models;
using ReadMe.Web.Models.Profile;

namespace ReadMe.Web.Infrastructure.Factories
{
    public interface IViewModelFactory
    {
        UserDetailsViewModel CreateUserProfileViewModel(User user, bool isOwner);
    }
}
