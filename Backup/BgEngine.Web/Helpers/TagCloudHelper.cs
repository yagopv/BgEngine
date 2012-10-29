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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace System.Web.Helpers
{
    public static class TagCloudHelper
    {
        public static MvcHtmlString TagCloud(this HtmlHelper html, IDictionary<string, int> tagsAndCounts, Func<string, string> urlExpression, object htmlAttributes)
        {
            if (tagsAndCounts == null || !tagsAndCounts.Any())
                return MvcHtmlString.Empty;

            var min = tagsAndCounts.Min(t => t.Value);
            var max = tagsAndCounts.Max(t => t.Value);
            var dist = (max - min) / 3;

            var links = new StringBuilder();
            foreach (var tag in tagsAndCounts)
            {
                string tagClass;

                if (tag.Value == max)
                {
                    tagClass = "largest";
                }
                else if (tag.Value > (min + (dist * 2)))
                {
                    tagClass = "large";
                }
                else if (tag.Value > (min + dist))
                {
                    tagClass = "medium";
                }
                else if (tag.Value == min)
                {
                    tagClass = "smallest";
                }
                else
                {
                    tagClass = "small";
                }

                links.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"{2}\">{1}</a>{3}",
                                   urlExpression(tag.Key), tag.Key, tagClass, Environment.NewLine);
            }

            var div = new TagBuilder("div");
            div.MergeAttribute("class", "tag-cloud");
            div.InnerHtml = links.ToString();

            if (htmlAttributes != null)
            {
                div.MergeAttributes(new RouteValueDictionary(htmlAttributes), false);
            }
            return MvcHtmlString.Create(div.ToString());
        }
    }
}