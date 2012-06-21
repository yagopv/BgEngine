//==============================================================================
// This file is part of BgEngine.
//
// BgEngine is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BgEngine is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BgEngine. If not, see <http://www.gnu.org/licenses/>.
//==============================================================================
// Copyright (c) 2011 Yago Pérez Vázquez
// Version: 1.0
//==============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.ServiceModel.Syndication;

using BgEngine.Domain.EntityModel;
using BgEngine.Security.Services;
using BgEngine.Domain.Filters;
using BgEngine.Application;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Web.Results;
using BgEngine.Extensions;


namespace BgEngine.Controllers
{
    public class PostController : BaseController
    {
        IBlogServices BlogServices;
        IService<Post> PostServices;
        IService<Category> CategoryServices;
        IService<Image> ImageServices;
        IService<User> UserServices;
        IService<Tag> TagServices;

        public PostController(IBlogServices blogservices, 
                              IService<Post> postservices, 
                              IService<Category> categoryservices, 
                              IService<Image> imageservices,
                              IService<User> userservices,
                              IService<Tag> tagservices)
        {
            this.BlogServices = blogservices;
            this.PostServices = postservices;
            this.CategoryServices = categoryservices;
            this.ImageServices = imageservices;
            this.UserServices = userservices;
            this.TagServices = tagservices;
        }

