using ReadMe.Models;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
    }
}
