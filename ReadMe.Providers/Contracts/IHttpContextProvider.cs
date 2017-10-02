using Microsoft.Owin;
using System.Web;
using System.Security.Principal;
using System.Web.Caching;

namespace ReadMe.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }
        
        IOwinContext CurrentOwinContext { get; }

        IIdentity CurrentIdentity { get; }

        TManager GetUserManager<TManager>();

        Cache ContextCache { get; }
    }
}
