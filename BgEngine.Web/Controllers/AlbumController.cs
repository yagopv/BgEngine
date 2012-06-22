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

using System.Linq;
using System.Web.Mvc;

using BgEngine.Domain.EntityModel;
using BgEngine.Security.Services;
using BgEngine.Web.Results;
using BgEngine.Filters;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Controllers
{
    public class AlbumController : BaseController
    {
        IMediaServices MediaServices;
        IService<Album> AlbumServices;
        /// <summary>
        /// ctor
        /// </summary>
        public AlbumController(IMediaServices mediaservices, IService<Album> albumservices)
        {
            this.MediaServices = mediaservices;
            this.AlbumServices = albumservices;
        }
        /// <summary>
        /// Show all the Albums
        /// </summary>
        /// <returns>List of Albums</returns>          
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Index()
        {
            return View(AlbumServices.FindAllEntities(null,null,null));
        }
        /// <summary>
        /// Get Album to Edit
        /// </summary>
        /// <param name="id">The identity of the Album</param>
        /// <returns>The View with the Album to Edit</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Edit(int id)
        {
            return View(AlbumServices.FindEntityByIdentity(id));
        }
        /// <summary>
        /// Save the Updated Album to the Database
        /// </summary>
        /// <param name="album">The updated Album</param>
        /// <returns>Redirect to Index page if the Album was saved.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                AlbumServices.SaveEntity(album);
                return RedirectToRoute("Default",new {controller="Album", action="Index"});
            }
            return View(album);
        }
        /// <summary>
        /// Get an Album for Delete
        /// </summary>
        /// <param name="id">The identity of the Album</param>
        /// <returns>A view for delete confirmation</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Delete(int id)
        {
            return View(AlbumServices.FindEntityByIdentity(id));
        }
        /// <summary>
        /// Delete Album confirmation
        /// </summary>
        /// <param name="id">The identity of the Album</param>
        /// <param name="deleteRelated">If should delete related Images</param>
        /// <returns>Redirect to Index View if the Album was deleted</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult DeleteConfirmed(int id, bool deleteRelated)
        {
            MediaServices.DeleteAlbum(id, deleteRelated, HttpContext.Server);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Create a new Album
        /// </summary>
        /// <returns>The View for Album creation</returns>
        [Authorize(Roles = "Admin")]
        [EnableCompression]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Save the new Album
        /// </summary>
        /// <param name="album">The Album to add</param>
        /// <returns>Redirect to Index if the Album was Created</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCompression]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                AlbumServices.AddEntity(album);
                return RedirectToAction("Index");
            }
            return View(album);
        }
        /// <summary>
        /// Get all the Images in Album
        /// </summary>
        /// <param name="id">The Album identity</param>
        /// <returns>Return the Galleria of Images representing an Album</returns>
        [EnableCompression]
        public ActionResult Galleria(int id)
        {
            Album album = AlbumServices.FindEntityByIdentity(id);
            if ((album.IsPublic) || (CodeFirstSecurity.IsAuthenticated && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole))))
            {
                return View("Galleria", album);
            }
            else
            {
                TempData["returnUrl"] = Request.Url.ToString();
                return RedirectToRoute("Default", new { controller = "Account", action = "LogOn" });
            }
        }
        /// <summary>
        /// Download an Album converting a list of Images in a zip file
        /// </summary>
        /// <param name="id">The identity of the Album</param>
        /// <returns>The zip file with all the images in Album</returns>
        public ActionResult DownloadAlbum(int id)
        {
            ZipResult result = new ZipResult(MediaServices.GetImagePathsForDownload(id, HttpContext.Server));
            Album album = AlbumServices.FindAllEntities(a => a.AlbumId == id,null,null).FirstOrDefault();
            if (album != null)
            {
                result.FileName = album.Name.Replace(" ","_") + ".zip";
            }
            return result;
        }
    }
}
