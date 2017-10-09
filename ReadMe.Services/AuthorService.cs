using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using ReadMe.Factories;

namespace ReadMe.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IEfRepository<Author> authorRepository;
        private readonly IAuthorFactory authorFactory;
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IEfRepository<Author> authorRepository, IAuthorFactory authorFactory, IUnitOfWork unitOfWork)
        {
            if (authorRepository == null)
            {
                throw new ArgumentNullException("Author repository cannot be null.");
            }

            if (authorFactory == null)
            {
                throw new ArgumentNullException("Author factory cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.authorRepository = authorRepository;
            this.authorFactory = authorFactory;
            this.unitOfWork = unitOfWork;
        }

        public Author GetAuthorByName(string firstName, string lastName)
        {
            var author = this.authorRepository
                .All
                .Where(x => x.FirstName.ToLower() == firstName.ToLower()
                    && x.LastName.ToLower() == lastName.ToLower())
                .FirstOrDefault();

            return author;
        }
    }
}
