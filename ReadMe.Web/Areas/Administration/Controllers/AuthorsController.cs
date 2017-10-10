using AutoMapper.QueryableExtensions;
using PagedList;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        private const int count = 15;
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;

        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            if (authorService == null)
            {
                throw new ArgumentNullException("Author service cannot be null");
            }

            if (bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null");
            }

            this.authorService = authorService;
            this.bookService = bookService;
        }

        // GET: Administration/Authors
        public PartialViewResult Index(int? page)
        {
            var authors = this.authorService.GetAllAndDeleted()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);
            var pageNumber = page ?? 1;

            var models = authors.ProjectTo<AuthorViewModel>();

            return this.PartialView("_AuthorsPartial", models.ToPagedList(pageNumber, count));
        }

        // GET: Administration/Authors/Add
        [HttpGet]
        public PartialViewResult Add()
        {
            return this.PartialView("_AddAuthorPartial");
        }

        // POST: Administration/Authors/Add
        [HttpPost]
        public ActionResult Add(AddAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_AddAuthorPartial", model);
            }

            var authorNames = model.FullName.Split(' ');
            this.authorService.AddAuthor(authorNames[0], authorNames[1], model.Nationality, model.Age,
                model.Biography, model.Website);

            return this.RedirectToAction("Index", "Authors");
        }


        // GET: Administration/Authors/Edit
        [HttpGet]
        public PartialViewResult Edit(Guid authorId)
        {
            var author = this.authorService.GetAuthorById(authorId);
            if (author.Count() == 0)
            {
                // TODO
                return this.PartialView("Error");
            }

            var model = author.ProjectTo<AuthorViewModel>().FirstOrDefault();

            return this.PartialView("_EditAuthorPartial", model);
        }

        // POST: Administration/Books/Edit
        [HttpPost]
        public ActionResult Edit(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_EditAuthorPartial", model);
            }

            var authorNames = model.FullName.Split(' ');
            this.authorService.UpdateAuthor(model.Id, authorNames[0], authorNames[1], model.Nationality,
                model.Age, model.Biography, model.Website, model.PhotoUrl);

            return this.RedirectToAction("Index", "Authors");
        }

        [HttpPost]
        public ActionResult Delete(Guid authorId, int page)
        {
            var authorBooks = this.bookService.GetBooksByAuthor(authorId).ToList();
            foreach (var book in authorBooks)
            {
                this.bookService.DeleteBook(book.Id);
            }

            this.authorService.DeleteAuthor(authorId);

            return this.RedirectToAction("Index", "Authors", new { page = page });
        }

        [HttpPost]
        public ActionResult Restore(Guid authorId, int page)
        {
            var authorBooks = this.bookService.GetAllBooksByAuthor(authorId).ToList();
            foreach (var book in authorBooks)
            {
                this.bookService.RestoreBook(book.Id);
            }

            this.authorService.RestoreAuthor(authorId);

            return this.RedirectToAction("Index", "Authors", new { page = page });
        }

        [HttpGet]
        public JsonResult CheckAuthorExists(string fullname)
        {
            var names = fullname.Split(' ');
            var firstName = names[0];
            var lastName = names[1];

            bool authorExists = (this.authorService.GetAuthorByName(firstName, lastName) != null) ? true : false;
            if (authorExists)
            {
                return Json("Author with that name already exists", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}