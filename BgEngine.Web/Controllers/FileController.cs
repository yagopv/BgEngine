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

using BgEngine.Web.Results;
using BgEngine.Domain.EntityModel;
using BgEngine.Application.Services;

namespace BgEngine.Controllers
{
    public class FileController : BaseController
    {

        IMediaServices MediaServices;
        IService<Album> AlbumServices;

		/// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediaservices">Media services for manage Image, Video</param>
        /// <param name="albumservices">Generic Services for Album Entity</param>
        public FileController(IMediaServices mediaservices, IService<Album> albumservices)
        {
            this.MediaServices = mediaservices;
            this.AlbumServices = albumservices;
        }

        /// <summary>
        /// Render SWF Object control for Upload file using this flash object
        /// </summary>
		/// <param name="albumid">Identity of the album</param>
        /// <returns>Render SWF Object</returns>
        [ChildActionOnly]        
        public ActionResult SWFUpload(int? albumid)
        {
            Album album = null;
            if (albumid != null)
            {
                album = AlbumServices.FindEntityByIdentity(albumid);
                return PartialView("SWFUpload", album);
            }
            else
            {
                album = AlbumServices.FindAllEntities(a => a.Name == Resources.AppMessages.Default_Album_Name,null,null).FirstOrDefault();                
            }
            return PartialView("SWFUpload", album);
            
        }

		/// <summary>
        /// Upload file to album (Image folder) or file (Files folder)
        /// </summary>
		/// <param name="albumid">Identity of the album</param>
        /// <returns>Json representing the upload result</returns>
        [HttpPost,ActionName("SWFUpload")]
        public FileUploadJsonResult SWFUploadToAlbum(int? albumid)
        {

            MediaServices.UploadFileToAlbum(Request.Files[0], Server, albumid);
            return new FileUploadJsonResult { Data = "ok" };
        }
    }
}
