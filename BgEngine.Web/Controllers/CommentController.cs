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
using System.Web.Mvc;
using System.Linq;

using BgEngine.Domain.EntityModel;
using BgEngine.Security.Services;
using BgEngine.Filters;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Web.ViewModels;
using System.Collections.Generic;

namespace BgEngine.Controllers
{
    public class CommentController : BaseController
    {
        IBlogServices BlogServices;
        IService<Comment> CommentServices;
        IService<User> UserServices;
        IService<Post> PostServices;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="blogservices">Blog Services application methods</param>
        /// <param name="commentservices">Generic Comment Application Services</param>
        /// <param name="userservices">Generic User Service</param>
        /// <param name="postservices">Generic Post Service</param>
        public CommentController(IBlogServices blogservices, IService<Comment> commentservices, IService<User> userservices, IService<Post> postservices)
        {
            this.BlogServices = blogservices;
            this.CommentServices = commentservices;
            this.UserServices = userservices;
            this.PostServices = postservices;
        }

        /// <summary>
        /// Show list of comments
        /// </summary>
        /// <param name="page">Page to be shown</param>
        /// <param name="sort">The field to be sorted by</param>
        /// <param name="sortdir">The sort direction (ASC,DESC)</param>
        /// <returns>View showing the list of comments</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_CommentsPerPage;
            ViewBag.TotalComments = CommentServices.TotalNumberOfEntity();
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
                return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.DateCreated, false,"User"));
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "commentid":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.CommentId, dir, "User"));
                    case "message":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.Message, dir, "User"));
                    case "isrelatedcomment":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.isRelatedComment, dir, "User"));
                    case "datecreated":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.DateCreated, dir, "User"));
                    case "dateupdated":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.DateUpdated, dir, "User"));
                    case "anonymoususer.username":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.AnonymousUser.Username, dir, "User"));
                    case "anonymoususer.email":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.AnonymousUser.Email, dir, "User"));
                    case "post.title":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.Post.Title, dir, "User"));
                    case "post.code":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.Post.Code, dir, "User"));
                    case "user.username":
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.User.Username, dir, "User"));
                    default:
                        return View(CommentServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CommentsPerPage), c => c.DateCreated, false, "User"));                        
                }
            }
        }

        /// <summary>
        /// Show the details for a comment
        /// </summary>
        /// <param name="id">Identity of the comment</param>
        /// <returns>View showing the details of the Comment</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Details(int id)
        {
            return View(CommentServices.FindEntityByIdentity(id));
        }

        /// <summary>
        /// Show a View with a Form allowing Comment creation
        /// </summary>
        /// <returns>A View with a Form for Comment creation</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(PostServices.FindAllEntities(null,null,null), "PostId", "Title");
            ViewBag.UserEmail = new SelectList(UserServices.FindAllEntities(null,null,null), "UserId", "UserName");
            return View();
        } 

        /// <summary>
        /// Accepts a Form and creates a new Comment
        /// </summary>
        /// <param name="comment">The Comment object</param>
        /// <returns>Redirect to index or the same view with model errors</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                BlogServices.CreateComment(comment,UserServices.FindEntityByIdentity(CodeFirstSecurity.CurrentUserId));
                return RedirectToAction("Index");  
            }
            ViewBag.PostId = new SelectList(PostServices.FindAllEntities(null, null, null), "PostId", "Title", comment.PostId);
            return View(comment);
        }
        
        /// <summary>
        /// Show the comment details in edit mode
        /// </summary>
        /// <param name="id">Identity of the Comment</param>
        /// <returns>A view in edit mode with the details of a Comment</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Edit(int id)
        {
            Comment comment = CommentServices.FindEntityByIdentity(id);
            ViewBag.PostId = new SelectList(PostServices.FindAllEntities(null, null, null), "PostId", "Title", comment.PostId);
            return View(comment);
        }

        /// <summary>
        /// Receive Comment details from Form and save it in the system
        /// </summary>
        /// <param name="comment">The Comment object</param>
        /// <returns>Redirect or same view if model error</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                BlogServices.SaveComment(comment);
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(PostServices.FindAllEntities(null, null, null), "PostId", "Title", comment.PostId);
            return View(comment);
        }

        /// <summary>
        /// Show details of the Comment and ask for deletion confirmation
        /// </summary>
        /// <param name="id">Identity of the Comment</param>
        /// <returns>A View showing details and removal confirmation</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Delete(int id)
        {
            return View(CommentServices.FindEntityByIdentity(id));
        }

        /// <summary>
        /// Delete a Comment from the system
        /// </summary>
        /// <param name="id">Identity of the Comment</param>
        /// <returns>Redirect or same View of error</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogServices.DeleteComment(id);
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="comment">Comment object</param>
        /// <returns>Json object with result of the operation</returns>
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType=typeof(HttpAntiForgeryException),View=("AntiForgeryError"))]
        public ActionResult AddComment(Comment comment)
        {
           if (TryValidateModel(comment))
           {
               if (!CodeFirstSecurity.IsAuthenticated)
               {
                   AnonymousCommentViewModel user;
                   if (comment.AnonymousUser != null)
                   {
                       user = AutoMapper.Mapper.Map<AnonymousUser, AnonymousCommentViewModel>(comment.AnonymousUser);
                   }
                   else
                   {
                       user = new AnonymousCommentViewModel();
                   }
                   if (TryValidateModel(user))
                   {
                       BlogServices.CreateComment(comment, null);
                       return Json(new { result = "ok" });
                   }
                   else
                   {
                       return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray() });
                   }
               }
               else
               {
                   BlogServices.CreateComment(comment, UserServices.FindEntityByIdentity(CodeFirstSecurity.CurrentUserId));
                   return Json(new { result = "ok" });
               }
           }
           return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray() });
        }

        /// <summary>
        /// Add Comment related to another one
        /// </summary>
        /// <param name="comment">Comment object</param>
        /// <param name="parent">Identity of the parent</param>
        /// <returns>Json object with result of the operation</returns>
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = ("AntiForgeryError"))]
        public ActionResult AddRelatedComment(Comment comment, int parent)
        {
            if (TryValidateModel(comment))
            {
                if (!CodeFirstSecurity.IsAuthenticated)
                {
                    AnonymousCommentViewModel user;
                    if (comment.AnonymousUser != null)
                    {
                        user = AutoMapper.Mapper.Map<AnonymousUser, AnonymousCommentViewModel>(comment.AnonymousUser);
                    }
                    else
                    {
                        user = new AnonymousCommentViewModel();
                    }
                    if (TryValidateModel(user))
                    {
                        BlogServices.AddRelatedComment(comment, parent, null);
                        return Json(new { result = "ok" });
                    }
                    else
                    {
                        return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray() });
                    }
                }
                else
                {
                    BlogServices.AddRelatedComment(comment, parent, UserServices.FindEntityByIdentity(CodeFirstSecurity.CurrentUserId));
                    return Json(new { result = "ok" });
                }
            }
            return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray() });
        }

        /// <summary>
        /// Retrieve list of comments for a Post
        /// </summary>
        /// <param name="postid">Identity of the Post</param>
        /// <returns>View showing Comments related to a Post</returns>
        [EnableCompression]
        public PartialViewResult GetPostComments(int postid)
        {
            return PartialView(CommentServices.FindAllEntities(c => c.PostId == postid,null,null));
        }

        /// <summary>
        /// Retrieve Anonymous Comment fields
        /// </summary>
        /// <returns>Partial View showing Anonymous Comment field</returns>
        public PartialViewResult AnonymousComment()
        {
            return PartialView();
        }
    }
}