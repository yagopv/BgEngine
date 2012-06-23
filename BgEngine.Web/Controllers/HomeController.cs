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

using BgEngine.Security.Services;
using BgEngine.Filters;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Web.Helpers;
using BgEngine.ViewModels;


namespace BgEngine.Controllers
{

    public class HomeController : BaseController
    {
        
        IBlogServices BlogServices;
        IMediaServices MediaServices;

        public HomeController(IBlogServices blogservices, IMediaServices mediaservices)
        {
            this.BlogServices = blogservices;
            this.MediaServices = mediaservices;
        }

        //
        // GET: /Home/
        [EnableCompression]
        public ActionResult Index()
        {
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View(BlogServices.HomePostsForRole(true, Int32.Parse(BgResources.Pager_HomeIndexPostsPerPage)));
            }
            else
            {
                return View(BlogServices.HomePostsForRole(false, Int32.Parse(BgResources.Pager_HomeIndexPostsPerPage)));
            }
        }

        //
        // GET: /Home/Galeria
        [EnableCompression]
        [HandleError(View = "MissedArgumentError", ExceptionType = typeof(ArgumentException))]
        public ActionResult Galeria()
        {
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View(MediaServices.FindAlbumsForRole(true));
            }
            else
            {
                return View(MediaServices.FindAlbumsForRole(false));
            }
        }

        //
        // GET: /Home/Videos
        [EnableCompression]
        public ActionResult Videos(int? page, string sortdir, string searchstring)
        {
            if (!String.IsNullOrEmpty(searchstring))
            {
                Session["videosearchstring"] = searchstring;
            }
            else
            {
                Session["videosearchstring"] = String.Empty;
            }
            var pageIndex = page ?? 0;
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return View(MediaServices.FindVideosForRole(true, pageIndex, Session["videosearchstring"].ToString()));
            }
            else
            {
                return View(MediaServices.FindVideosForRole(false, pageIndex, Session["videosearchstring"].ToString()));                
            }
        }

        //
        // GET: /Home/About
        [EnableCompression]
        public ActionResult About()
        {
            return View(BlogServices.FindAboutMe());
        }

        public PartialViewResult Contact()
        {
            return PartialView("Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public PartialViewResult SendContactMessage(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string message =  "<h3><strong>Email</strong></h3>" +
                                      "<p>" + model.Email + "</p>" +
                                      "<h3><strong>Web</strong></h3>" +
                                      "<p>" + model.Web + "</p>" +
                                      "<h3><strong>Message</strong></h3>" +
                                      "<p>" + model.Message + "</p>";
                    Utilities.SendMail(BgResources.Email_UserName, model.Subject, message, true);
                    return PartialView("AjaxSuccess", Resources.AppMessages.Contact_Mail_Sent);
                }
                catch (Exception)
                {
                    return PartialView("AjaxError", Resources.AppMessages.Contact_Mail_Error);
                }
            }
            return PartialView("AjaxError", Resources.AppMessages.Contact_Mail_Error);
        }
    }
}
