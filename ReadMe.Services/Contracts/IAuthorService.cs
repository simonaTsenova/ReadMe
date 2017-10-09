using ReadMe.Models;

namespace ReadMe.Services.Contracts
{
    public interface IAuthorService
    {
        Author GetAuthorByName(string firstName, string lastName);
    }
}
