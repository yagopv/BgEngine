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
using BgEngine.Domain.Filters;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Controllers
{
    public class CategoryController : BaseController
    {
        IService<Category> CategoryServices;
        /// <summary>
        /// ctor
        /// </summary>
        public CategoryController(IService<Category> categoryservices)
        {
            this.CategoryServices = categoryservices;
        }
        /// <summary>
        /// Show all the Categories
        /// </summary>
        /// <returns>List of Categories</returns>     
        [Authorize(Roles = "Admin")] 
        [EnableCompression]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_CategoriesPerPage;
            ViewBag.TotalCategories = CategoryServices.TotalNumberOfEntity();
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
                return View(CategoryServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), c => c.DateCreated, false));
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "name":
                        return View(CategoryServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), c => c.Name, dir));                       
                    case "datecreated":
                        return View(CategoryServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), c => c.DateCreated, dir));
                    default:
                        return View(CategoryServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), c => c.DateCreated, false));
                }
            }           
        }
        /// <summary>
        /// Show Category details
        /// </summary>
        /// <param name="id">The Identity of the Category</param>
        /// <returns>The View for check Category details/returns>     
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Details(int id)
        {
            return View(CategoryServices.FindEntityByIdentity(id));
        }
        /// <summary>
        /// Renders a View for create a Category
        /// </summary>
        /// <returns>The View for check Category details/returns>  
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryServices.AddEntity(category);                
                return RedirectToAction("Index");  
            }
            return View(category);
        }
        
        //
        // GET: /Category/Edit/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Edit(int id)
        {
            return View(CategoryServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Category/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryServices.SaveEntity(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Delete(int id)
        {
            return View(CategoryServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Category/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CategoryServices.DeleteEntity(id);
            }
            catch (OperationCanceledException ex)
            {
                ModelState.AddModelError("", Resources.AppMessages.Error_Category_With_Posts);
                return View(CategoryServices.FindEntityByIdentity(id));
            }
            return RedirectToAction("Index");
        }

        public ActionResult CategoryList()
        {
            return PartialView("_categoryListView", CategoryServices.FindAllEntities(null,null,"Posts"));
        }
    }
}