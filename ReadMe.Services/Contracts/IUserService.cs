using ReadMe.Models;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IUserService
    {
        IQueryable<User> GetUserByUsername(string username);

        User GetUserById(string id);

        User EditUser(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote);
    }
}
