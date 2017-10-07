using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IReviewService reviewService;

        public BooksController(IBookService bookService, IReviewService reviewService)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service canot be null.");
            }

            if (bookService == null)
            {
                throw new ArgumentNullException("Review service canot be null.");
            }

            this.bookService = bookService;
            this.reviewService = reviewService;
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

            var bookInfoModel = book.ProjectTo<BookInfoViewModel>()
                .FirstOrDefault();

            var reviewModels = this.reviewService
                .GetByBookId(bookInfoModel.Id)
                .ProjectTo<ReviewViewModel>()
                .ToList();

            var model = new BookDetailsViewModel()
            {
                BookInfoViewModel = bookInfoModel,
                ReviewViewModels = reviewModels
            };

            return View(model);
        }
    }
}