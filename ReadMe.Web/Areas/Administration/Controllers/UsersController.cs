using PagedList;
using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private const int count = 15;
        private readonly IUserService userService;
        private readonly IAuthenticationProvider authProvider;

        public UsersController(IUserService userService, IAuthenticationProvider authProvider)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("User service cannot be null");
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException("Auth provider cannot be null");
            }

            this.userService = userService;
            this.authProvider = authProvider;
        }

        // GET: Administration/Users
        public PartialViewResult Index(int? page)
        {
            var users = this.userService.GetAllAndDeleted().OrderBy(u => u.UserName);
            var pageNumber = page ?? 1;

            var models = new List<UserViewModel>();
            foreach (var user in users)
            {
                var isAdmin = this.authProvider.IsInRole(user.Id, "Admin");
                var model = new UserViewModel(user, isAdmin);

                models.Add(model);
            }

            return this.PartialView("_UsersPartial", models.ToPagedList(pageNumber, count));
        }

        public ActionResult Delete(string userId, int page)
        {
            this.userService.DeleteUser(userId);

            return this.RedirectToAction("Index", "Users", new { page = page });
        }

        public ActionResult Restore(string userId, int page)
        {
            this.userService.RestoreUser(userId);

            return this.RedirectToAction("Index", "Users", new { page = page });
        }

        public ActionResult RemoveAdmin(string userId, int page)
        {
            this.authProvider.RemoveFromRole(userId, "Admin");

            return this.RedirectToAction("Index", "Users", new { page = page });
        }

        public ActionResult AddAdmin(string userId, int page)
        {
            this.authProvider.AddToRole(userId, "Admin");

            return this.RedirectToAction("Index", "Users", new { page = page });
        }
    }
}