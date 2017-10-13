using AutoMapper.QueryableExtensions;
using ReadMe.Authentication.Contracts;
using ReadMe.Models.Enumerations;
using ReadMe.Services.Contracts;
using ReadMe.Web.Infrastructure.Factories;
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
        private readonly IUserBookService userBookService;
        private readonly IViewModelFactory factory;
        private readonly IAuthenticationProvider authProvider;

        public BooksController(IBookService bookService, IReviewService reviewService,
            IUserBookService userBookService, IViewModelFactory factory, IAuthenticationProvider authProvider)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service canot be null.");
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service canot be null.");
            }

            if (userBookService == null)
            {
                throw new ArgumentNullException("UserBook service canot be null.");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("Factory canot be null.");
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider canot be null.");
            }

            this.bookService = bookService;
            this.reviewService = reviewService;
            this.userBookService = userBookService;
            this.factory = factory;
            this.authProvider = authProvider;
        }

        // GET: Books/Details/{id}
        public ActionResult Details(Guid id)
        {
            var book = this.bookService.GetBookById(id);

            if (!book.Any())
            {
                return this.View("Error");
            }

            ViewBag.Title = "Book details";
            var bookInfoModel = book
                .ProjectTo<BookInfoViewModel>()
                .FirstOrDefault();

            var reviewModels = this.reviewService
                .GetByBookId(bookInfoModel.Id)
                .ProjectTo<ReviewViewModel>()
                .ToList();

            var currentUserId = this.authProvider.CurrentUserId;
            var currentBookId = book.FirstOrDefault().Id;
            ReviewViewModel formReviewModel = null;
            var review = this.reviewService.GetByUserIdAndBookId(currentUserId, currentBookId);
            if (review == null)
            {
                formReviewModel = this.factory.CreateReviewViewModel();
                formReviewModel.UserId = currentUserId;
                formReviewModel.BookId = currentBookId;
            }

            ReadStatus status = bookInfoModel.UserBooks
                .Where(x => x.BookId == currentBookId)
                .Select(x => x.ReadStatus)
                .FirstOrDefault();

            bookInfoModel.CurrentStatus = status;

            var model = this.factory.CreateBookDetailsViewModel(
                bookInfoModel,
                reviewModels,
                formReviewModel
            );

            return View(model);
        }

        // POST: Books/Status
        [Authorize]
        [HttpPost]
        public ContentResult Status(BookInfoViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = this.authProvider.CurrentUserId;

                this.userBookService.UpdateStatus(userId, model.Id, model.CurrentStatus);

                return this.Content("Status updated");
            }

            return this.Content("Oops! Status could not be updated");
        }
    }
}