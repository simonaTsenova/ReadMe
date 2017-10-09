using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MainController : Controller
    {
        // GET: Administration/Main
        public ActionResult Index()
        {
            return View();
        }
    }
}