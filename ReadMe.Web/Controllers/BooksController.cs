using AutoMapper.QueryableExtensions;
using ReadMe.Authentication.Contracts;
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
        private readonly IAuthenticationProvider authProvider;

        public BooksController(IBookService bookService, IReviewService reviewService, IAuthenticationProvider authProvider)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service canot be null.");
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service canot be null.");
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider canot be null.");
            }

            this.bookService = bookService;
            this.reviewService = reviewService;
            this.authProvider = authProvider;
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

            var currentUserId = this.authProvider.CurrentUserId;
            var currentBookId = book.FirstOrDefault().Id;
            ReviewViewModel formReviewModel = null;
            if (this.reviewService.GetByUserIdAndBookId(currentUserId, currentBookId) == null)
            {
                formReviewModel = new ReviewViewModel()
                {
                    UserId = this.authProvider.CurrentUserId,
                    BookId = book.FirstOrDefault().Id
                };
            }

            var model = new BookDetailsViewModel()
            {
                BookInfoViewModel = bookInfoModel,
                ReviewViewModels = reviewModels,
                FormReviewViewModel = formReviewModel
            };

            return View(model);
        }
    }
}