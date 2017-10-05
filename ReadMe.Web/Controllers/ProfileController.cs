using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Infrastructure;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Profile/Details/{username}
        public ActionResult Details(string username)
        {
            if(!this.authProvider.IsAuthenticated || username == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = this.userService.GetUserByUsername(username);
            var currentUserId = this.authProvider.CurrentUserId;

            var isOwner = user.Id.Equals(currentUserId);

            var userModel = this.factory.CreateUserProfileViewModel(user.Email, user.UserName, user.FirstName + " " + user.LastName,
                user.Nationality, user.Age, user.FavouriteQuote, user.PhotoUrl, user.UserBooks, isOwner);

            return View(userModel);
        }
    }
}