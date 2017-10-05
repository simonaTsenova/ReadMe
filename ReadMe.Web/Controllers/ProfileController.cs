using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Profile;
using System;
using System.Web.Mvc;

namespace ReadMe.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthenticationProvider authProvider;
        private readonly IUserService userService;
        private readonly IViewModelFactory factory;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService, IViewModelFactory factory)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider cannot be null.");
            }

            if(userService == null)
            {
                throw new ArgumentNullException("User service cannot be null.");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("Factory cannot be null.");
            }

            this.authProvider = authProvider;
            this.userService = userService;
            this.factory = factory;
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
            var currentUserId = this.authProvider.CurrentUserId;

            var isOwner = user.Id.Equals(currentUserId);
            
            var userModel = this.factory.CreateUserProfileViewModel(user, isOwner);

            return View(userModel);
        }

        //
        // POST: Profile/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                //this.userService.EditUser(model.Id, model.FirstName, model.LastName, model.Nationality, model.Age, model.FavouriteQuote);
            }

            return RedirectToAction("Details", "Profile", routeValues: new { model.UserName });
        }
    }
}