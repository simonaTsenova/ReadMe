using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IGenreService
    {
        Genre GetById(Guid id);

        IQueryable<Genre> GetAll();

        IQueryable<Genre> GetAllAndDeleted();

        void AddGenre(string name);

        void UpdateGenre(Guid id, string name);
    }
}
