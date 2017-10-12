using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Reviews;
using System;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IUserService userService;

        public ReviewsController(IReviewService reviewService, IUserService userService)
        {
            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service canot be null.");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("User service canot be null.");
            }

            this.reviewService = reviewService;
            this.userService = userService;
        }

        // Post: Reviews/Post
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Post(ReviewViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return this.PartialView("_FormReviewPartial", model);
            }

            var review = this.reviewService.AddReview(model.UserId, model.BookId, model.Content);
            var user = this.userService.GetUserById(review.UserId);
            model.User = user;
            model.PostedOn = review.PostedOn;

            return this.PartialView("_ReviewPartial", model);
        }

        [Authorize]
        [HttpDelete]
        public ActionResult Index(Guid id)
        {
            var review = this.reviewService.GetById(id);
            var userName = review.User.UserName;

            this.reviewService.DeleteReview(id);

            return this.RedirectToAction("Details", "Profile", new { username = userName });
        }
    }
}