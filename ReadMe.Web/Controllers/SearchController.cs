using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Infrastructure.Factories;
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
        private readonly IGenreService genreService;
        private readonly IViewModelFactory factory;

        public SearchController(IBookService bookService, IGenreService genreService, IViewModelFactory factory)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null.");
            }

            if(genreService == null)
            {
                throw new ArgumentNullException("Genre service cannot be null.");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("Factory cannot be null.");
            }

            this.bookService = bookService;
            this.genreService = genreService;
            this.factory = factory;
        }

        // GET: Search
        public ActionResult Index()
        {
            var genres = this.genreService
                .GetAll()
                .ToList();

            var model = this.factory.CreateSearchViewModel(genres);

            return View(model);
        }

        // POST: Search/Search/{pattern}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Search(SearchViewModel model, string[] genres)
        {
            var resultBooks = this.bookService
                .Search(model.SearchPattern, model.SearchType, genres)
                .ProjectTo<BookShortViewModel>()
                .ToList();

            return this.PartialView("_BooksListPartial", resultBooks);
        }
    }
}