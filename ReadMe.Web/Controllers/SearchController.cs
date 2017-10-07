using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Search;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBookService bookService;

        public SearchController(IBookService bookService)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null.");
            }

            this.bookService = bookService;
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // POST: Search/Search/{pattern}
        [HttpPost]
        public PartialViewResult Search(SearchViewModel model)
        {
            var resultBooks = this.bookService
                .SearchByTitle(model.SearchPattern)
                .ProjectTo<BookViewModel>()
                .ToList();

            return this.PartialView("_BooksListPartial", resultBooks);
        }
    }
}