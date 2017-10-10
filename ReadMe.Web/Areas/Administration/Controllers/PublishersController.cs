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
    public class PublishersController : Controller
    {
        private const int count = 15;
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            if (publisherService == null)
            {
                throw new ArgumentNullException("Publisher service cannot be null");
            }

            this.publisherService = publisherService;
        }

        // GET: Administration/Publishers
        public PartialViewResult Index(int? page)
        {
            var publishers = this.publisherService
                .GetAllAndDeleted()
                .OrderBy(x => x.Name);
            var pageNumber = page ?? 1;

            var models = publishers.ProjectTo<PublisherViewModel>();

            return this.PartialView("_PublishersPartial", models.ToPagedList(pageNumber, count));
        }

        // GET: Administration/Publishers/Add
        [HttpGet]
        public PartialViewResult Add()
        {
            return this.PartialView("_AddPublisherPartial");
        }

        // POST: Administration/Publishers/Add
        [HttpPost]
        public ActionResult Add(AddPublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_AddPublisherPartial", model);
            }

            this.publisherService.AddPublisher(model.Name, model.Owner, model.PhoneNumber, model.City,
                model.Address, model.Country, model.Website);

            return this.RedirectToAction("Index", "Publishers");
        }

        // GET: Administration/Publishers/Edit
        [HttpGet]
        public PartialViewResult Edit(Guid publisherId)
        {
            var publisher = this.publisherService.GetPublisherById(publisherId);
            if (publisher.Count() == 0)
            {
                // TODO
                return this.PartialView("Error");
            }

            var model = publisher.ProjectTo<PublisherViewModel>().FirstOrDefault();

            return this.PartialView("_EditPublisherPartial", model);
        }

        // POST: Administration/Publishers/Edit
        [HttpPost]
        public ActionResult Edit(PublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_EditPublisherPartial", model);
            }

            this.publisherService.UpdatePublisher(model.Id, model.Name, model.Owner, model.PhoneNumber, model.City,
                model.Address, model.Country, model.Website, model.LogoUrl);

            return this.RedirectToAction("Index", "Publishers");
        }

        [HttpPost]
        public ActionResult Delete(Guid publisherId, int page)
        {
            //var authorBooks = this.bookService.GetBooksByAuthor(authorId);
            //foreach (var book in authorBooks)
            //{
            //    // TODO
            //    //this.bookService.DeleteBook(book.Id);
            //}

            this.publisherService.DeletePublisher(publisherId);

            return this.RedirectToAction("Index", "Publishers", new { page = page });
        }

        [HttpPost]
        public ActionResult Restore(Guid publisherId, int page)
        {
            //var authorBooks = this.bookService.GetBooksByAuthor(authorId);
            //foreach (var book in authorBooks)
            //{
            //    //this.bookService.DeleteBook(book.Id);
            //}

            this.publisherService.RestorePublisher(publisherId);

            return this.RedirectToAction("Index", "Publishers", new { page = page });
        }

        [HttpGet]
        public JsonResult CheckPublisherExists(string name)
        {
            bool publisherExists = (this.publisherService.GetPublisherByName(name) != null) ? true : false;
            if (publisherExists)
            {
                return Json("Publisher with that name already exists", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}