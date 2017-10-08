using AutoMapper.QueryableExtensions;
using ReadMe.Authentication.Contracts;
using ReadMe.Models.Enumerations;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models;
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
        private readonly IUserBookService userBookService;

        public BooksController(IBookService bookService, IReviewService reviewService,
            IAuthenticationProvider authProvider, IUserBookService userBookService)
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

            if (userBookService == null)
            {
                throw new ArgumentNullException("UserBook service canot be null.");
            }

            this.bookService = bookService;
            this.reviewService = reviewService;
            this.authProvider = authProvider;
            this.userBookService = userBookService;
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
                    UserId = currentUserId,
                    BookId = currentBookId
                };
            }

            ReadStatus status = bookInfoModel.UserBooks
                .Where(x => x.BookId == currentBookId)
                .Select(x => x.ReadStatus)
                .FirstOrDefault();

            bookInfoModel.CurrentStatus = status;

            var model = new BookDetailsViewModel()
            {
                BookInfoViewModel = bookInfoModel,
                ReviewViewModels = reviewModels,
                FormReviewViewModel = formReviewModel,
            };

            return View(model);
        }

        // POST: Books/Status
        [Authorize]
        [HttpPost]
        public ActionResult Status(BookInfoViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = this.authProvider.CurrentUserId;

                this.userBookService.UpdateStatus(userId, model.Id, model.CurrentStatus);
            }

            return this.PartialView();
        }
    }
}