using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using System.Data.Entity;

namespace ReadMe.Services
{
    public class BookService : IBookService
    {
        private readonly IEfRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookService(IEfRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("Book repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Book> GetBookById(Guid id)
        {
            var book = this.bookRepository.All
                .Where(bk => bk.Id == id)
                .Include(x => x.Genres)
                .Include(x => x.UserBooks);

            return book;
        }

        public IQueryable<Book> Search(string pattern, string searchType)
        {
            switch (searchType)
            {
                case "title":
                    {
                        return this.SearchByTitle(pattern);
                    }
                case "author":
                    {
                        return this.SearchByAuthor(pattern);
                    }
                case "year":
                    {
                        return this.SearchByYear(pattern);
                    }
                default:
                    {
                        return null;
                    }
            }


        }

        private IQueryable<Book> SearchByTitle(string searchPattern)
        {
            var results = this.bookRepository.All
                .Where(book => book.Title.ToLower().Contains(searchPattern.ToLower()));

            return results;
        }

        private IQueryable<Book> SearchByAuthor(string searchPattern)
        {
            var results = this.bookRepository.All
                .Where(book =>
                    book.Author.FirstName.ToLower().Contains(searchPattern.ToLower()) ||
                    book.Author.LastName.ToLower().Contains(searchPattern.ToLower())
                );
                   

            return results;
        }

        private IQueryable<Book> SearchByYear(string searchPattern)
        {
            var results = this.bookRepository.All
                .Where(book => book.Published.ToString().ToLower().Contains(searchPattern.ToLower()));

            return results;
        }
    }
}
