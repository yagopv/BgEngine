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
using System.Web;
using System.Web.Mvc;

using PagedList;
using BgEngine.Domain.EntityModel;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Controllers
{
    public class ImageController : BaseController
    {

        IMediaServices MediaServices;
        IService<Image> ImageServices;
        IService<Album> AlbumServices;

        public ImageController(IMediaServices mediaservices, IService<Image> imageservices, IService<Album> albumservices)
        {
            this.MediaServices = mediaservices;
            this.ImageServices = imageservices;
            this.AlbumServices = albumservices;
        }

        //
        // GET: /Image/
        [Authorize(Roles = "Admin")]
        [HandleError(View = "MissedArgumentError", ExceptionType = typeof(ArgumentException))]
        public ViewResult Index(int id)
        {
            Album album = AlbumServices.FindEntityByIdentity(id);
            if (album == null)
            {
                return new NotFoundMvc.NotFoundViewResult();
            }
            else
            {
                return View(album);
            }
        }

        //
        // GET: /Image/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            return View(ImageServices.FindEntityByIdentity(id));
        }

        //
        // GET: /Image/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                ImageServices.AddEntity(image);
                return RedirectToAction("Index");  
            }

            return View(image);
        }
        
        //
        // GET: /Image/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(ImageServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Image/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                ImageServices.SaveEntity(image);
                return RedirectToAction("Index", new { id = image.AlbumId });
            }
            return View(image);
        }

        //
        // GET: /Image/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(ImageServices.FindEntityByIdentity(id));
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                return MediaServices.DeleteImageFromDatabaseAndServer(id, HttpContext.Server)
                       ?
                       Json(new { result = "ok" })
                       :
                       Json(new { result = Resources.AppMessages.Trash_Error_Deleting_Images });           
            }
            return Json(new { result = Resources.AppMessages.Trash_Error_Deleting_Images });
        }

        //
        // GET: /Image/Galleria
        public JsonResult GetGalleriaJson(int id)
        {
            return Json(MediaServices.BuildGalleriaForAlbum(id, url => Url.Content(url)), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Home/GetImageAutoCompleteSuggestions/id

        public JsonResult GetImageAutoCompleteSuggestions(string term)
        {
            return Json(MediaServices.BuildImageAutocompleteSuggestions(term), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ShowAllImages(int? page,string searchstring)
        {
            if (!String.IsNullOrEmpty(searchstring))
            {
                Session["imagesearchstring"] = searchstring;
            }
            else
            {
                Session["imagesearchstring"] = String.Empty;
            }
            var pageIndex = page ?? 0;
            return PartialView(MediaServices.SearchForImagesByParam(i => i.DateCreated, Session["imagesearchstring"].ToString()).ToPagedList(pageIndex, Int32.Parse(BgResources.Pager_SearchImagesPerPage)));
        }   
    }
}