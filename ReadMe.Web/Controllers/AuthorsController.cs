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

        public AuthorsController(IAuthorService authorService)
        {
            if (authorService == null)
            {
                throw new ArgumentNullException("Author service cannot be null.");
            }

            this.authorService = authorService;
        }

        // GET: Authors/Details/{id}
        public ActionResult Details(Guid id)
        {
            var author = this.authorService.GetAuthorById(id);

            var authorModel = author
                .ProjectTo<AuthorViewModel>()
                .FirstOrDefault();

            return View(authorModel);
        }
    }
}