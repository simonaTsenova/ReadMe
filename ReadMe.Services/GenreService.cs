using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadMe.Models;
using ReadMe.Data.Contracts;

namespace ReadMe.Services
{
    public class GenreService : IGenreService
    {
        private readonly IEfRepository<Genre> genreRepository;
        private readonly IUnitOfWork unitOfWork;

        public GenreService(IEfRepository<Genre> genreRepository, IUnitOfWork unitOfWork)
        {
            if (genreRepository == null)
            {
                throw new ArgumentNullException("Genre repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.genreRepository = genreRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Genre> GetAll()
        {
            var results = this.genreRepository.All;

            return results;
        }
    }
}
