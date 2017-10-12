using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Authors;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;

        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            if (authorService == null)
            {
                throw new ArgumentNullException("Author service cannot be null.");
            }

            if (bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null.");
            }

            this.authorService = authorService;
            this.bookService = bookService;
        }

        // GET: Authors/Details/{id}
        public ActionResult Details(Guid id)
        {
            var author = this.authorService.GetAuthorById(id);

            if (author.Count() == 0)
            {
                return this.View("Error");
            }

            var authorModel = author
                .ProjectTo<AuthorViewModel>()
                .FirstOrDefault();

            return View(authorModel);
        }
    }
}