using ReadMe.Models;
using ReadMe.Web.Models.Profile;
using System.Collections.Generic;

namespace ReadMe.Web.Infrastructure.Factories
{
    public interface IViewModelFactory
    {
        UserProfileViewModel CreateUserProfileViewModel(string email, string username, string fullname, string nationality,
            int age, string favouriteQuote, string photoUrl, ICollection<UserBook> userBooks, bool isOwner);
    }
}
