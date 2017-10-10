using ReadMe.Services.Contracts;
using System;
using System.Linq;
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

        public void AddAuthor(string firstName, string lastName, string nationality, int age, string biography, string website)
        {
            var author = this.authorFactory.CreateAuthor(firstName, lastName, nationality, age, biography, website);

            this.authorRepository.Add(author);
            this.unitOfWork.Commit();
        }

        public void DeleteAuthor(Guid id)
        {
            var author = this.authorRepository.GetById(id);
            var dateDeleted = DateTime.Now;

            if(author != null)
            {
                author.IsDeleted = true;
                author.DeletedOn = dateDeleted;

                this.authorRepository.Update(author);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<Author> GetAllAndDeleted()
        {
            return this.authorRepository.AllAndDeleted;
        }

        public IQueryable<Author> GetAuthorById(Guid authorId)
        {
            var author = this.authorRepository.All
                .Where(x => x.Id == authorId);

            return author;
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

        public void RestoreAuthor(Guid id)
        {
            var author = this.authorRepository.GetById(id);

            if (author != null)
            {
                author.IsDeleted = false;
                author.DeletedOn = null;

                this.authorRepository.Update(author);
                this.unitOfWork.Commit();
            }
        }

        public void UpdateAuthor(Guid id, string firstName, string lastName, string nationality, int age, string biography, string website, string photoUrl)
        {
            var author = this.authorRepository.GetById(id);

            if (author != null)
            {
                author.FirstName = firstName;
                author.LastName = lastName;
                author.Nationality = nationality;
                author.Age = age;
                author.Biography = biography;
                author.Website = website;
                author.PhotoUrl = photoUrl;

                this.authorRepository.Update(author);
                this.unitOfWork.Commit();
            }
        }
    }
}
