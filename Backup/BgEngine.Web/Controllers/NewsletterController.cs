using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BgEngine.Application.Services;
using BgEngine.Domain.EntityModel;
using BgEngine.Filters;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.ViewModels;
using BgEngine.Web.Mailers;
using BgEngine.Web.Helpers;
using System.Threading;
using StructureMap;
using System.Net.Mail;

namespace BgEngine.Controllers
{
    public class NewsletterController : BaseController
    {
        INewsletterServices NewsletterServices;
        IBlogServices BlogServices;
        IService<Post> PostServices;
        IUserMailer UserMailer;

        public NewsletterController(INewsletterServices newsletterservices, IBlogServices blogservices, IService<Post> postservices)
        {
            this.NewsletterServices = newsletterservices;
            this.BlogServices = blogservices;
            this.UserMailer = new UserMailer();
            this.PostServices = postservices;
        }

        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_CategoriesPerPage;
            ViewBag.TotalCategories = NewsletterServices.TotalNumberOfEntity();
            var pageIndex = page ?? 0;
            bool dir;
            if (sortdir == null)
            {
                dir = false;
            }
            else
            {
                dir = sortdir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? true : false;
            }
            if (sort == null)
            {
                return View(NewsletterServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, false));
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "name":
                        return View(NewsletterServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.Name, dir));
                    case "datecreated":
                        return View(NewsletterServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, dir));
                    case "haspendingtasks":
                        return View(NewsletterServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.HasPendingTasks, false));
                    default:
                        return View(NewsletterServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, false));
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            return View(NewsletterServices.FindAllEntities(n => n.NewsletterId == id, o => o.OrderByDescending(n => n.DateCreated), "NewsletterTasks").FirstOrDefault());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(string name, int[] posts)
        {
            if (String.IsNullOrEmpty(name))
            {
                return Json(new { result = "error", message= @Resources.AppMessages.RequiredFields });
            }
            if (posts == null)
            {
                return Json(new { result = "error", message = @Resources.AppMessages.NoPostsSelected });
            }
            List<Post> newsletterPosts = new List<Post>();
            foreach (int postid in posts)
            {
                Post post = PostServices.FindAllEntities(p => p.PostId == postid, null, "Image").First();                
                newsletterPosts.Add(post);
            }
            NewsletterServices.CreateNewsletter(name, UserMailer.GetNewsletterHtml(newsletterPosts, name).Body);
            return Json(new { result = "ok" });                        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(NewsletterServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Tag/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                NewsletterServices.SaveEntity(newsletter);
                return RedirectToAction("Index");
            }
            return View(newsletter);
        }

        //
        // GET: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(NewsletterServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsletterServices.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetPostsForNewsletter(int? page)
        {
            var pageIndex = page ?? 0;
            return PartialView("GetPostsForNewsletter", BlogServices.FindPagedPostsForNewsletter(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
        }

        public ActionResult GetNewsletter(int id)
        {
            return Content(NewsletterServices.FindAllEntities(n => n.NewsletterId == id,null,null).FirstOrDefault().Html);
        }

    }
}
