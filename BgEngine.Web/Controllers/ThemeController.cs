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

namespace BgEngine.Controllers
{
    public class ThemeController : BaseController
    {
	    /// <summary>
        /// Set the cookie storing theme selected by the user
        /// </summary>
		/// <param name="theme">The name of the Theme stored in Themes folder</param>
        /// <returns>Json representing result of the operation</returns>
        [HttpPost]
        public JsonResult SetThemeCookie(string theme)
        {
            if (!string.IsNullOrEmpty(theme))
            {
                if (Request.Cookies["app_theme"] != null)
                {
                    Response.Cookies["app_theme"].Value = theme;
                    Response.Cookies["app_theme"].Expires = DateTime.Now.AddYears(1);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("app_theme", theme);
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }
            }
            return Json(true);
        }
    }
}
