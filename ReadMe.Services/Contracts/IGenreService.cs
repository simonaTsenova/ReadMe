using ReadMe.Models;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IGenreService
    {
        IQueryable<Genre> GetAll();
    }
}
