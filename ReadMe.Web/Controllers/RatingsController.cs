using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService ratingService;
        private readonly IBookService bookService;
        private readonly IAuthenticationProvider authProvider;

        public RatingsController(IRatingService ratingService, 
            IBookService bookService, IAuthenticationProvider authProvider)
        {
            if (ratingService == null)
            {
                throw new ArgumentNullException("Rating service canot be null.");
            }

            if (bookService == null)
            {
                throw new ArgumentNullException("Book service canot be null.");
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider canot be null.");
            }

            this.ratingService = ratingService;
            this.bookService = bookService;
            this.authProvider = authProvider;
        }

        // POST: Ratings
        [Authorize]
        [HttpPost]
        public ActionResult Index(BookInfoViewModel model)
        {
            var currentUserId = this.authProvider.CurrentUserId;
            var rating = this.ratingService.GetByBookIdAndUserId(model.Id, currentUserId);

            if(rating == null)
            {
                this.ratingService.AddRating(model.Id, currentUserId, (int)model.Rating);
            }
            else
            {
                this.ratingService.UpdateRating(model.Id, currentUserId, (int)model.Rating);
            }

            var bookRatings = this.ratingService
                .GetAll()
                .Where(r => r.BookId == model.Id);

            var ratingsCount = bookRatings.Count();
            var ratingSum = bookRatings.Sum(b => b.Stars);
            var newRating = (double)ratingSum / (double)ratingsCount;

            this.bookService.UpdateRating(model.Id, newRating);

            return this.RedirectToAction("Details", "Books", new { id = model.Id });
        }
    }
}