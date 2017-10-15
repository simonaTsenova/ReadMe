using AutoMapper.QueryableExtensions;
using PagedList;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private const int count = 15;
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            if(genreService == null)
            {
                throw new ArgumentNullException("Genre service cannot be null");
            }

            this.genreService = genreService;
        }

        // GET: Administration/Genres
        [HttpGet]
        public PartialViewResult Index(int? page)
        {
            var genres = this.genreService.GetAllAndDeleted().OrderBy(g => g.Name);
            var pageNumber = page ?? 1;

            var models = genres.ProjectTo<GenreViewModel>().ToList();

            return this.PartialView("_GenresPartial", models.ToPagedList(pageNumber, count));
        }

        // GET: Administration/Genres/Add
        [HttpGet]
        public ActionResult Add()
        {
            return this.PartialView("_AddGenrePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Add(GenreViewModel model)
        {
            this.genreService.AddGenre(model.Name);

            return this.RedirectToAction("Index", "Genres");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GenreViewModel model)
        {
            this.genreService.UpdateGenre(model.Id, model.Name);

            return this.RedirectToAction("Index", "Genres");
        }

        [HttpGet]
        public JsonResult CheckNameExists(string name)
        {
            bool nameExists = (this.genreService.GetAllAndDeleted().Where(x => x.Name == name).Count() > 0) == true ? true : false;
            if (nameExists)
            {
                return Json("Genre with that name already exists", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}