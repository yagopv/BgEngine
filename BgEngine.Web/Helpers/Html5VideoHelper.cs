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

using System.Web.Mvc;

namespace System.Web.Helpers
{
    public static class Html5VideoHelper
    {
        public static MvcHtmlString Html5Video(this HtmlHelper html, string path, string width = "300", string height = "200", bool controls = false)
        {
            TagBuilder videotag = new TagBuilder("video");
            videotag.MergeAttribute("src", path);
            videotag.MergeAttribute("width", width);
            videotag.MergeAttribute("height", height);
            if (controls)
            {
                videotag.MergeAttribute("controls", "controls");
            }            
            return MvcHtmlString.Create(videotag.ToString());
        }
    }
}