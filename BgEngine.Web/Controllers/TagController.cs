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

using BgEngine.Domain.EntityModel;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Security.Services;

namespace BgEngine.Controllers
{
    public class TagController : BaseController
    {
        IBlogServices BlogServices;
        IService<Tag> TagServices;

        public TagController(IBlogServices blogservices, IService<Tag> tagservices)
        {
            this.BlogServices = blogservices;
            this.TagServices = tagservices;
        }

        //
        // GET: /Tag/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_TagsPerPage;
            ViewBag.TotalTags = TagServices.TotalNumberOfEntity();
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
                return View(TagServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_TagsPerPage), t => t.TagName, false));                
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "tagname":
                        return View(TagServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_TagsPerPage), t => t.TagName, dir));                
                        break;
                    case "tagdescription ":
                        return View(TagServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_TagsPerPage), t => t.TagDescription, dir));                
                    default:
                        return View(TagServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_TagsPerPage), t => t.TagName, false));
                }
            }            
        }

        //
        // GET: /Tag/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            return View(TagServices.FindEntityByIdentity(id));
        }

        //
        // GET: /Tag/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tag/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagServices.AddEntity(tag);
                return RedirectToAction("Index");  
            }
            return View(tag);
        }
        
        //
        // GET: /Tag/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(TagServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Tag/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Tag Tag)
        {
            if (ModelState.IsValid)
            {
                TagServices.SaveEntity(Tag);
                return RedirectToAction("Index");
            }
            return View(Tag);
        }

        //
        // GET: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(TagServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id)
        {
            TagServices.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult TagCloud()
        {
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return PartialView("TagCloud", BlogServices.GetModelForTagCloud(true));
            }
            else
            {
                return PartialView("TagCloud", BlogServices.GetModelForTagCloud(false));
            }
            
        }
    }
}