        //
        // GET: /Admin/

        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Index(int? page, string sort, string sortdir,string searchstring)
        {
           ViewBag.RowsPerPage = BgResources.Pager_PostPerPage;           
           if (!String.IsNullOrEmpty(searchstring))
            {
                Session["searchstring"] = searchstring;
            }
            else
            {
                Session["searchstring"] = String.Empty;
            }
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
                 Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.DateCreated, dir, Session["searchstring"].ToString());
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "title":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.Title, dir, Session["searchstring"].ToString());
                        break;
                    case "datecreated":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.DateCreated, dir, Session["searchstring"].ToString());
                        break;
                    case "dateupdated":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.DateUpdated, dir, Session["searchstring"].ToString());
                        break;
                    case "ispublic":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.IsPublic, dir, Session["searchstring"].ToString());
                        break;
                    case "ishomepost":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.IsHomePost, dir, Session["searchstring"].ToString());
                        break;
                    case "isaboutme":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.IsAboutMe, dir, Session["searchstring"].ToString());
                        break;
                    case "allowanonymouscomments":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.AllowAnonymousComments, dir, Session["searchstring"].ToString());
                        break;
                    case "ispostcommentsclosed":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.IsPostCommentsClosed, dir, Session["searchstring"].ToString());
                        break;
                    case "category.name":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.Category.Name, dir, Session["searchstring"].ToString());
                        break;
                    case "user.username":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.User.Username, dir, Session["searchstring"].ToString());
                        break;
                    case "visits":
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.Visits, dir, Session["searchstring"].ToString());
                        break;
                    default:
                        Posts = BlogServices.SearchForPagedPostsByParam(pageIndex, Int32.Parse(BgResources.Pager_PostPerPage), p => p.DateCreated, dir, Session["searchstring"].ToString());
                        break;
                }
            }
            // Get Paged number of Posts after do the search with the parameter (searchstring)
            ViewBag.TotalPosts = BlogServices.GetPagedNumber();
            return View(Posts);
        }

        //
        // GET: /Admin/Details/5

        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Details(int id)
        {
            return View(PostServices.FindEntityByIdentity(id));
        }

        //
        // GET: /Admin/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ImageId = new SelectList(ImageServices.FindAllEntities(null,null,null), "ImageId", "FileName");
            ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null,null,null), "CategoryId", "Name");
            ViewBag.UserId = new SelectList(UserServices.FindAllEntities(null,null,null), "UserId", "Username", CodeFirstSecurity.CurrentUserId);
            ViewBag.Tags = TagServices.FindAllEntities(null, o => o.OrderBy(t => t.TagName), null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Post post, int[] selectedtags)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BlogServices.CreatePost(post, selectedtags);
                    return RedirectToAction("Index");
                }
                catch (ApplicationValidationErrorsException ex)
                {
                    foreach (string str in ex.ValidationErrors)
                    {
                        ModelState.AddModelError("", str);
                    }                    
                }
            }
            ViewBag.ImageId = new SelectList(ImageServices.FindAllEntities(null, null, null), "ImageId", "FileName", post.ImageId);
            ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null, null, null), "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(UserServices.FindAllEntities(null, null, null), "UserId", "Username", post.UserId);
            ViewBag.Tags = TagServices.FindAllEntities(null, o => o.OrderBy(t => t.TagName), null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
            return View(post);
        }

        //
        // GET: /Admin/Edit/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Edit(int id)
        {
            Post post = PostServices.FindAllEntities(p => p.PostId == id, null, "Tags").FirstOrDefault();
            ViewBag.ImageId = new SelectList(ImageServices.FindAllEntities(null, null, null), "ImageId", "FileName", post.ImageId);
            ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null, null, null), "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(UserServices.FindAllEntities(null, null, null), "UserId", "Username", post.UserId);
            ViewBag.Tags = TagServices.FindAllEntities(null, o => o.OrderBy(t => t.TagName), null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
            return View(post);
        }

        //
        // POST: /Admin/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Post post, int[] selectedtags)
        {
            Post posttoupdate = PostServices.FindAllEntities(p => p.PostId == post.PostId, null, "Tags").FirstOrDefault();
            if (TryUpdateModel(posttoupdate, "", null, new string[] { }))
            {
                try
                {
                    UpdateModel(posttoupdate, "", null, new string[] { });
                    BlogServices.UpdatePost(posttoupdate, selectedtags);
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", Resources.AppMessages.Error_Saving_Changes);
                }
                catch (ApplicationValidationErrorsException ex)
                {
                    foreach (string str in ex.ValidationErrors)
                    {
                        ModelState.AddModelError("", str);
                    }
                }
            }
            ViewBag.ImageId = new SelectList(ImageServices.FindAllEntities(null, null, null), "ImageId", "FileName", post.ImageId);
            ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null, null, null), "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(UserServices.FindAllEntities(null, null, null), "UserId", "Username", post.UserId);
            ViewBag.Tags = TagServices.FindAllEntities(null, o => o.OrderBy(t => t.TagName), null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
            return View(posttoupdate);
        }

        //
        // GET: /Admin/Delete/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Delete(int id)
        {
            Post post = PostServices.FindEntityByIdentity(id);
            return View(post);
        }

        //
        // POST: /Admin/Delete/5

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id)
        {
            PostServices.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Admin()
        {            
            return View();
        }

        //
        // GET: /Post/GetPostByCode/id
        [EnableCompression]
        public ActionResult GetPostByCode(string id)
        {
            Post post = BlogServices.FindPost(id);
            if (post == null)
            {
                return new NotFoundMvc.NotFoundViewResult();
            }
            ViewBag.MetaDescription = post.Description;
            if ((post.IsPublic) || (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.IsAuthenticated ? CodeFirstSecurity.CurrentUserName : " ", BgResources.Security_PremiumRole)))
            {
                return View("Post", post);
            }
            else
            {
                TempData["returnUrl"] = Request.Url.ToString();
                return RedirectToRoute("Default", new { controller = "Account", action = "LogOn"});
            }
        }

        //
        // GET: /Post/GetPostById/id
        [EnableCompression]
        public ActionResult GetPostById(int id)
        {
            Post post = BlogServices.FindPost(id);
            if (post == null)
            {
                return new NotFoundMvc.NotFoundViewResult();
            }
            ViewBag.MetaDescription = post.Description;
            if ((post.IsPublic) || (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.IsAuthenticated ? CodeFirstSecurity.CurrentUserName : " ", BgResources.Security_PremiumRole)))
            {
                return View("Post", post);
            }
            else
            {
                TempData["returnUrl"] = Request.Url.ToString();
                return RedirectToRoute("Default", new { controller = "Account", action = "LogOn"});
            }
        }

        //
        // GET: /Post/GetPostsByCategory/category
        [EnableCompression]
        public ActionResult GetPostsByCategory(string id, int? page)
        {
            var pageIndex = page ?? 0;
            ViewBag.Action = "GetPostsByCategory";
            ViewBag.Route = "Default";
            ViewBag.Tag = id;
            ViewBag.Title = "Posts - " + id;
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View("Posts",BlogServices.FindPagedPostsByCategory(true, id, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
            else
            {
                return View("Posts",BlogServices.FindPagedPostsByCategory(false, id, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
        }

        //
        // GET: /Home/GetPostsByDate/id
        [EnableCompression]
        public ActionResult GetPostsByDate(int year, int month, int? page)
        {
            ViewBag.Title = "Posts - " + new DateTime(year, month, 1).ToString("y");
            ViewBag.Action = "GetPostsByDate";
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.Route = "PostByDate";
            var pageIndex = page ?? 0;
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View("Posts",BlogServices.FindPagedPostsByDate(true, year, month, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
            else
            {
                return View("Posts", BlogServices.FindPagedPostsByDate(false, year, month, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
        }

        //
        // GET: /Home/GetPostsByTag/id
        [EnableCompression]
        public ActionResult GetPostsByTag(string id, int? page)
        {
            ViewBag.Action = "GetPostsByTag";
            ViewBag.Route = "Default";
            ViewBag.Tag = id;
            ViewBag.Title = "Posts - " + id;
            var pageIndex = page ?? 0;
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View("Posts", BlogServices.FindPagedPostsByTag(true, id, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
            else
            {
                return View("Posts", BlogServices.FindPagedPostsByTag(true, id, pageIndex, Int32.Parse(BgResources.Pager_PostPerPage)));
            }
        }

        //
        // GET: /Home/GetPostAutoCompleteSuggestions/id
        [EnableCompression]
        public JsonResult GetPostAutoCompleteSuggestions(string term)
        {
            return Json(BlogServices.BuildPostsAutocompleteSuggestions(term), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ArchiveList()
        {
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return PartialView("_archiveListView", BlogServices.FindAllPostsByDate(true));
            }
            else
            {
                return PartialView("_archiveListView", BlogServices.FindAllPostsByDate(false));
            }
            
        }

        /// <summary>
        /// Subscribe the latest post in this website
        /// </summary>
        /// <returns>Xml rss 2.0 formatted Posts</returns>
        public ActionResult RssFeed()
        {
            bool ispremium;
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                ispremium = true;
            }
            else
            {
                ispremium = false;
            }
            var postItems = BlogServices.FindRSSPosts(ispremium, 20)
                .Select(p => new SyndicationItem(p.Title, p.Description, new Uri(Url.AbsoluteAction("GetPostByCode", "Post", new { id = p.Code }))) 
                {
                    PublishDate = new DateTimeOffset(p.DateCreated),
                    Copyright = new TextSyndicationContent(BgResources.Messages_Copyright),                    
                });
            var feed = new SyndicationFeed(String.Format(Resources.AppMessages.Rss_Latest_Post_Title,BgResources.Messages_SiteTitle), Resources.AppMessages.Rss_Latest_Post_Description, new Uri(Url.AbsoluteAction("NewPosts", "Post")), postItems);
            return new FeedResult(new Rss20FeedFormatter(feed));
        }	

        IEnumerable<Post> Posts { get; set; }
    }
}