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
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Web;
using PagedList;

using BgEngine.Domain.EntityModel;
using BgEngine.Application.DTO;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Contract with operations for Audio, Video, Image and whatever Media involved
    /// </summary>
    public interface IMediaServices
    {
        void AddImage(Image entity);
        IEnumerable<Image> SearchForImagesByParam<TKey>(Expression<Func<BgEngine.Domain.EntityModel.Image, TKey>> orderByExpression, string searchstring = "");
        void DeleteAlbum(int albumid, bool deleterelated, HttpServerUtilityBase server);
        string[] GetImagePathsForDownload(int id, HttpServerUtilityBase server);
        bool DeleteImageFromDatabaseAndServer(int id, HttpServerUtilityBase server);
        List<ImageDTO> BuildGalleriaForAlbum(int albumid, Func<string, string> url);
        List<StringValueDTO> BuildImageAutocompleteSuggestions(string searchstring);
        object UploadFileToServer(ICollection<HttpPostedFileBase> files, HttpServerUtilityBase server, HttpRequestBase request, int? albumid);
        void UploadFileToAlbum(HttpPostedFileBase file, HttpServerUtilityBase server, int? albumid);
        IPagedList<Video> FindVideosForRole(bool ispremium, int pageindex, string searchstring);
        List<StringValueDTO> BuildVideoAutocompleteSuggestions(string searchstring, bool ispremium);
        IEnumerable<Image> FindImagesForRole(bool ispremium);
        IEnumerable<Album> FindAlbumsForRole(bool ispremium);
        IEnumerable<Video> FindLatestVideos(int howmany, bool ispremium, string tag, string category);
		void CreateVideo(Video video, int[] tags);
		void UpdateVideo(Video video, int[] tags);
    }
}
