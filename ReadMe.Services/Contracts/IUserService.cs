using ReadMe.Models;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IUserService
    {
        IQueryable<User> GetAll();

        IQueryable<User> GetAllAndDeleted();

        IQueryable<User> GetUserByUsername(string username);

        IQueryable<Book> GetUserReadBooks(string userId);

        IQueryable<Book> GetUserWantToReadBooks(string userId);

        IQueryable<Book> GetUserCurrentlyReadingBooks(string userId);

        User GetUserById(string id);

        User EditUser(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote);

        void DeleteUser(string userId);

        void RestoreUser(string userId);
    }
}
