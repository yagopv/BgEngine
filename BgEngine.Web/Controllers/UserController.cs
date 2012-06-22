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
using System.Web.Security;

using BgEngine.Domain.EntityModel;
using BgEngine.Web.ViewModels;
using BgEngine.Filters;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Controllers
{
    public class UserController : BaseController
    {

        IAccountServices AccountServices;

        public UserController(IAccountServices accountservices)
        {
            this.AccountServices = accountservices;
        }

        //
        // GET: /User/
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Index(int? page, string sort, string sortdir)
        {
            ViewBag.RowsPerPage = BgResources.Pager_UsersPerPage;
            ViewBag.TotalUsers =  AccountServices.TotalNumberOf<User>();
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

            return View(AccountServices.RetrievePagedUsers(sort,pageIndex,dir));
        }

        //
        // GET: /User/Details/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ViewResult Details(Guid id)
        {
            return View(AccountServices.FindUserByIdentity(id));
        }

        //
        // GET: /User/Create
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Create()
        {            
            return View();
        } 

        //
        // POST: /User/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(RegisterModel user, string[] selectedroles)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    AccountServices.CreateAccount(user.UserName, user.Password, user.Email, user.FirstName, user.LastName, user.TimeZone, user.Culture, false,selectedroles);
                    return RedirectToRoute("Default", new { controller = "User", action = "Index" });
                }
                catch (MembershipCreateUserException ex)
                {
                    if ((ex.StatusCode == MembershipCreateStatus.DuplicateUserName) || (ex.StatusCode == MembershipCreateStatus.InvalidUserName))
                    {
                        ModelState.AddModelError("UserName", ErrorCodeToString(ex.StatusCode));
                    }
                    else if ((ex.StatusCode == MembershipCreateStatus.DuplicateEmail) || (ex.StatusCode == MembershipCreateStatus.InvalidEmail))
                    {
                        ModelState.AddModelError("Email", ErrorCodeToString(ex.StatusCode));
                    }
                    else if (ex.StatusCode == MembershipCreateStatus.InvalidPassword)
                    {
                        ModelState.AddModelError("Password", ErrorCodeToString(ex.StatusCode));
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Se ha producido un error en el envío del mail. Intentelo de nuevo en unos minutos.");
                }
            }
            return View(user);
        }
        
        //
        // GET: /User/Edit/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Edit(Guid id)
        {
            return View(AccountServices.FindUserByIdentity(id));
        }

        //
        // POST: /User/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(User user, string[] selectedroles)
        {
            if (ModelState.IsValid)
            {
                AccountServices.SaveUser(user,selectedroles);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Delete(Guid id)
        {
            return View(AccountServices.FindUserByIdentity(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                AccountServices.DeleteUser(id);
            }
            catch (OperationCanceledException ex)
            {   
                ModelState.AddModelError("",ex.Message);
                return View(AccountServices.FindUserByIdentity(id));
            }
            return RedirectToAction("Index");
        }

        [EnableCompression]
        public ActionResult GetTooltipData(Guid id)
        {
            return PartialView("GetTooltipData", AccountServices.FindUserByIdentity(id));
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El usuario ya existe, por favor introduce otro nuevo";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un usuario con esa direccion de mail. Introduce una nueva direccion de mail";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña introducida no es válida";

                case MembershipCreateStatus.InvalidEmail:
                    return "La direccion de mail no es válida. Por favor revísala";

                case MembershipCreateStatus.InvalidUserName:
                    return "El usuario introducido no es un nombre de usuario válido";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}