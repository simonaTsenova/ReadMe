using ReadMe.Authentication.Contracts;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using ReadMe.Authentication.Managers;

namespace ReadMe.Authentication
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private const int BanHours = 24 * 265;

        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IHttpContextProvider httpContextProvider;

        public AuthenticationProvider(IDateTimeProvider dateTimeProvider, IHttpContextProvider httpContextProvider)
        {
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("DateTimeProvider cannot be null.");
            }

            if (httpContextProvider == null)
            {
                throw new ArgumentNullException("HttpContextProvider cannot be null.");
            }

            this.dateTimeProvider = dateTimeProvider;
            this.httpContextProvider = httpContextProvider;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationUserManager>();
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationSignInManager>();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public string CurrentUserUsername
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserName();
            }
        }

        public IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser)
        {
            var result = this.UserManager.Create(user, password);

            if(result.Succeeded)
            {
                this.SignInManager.SignIn(user, isPersistent, rememberBrowser);
            }

            return result;
        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return this.SignInManager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public bool IsInRole(string userId, string roleName)
        {
            return userId != null && this.UserManager.IsInRole(userId, roleName);
        }

        public IdentityResult AddToRole(string userId, string roleName)
        {
            return this.UserManager.AddToRole(userId, roleName);
        }

        public IdentityResult RemoveFromRole(string userId, string roleName)
        {
            return this.UserManager.RemoveFromRole(userId, roleName);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void BanUser(string userId)
        {
            var user = this.UserManager.FindById(userId);
            user.LockoutEndDateUtc = this.dateTimeProvider.GetTimeFromCurrentTime(BanHours, 0, 0);

            this.UserManager.Update(user);
        }

        public void UnbanUser(string userId)
        {
            var user = this.UserManager.FindById(userId);
            user.LockoutEndDateUtc = null;

            this.UserManager.Update(user);
        }
    }
}
