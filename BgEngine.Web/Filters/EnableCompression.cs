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

using System.Web;
using System.Web.Mvc;
using System.IO.Compression;

namespace BgEngine.Domain.Filters
{
    public class EnableCompressionAttribute : ActionFilterAttribute
    {
        const CompressionMode compress = CompressionMode.Compress;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (acceptEncoding == null)
                return;
            if (acceptEncoding.ToLower().Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.ToLower().Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}