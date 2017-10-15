using AutoMapper.QueryableExtensions;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;
        private readonly IReviewService reviewService;

        public HomeController(IBookService bookService, IReviewService reviewService)
        {
            if(bookService == null)
            {
                throw new ArgumentNullException("Book service cannot be null.");
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service cannot be null.");
            }

            this.bookService = bookService;
            this.reviewService = reviewService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 60 * 4)]
        [ChildActionOnly]
        public ActionResult TopBooks()
        {
            var topRatedBooks = this.bookService
                .GetTopRatedBooks()
                .ProjectTo<BookShortViewModel>()
                .ToList();

            return this.PartialView("_TopBooksListPartial", topRatedBooks);
        }

        [OutputCache(Duration = 60 * 4)]
        [ChildActionOnly]
        public ActionResult LatestBooks()
        {
            var latestPublishedBooks = this.bookService
                .GetLatestBooks()
                .ProjectTo<BookShortViewModel>()
                .ToList();

            return this.PartialView("_LatestBooksListPartial", latestPublishedBooks);
        }

        [OutputCache(Duration = 60 * 4)]
        [ChildActionOnly]
        public ActionResult LatestReviews()
        {
            var latestReviews = this.reviewService
                .GetLatestReviews()
                .ProjectTo<ReviewViewModel>()
                .ToList();

            return this.PartialView("_ReviewsPartial", latestReviews);
        }
    }
}