using AutoMapper.QueryableExtensions;
using PagedList;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private const int count = 15;
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;

        public BooksController(IBookService bookService, IGenreService genreService,
            IAuthorService authorService, IPublisherService publisherService)
        {
            if (bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null");
            }

            if (genreService == null)
            {
                throw new ArgumentNullException("Genre service cannot be null");
            }

            if (authorService == null)
            {
                throw new ArgumentNullException("Author service cannot be null");
            }

            if (publisherService == null)
            {
                throw new ArgumentNullException("Publisher service cannot be null");
            }

            this.bookService = bookService;
            this.genreService = genreService;
            this.authorService = authorService;
            this.publisherService = publisherService;
        }

        // GET: Administration/Books
        public PartialViewResult Index(int? page)
        {
            var books = this.bookService.GetAllAndDeleted().OrderBy(b => b.Published);
            var pageNumber = page ?? 1;

            var models = books.ProjectTo<BookViewModel>();

            return this.PartialView("_BooksPartial", models.ToPagedList(pageNumber, count));
        }

        // GET: Administration/Books/Add
        [HttpGet]
        public PartialViewResult Add()
        {
            var genres = this.genreService.GetAll().ToList();

            var model = new BookViewModel()
            {
                Genres = genres
            };

            return this.PartialView("_AddBookPartial", model);
        }

        // POST: Administration/Books/Add
        [HttpPost]
        public ActionResult Add(BookViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return this.PartialView("_AddBookPartial", model);
            }

            var bookGenres = new List<Genre>();
            foreach (var i in model.GenresIds)
            {
                var genre = this.genreService.GetById(i);
                bookGenres.Add(genre);
            }

            var authorNames = model.Author.Split(' ');
            var author = this.authorService.GetAuthorByName(authorNames[0], authorNames[1]);
            var publisher = this.publisherService.GetPublisherByName(model.Publisher);

            this.bookService.AddBook(model.Title, model.Published, model.ISBN, author,
                model.Summary, model.Language, publisher, bookGenres);

            return this.RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public JsonResult CheckTitleExists(string title)
        {
            bool titleExists = (this.bookService.GetAllAndDeleted().Where(x => x.Title == title).Count() > 0) == true ? true : false;
            if (titleExists)
            {
                return Json("Book with that title already exists", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckAuthorExists(string author)
        {
            var names = author.Split(' ');
            var firstName = names[0];
            var lastName = names[1];

            bool authorExists = (this.authorService.GetAuthorByName(firstName, lastName) != null) ? true : false;
            if (!authorExists)
            {
                return Json("Author with that name does not exist", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckPublisherExists(string publisher)
        {
            bool publisherExists = (this.publisherService.GetPublisherByName(publisher) != null) ? true : false;
            if (!publisherExists)
            {
                return Json("Publisher with that name does not exist", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}