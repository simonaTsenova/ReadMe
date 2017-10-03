using ReadMe.Models;

namespace ReadMe.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string email, string username, string firstName, string lastName);
    }
}
