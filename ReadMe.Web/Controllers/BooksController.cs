using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service canot be null.");
            }

            this.bookService = bookService;
        }

        // GET: Books/Details/{id}
        public ActionResult Details(Guid id)
        {
            ViewBag.Title = "Book details";
            var book = this.bookService.GetBookById(id);

            if (!book.Any())
            {
                // TODO - return error page
                return RedirectToAction("Index", "Home");
            }

            var model = book.ProjectTo<BookDetailsViewBodel>()
                .FirstOrDefault();

            return View(model);
        }
    }
}