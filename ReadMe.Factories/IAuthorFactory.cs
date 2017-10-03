using ReadMe.Models;

namespace ReadMe.Factories
{
    public interface IAuthorFactory
    {
        Author CreateAuthor(string firstName, string lastName, string nationality, int age, string biography, string website);
    }
}
