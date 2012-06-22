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
using BgEngine.Web.ViewModels;
using BgEngine.Filters;
using BgEngine.Application.Services;
using BgEngine.Security.Services;
using BgEngine.Application.ResourceConfiguration;
using System.Data;


namespace BgEngine.Controllers
{ 
	public class VideoController : BaseController
	{
		IMediaServices MediaServices;
		IService<Video> VideoServices;
		IService<Category> CategoryServices;
		IService<Tag> TagServices;		

		public VideoController(IMediaServices mediaservices, 
							   IService<Video> videoservices,
							   IService<Category> categoryservices,
							   IService<Tag> tagservices)
		{
			this.MediaServices = mediaservices;
			this.VideoServices = videoservices;
			this.CategoryServices = categoryservices;
			this.TagServices = tagservices;						
		}

		//
		// GET: /Video/
		[Authorize]
		[EnableCompression]
		public ViewResult Index(int? page, string sortdir, string searchstring)
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
			return View(MediaServices.FindVideosForRole(true,pageIndex,Session["videosearchstring"].ToString()));
		}

		//
		// GET: /Home/GetVideoAutoCompleteSuggestions/id

		public JsonResult GetVideoAutoCompleteSuggestions(string term)
		{
			if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
			{
				return Json(MediaServices.BuildVideoAutocompleteSuggestions(term, true), JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(MediaServices.BuildVideoAutocompleteSuggestions(term, false), JsonRequestBehavior.AllowGet);
			}
		}

		//
		// GET: /Video/Details/5
		[Authorize]
		[EnableCompression]
		public ViewResult Details(int id)
		{
			Video video = VideoServices.FindEntityByIdentity(id);
			return View(video);
		}

		//
		// GET: /Video/Create
		[Authorize]
		[EnableCompression]
		public ActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null,null,null), "CategoryId", "Name");
			ViewBag.Tags = TagServices.FindAllEntities(null, null, null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
			return View();
		} 

		//
		// POST: /Video/Create

		[HttpPost]
		[Authorize]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult Create(Video video, int[] selectedtags)
		{
			if (ModelState.IsValid)
			{
				MediaServices.CreateVideo(video,selectedtags);
				return RedirectToAction("Index");  				
			}
			ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null, null, null), "CategoryId", "Name", video.CategoryId);
			ViewBag.Tags = TagServices.FindAllEntities(null,null,null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
			return View(video);
		}
		
		//
		// GET: /Video/Edit/5
		[Authorize]
		[EnableCompression]
		public ActionResult Edit(int id)
		{
			Video video = VideoServices.FindEntityByIdentity(id);
			ViewBag.CategoryId = new SelectList(CategoryServices.FindAllEntities(null, null, null), "CategoryId", "Name", video.CategoryId);
			ViewBag.Tags = TagServices.FindAllEntities(null, null, null).ToDictionary<Tag, int, string>(t => t.TagId, t => t.TagName);
			return View(video);
		}

		//
		// POST: /Video/Edit/5

		[HttpPost]
		[Authorize]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult Edit(Video video, int[] selectedtags)
		{
			Video videotoupdate =  VideoServices.FindAllEntities(v => v.VideoId == video.VideoId, null, "Tags").FirstOrDefault();
			if (TryUpdateModel(videotoupdate, "", null, new string[] { }))
			{
				try
				{
					MediaServices.UpdateVideo(videotoupdate, selectedtags);
					return RedirectToAction("Index");
				}
				catch (DataException)
				{
					ModelState.AddModelError("", Resources.AppMessages.Error_Saving_Changes);
				}            
			}
			return View(videotoupdate);
		}

		//
		// GET: /Video/Delete/5
		[Authorize]
		[EnableCompression]
		public ActionResult Delete(int id)
		{
			Video video = VideoServices.FindEntityByIdentity(id);
			return View(video);
		}

		//
		// POST: /Video/Delete/5

		[HttpPost, ActionName("Delete")]
		[Authorize]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult DeleteConfirmed(int id)
		{
			VideoServices.DeleteEntity(id);
			return RedirectToAction("Index");
		}

		[HttpPost]
		[Authorize]
		[EnableCompression]
		public ActionResult RenderVideo(RenderVideoViewModel Video)
		{
			return PartialView(Video);
		}

		/// <summary>
		/// Get latest videos published for home index
		/// </summary>
		/// <returns>List of videos</returns>
		public ActionResult LatestVideos(string tag, string category)
		{
            if (!(String.IsNullOrEmpty(tag) && String.IsNullOrEmpty(category)))
            {
                ViewBag.IsHome = false;
            }
            else
            {
                ViewBag.IsHome = true;
            }
			if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
			{
				return PartialView(MediaServices.FindLatestVideos(6,true,tag, category));
			}
			else
			{
				return PartialView(MediaServices.FindLatestVideos(6, false, tag, category));
			}
		}
	}
}