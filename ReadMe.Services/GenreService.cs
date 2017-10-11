using ReadMe.Services.Contracts;
using System;
using System.Linq;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using ReadMe.Factories;

namespace ReadMe.Services
{
    public class GenreService : IGenreService
    {
        private readonly IEfRepository<Genre> genreRepository;
        private readonly IGenreFactory genreFactory;
        private readonly IUnitOfWork unitOfWork;

        public GenreService(IEfRepository<Genre> genreRepository, IGenreFactory genreFactory, IUnitOfWork unitOfWork)
        {
            if (genreRepository == null)
            {
                throw new ArgumentNullException("Genre repository cannot be null.");
            }

            if (genreFactory == null)
            {
                throw new ArgumentNullException("Genre factory cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.genreRepository = genreRepository;
            this.genreFactory = genreFactory;
            this.unitOfWork = unitOfWork;
        }

        public void AddGenre(string name)
        {
            var genre = this.genreFactory.CreateGenre(name);
            this.genreRepository.Add(genre);
            this.unitOfWork.Commit();
        }

        public IQueryable<Genre> GetAll()
        {
            var results = this.genreRepository.All;

            return results;
        }

        public IQueryable<Genre> GetAllAndDeleted()
        {
            var results = this.genreRepository.AllAndDeleted;

            return results;
        }

        public Genre GetById(Guid id)
        {
            return this.genreRepository.GetById(id);
        }

        public void UpdateGenre(Guid id, string name)
        {
            var genre = this.genreRepository.GetById(id);

            if(genre != null)
            {
                genre.Name = name;

                this.genreRepository.Update(genre);
                this.unitOfWork.Commit();
            }
        }
    }
}
