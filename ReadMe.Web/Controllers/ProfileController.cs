using AutoMapper.QueryableExtensions;
using ReadMe.Authentication.Contracts;
using ReadMe.Models.Enumerations;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Profile;
using ReadMe.Web.Models.Reviews;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthenticationProvider authProvider;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService, IReviewService reviewService)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider cannot be null.");
            }

            if(userService == null)
            {
                throw new ArgumentNullException("User service cannot be null.");
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException("Review service cannot be null.");
            }

            this.authProvider = authProvider;
            this.userService = userService;
            this.reviewService = reviewService;
        }

        //
        // GET: Profile/Details/{username}
        public ActionResult Details(string username)
        {
            if(!this.authProvider.IsAuthenticated || username == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Details";
            var user = this.userService.GetUserByUsername(username);

            var userModel = user
                .ProjectTo<UserDetailsViewModel>()
                .FirstOrDefault();

            var currentlyReading = user
                .FirstOrDefault().UserBooks
                .Where(u => u.ReadStatus == ReadStatus.CurrentlyReading)
                .Select(x => x.Book)
                .AsQueryable()
                .ProjectTo<BookShortViewModel>()
                .ToList();

            var wishlist = user
                .FirstOrDefault().UserBooks
                .Where(u => u.ReadStatus == ReadStatus.WantToRead)
                .Select(x => x.Book)
                .AsQueryable()
                .ProjectTo<BookShortViewModel>()
                .ToList();

            var reviewsModel = this.reviewService
                .GetByUserId(userModel.Id)
                .ProjectTo<ReviewViewModel>()
                .ToList();

            var model = new ProfileViewModel()
            {
                UserDetailsViewModel = userModel,
                WishlistBooks = wishlist,
                CurrentlyReadingBooks = currentlyReading,
                ReviewsModels = reviewsModel
            };

            return View(model);
        }

        //
        // POST: Profile/Edit
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserDetailsViewModel model)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                this.userService.EditUser(model.Id, model.FirstName, model.LastName, model.Nationality, model.Age, model.FavouriteQuote);
            }

            return this.RedirectToAction("Details", new { username = model.UserName });
        }
    }
}