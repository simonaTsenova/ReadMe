using AutoMapper.QueryableExtensions;
using ReadMe.Authentication.Contracts;
using ReadMe.Models.Enumerations;
using ReadMe.Services.Contracts;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Profile;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthenticationProvider authProvider;
        private readonly IUserService userService;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider cannot be null.");
            }

            if(userService == null)
            {
                throw new ArgumentNullException("User service cannot be null.");
            }

            this.authProvider = authProvider;
            this.userService = userService;
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
                .ProjectTo<BookViewModel>()
                .ToList();

            var wishlist = user
                .FirstOrDefault().UserBooks
                .Where(u => u.ReadStatus == ReadStatus.WantToRead)
                .Select(x => x.Book)
                .AsQueryable()
                .ProjectTo<BookViewModel>()
                .ToList();

            var model = new ProfileViewModel()
            {
                UserDetailsViewModel = userModel,
                WishlistBooks = wishlist,
                CurrentlyReadingBooks = currentlyReading
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
            if(!this.Request.IsAjaxRequest())
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                this.userService.EditUser(model.Id, model.FirstName, model.LastName, model.Nationality, model.Age, model.FavouriteQuote);
            }

            return this.RedirectToAction("Details", "Profile", routeValues: new { username = model.UserName });
        }
    }
}