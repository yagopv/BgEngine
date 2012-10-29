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

namespace BgEngine.Controllers
{
    public class RoleController : BaseController
    {
        
        IAccountServices AccountServices;

        public RoleController(IAccountServices accountservices)
        {
            this.AccountServices = accountservices;
        }

        //
        // GET: /Tag/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_UsersPerPage;
            ViewBag.TotalRoles = AccountServices.TotalNumberOf<Role>();
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
            return View(AccountServices.RetrievePagedRoles(sort, pageIndex,dir));
        }

        //
        // GET: /Tag/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(Guid id)
        {
            return View(AccountServices.FindRoleByIdentity(id));
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
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                AccountServices.CreateNew<Role>(role);
                return RedirectToAction("Index");  
            }
            return View(role);
        }
        
        //
        // GET: /Tag/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            return View(AccountServices.FindRoleByIdentity(id));
        }

        //
        // POST: /Tag/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                AccountServices.Save<Role>(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            return View(AccountServices.FindRoleByIdentity(id));
        }

        //
        // POST: /Tag/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                AccountServices.DeleteRole(id);
            }
            catch (OperationCanceledException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View(AccountServices.FindRoleByIdentity(id));
            }
            return RedirectToAction("Index");
        }
    }
}