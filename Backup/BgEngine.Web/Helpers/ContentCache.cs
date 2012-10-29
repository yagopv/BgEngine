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

using System.Text;

namespace System.Web.Mvc
{
    public static class CacheContent
    {
        /// <summary>
        /// Renders a Javascript script include tag and appends the assembly's hash code as a querystring to the file 
        /// to ensure users load the latest code file when the code is updated.
        /// </summary>
        /// <param name="type">Pass a type so we can make a hash of its assembly.  This should be a class in your web project.</param>
        public static MvcHtmlString JavascriptInclude(this HtmlHelper html, Type type, params string[] urls)
        {
            var sb = new StringBuilder();
            var hash = type.Assembly.GetHashCode();
            foreach (string url in urls)
                sb.AppendLine(string.Format("<script language=\"javascript\" type=\"text/javascript\" src=\"{0}?v={1}\"></script>",
                    url, hash));
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Renders a CSS include tag and appends the assembly's hash code as a querystring to the file
        /// to ensure users load the latest code file when the code is updated.
        /// </summary>
        /// <param name="type">Pass a type so we can make a hash of its assembly.  This should be a class in your web project.</param>
        public static MvcHtmlString CssInclude(this HtmlHelper html, Type type, params string[] urls)
        {
            var sb = new StringBuilder();
            var hash = type.Assembly.GetHashCode();
            foreach (string url in urls)
                sb.AppendLine(string.Format("<link rel=\"Stylesheet\" type=\"text/css\" href=\"{0}?v={1}\" />",
                         url, hash));
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}