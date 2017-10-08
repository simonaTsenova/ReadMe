using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Reviews;
using System;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service canot be null.");
            }

            this.reviewService = reviewService;
        }

        // Post: Reviews/Post
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(ReviewViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return this.PartialView("_FormReviewPartial", model);
            }

            this.reviewService.AddReview(model.UserId, model.BookId, model.Content);
            
            return RedirectToAction("Details", "Books", routeValues: new { id = model.BookId });
        }

        [Authorize]
        [HttpDelete]
        public ActionResult Index(Guid id)
        {
            this.reviewService.DeleteReview(id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}