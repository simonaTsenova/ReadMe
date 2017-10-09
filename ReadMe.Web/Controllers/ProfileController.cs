using AutoMapper;
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
        private readonly IMapper mapper;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService,
            IReviewService reviewService, IMapper mapper)
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

            if(mapper == null)
            {
                throw new ArgumentNullException("Mapper cannot be null");
            }

            this.authProvider = authProvider;
            this.userService = userService;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        //
        // GET: Profile/Details/{username}
        [Authorize]
        public ActionResult Details(string username)
        {
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

            var read = user
                .FirstOrDefault().UserBooks
                .Where(u => u.ReadStatus == ReadStatus.Read)
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
                ReadBooks = read,
                ReviewsModels = reviewsModel
            };

            return View(model);
        }

        //
        // POST: Profile/Edit
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public PartialViewResult Edit(UserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_EditUserPartial", model);
            }

            var updatedUser = this.userService.EditUser(model.Id, model.Email, model.FirstName, model.LastName, model.Nationality, model.Age, model.FavouriteQuote);
            var userInfoModel = this.mapper.Map<UserDetailsViewModel>(updatedUser);

            return this.PartialView("_UserInfoPartial", userInfoModel);
        }
    }
}