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
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BgEngine.Web.Results
{
    /// <summary>
    /// Result for return rss content
    /// </summary>
    public class FeedResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }

        private readonly SyndicationFeedFormatter feed;
        public SyndicationFeedFormatter Feed{
            get { return feed; }
        }

        public FeedResult(SyndicationFeedFormatter feed) {
            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context) {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (feed != null)
                using (var xmlWriter = new XmlTextWriter(response.Output)) {
                    xmlWriter.Formatting = Formatting.Indented;
                    feed.WriteTo(xmlWriter);
                }
        }
    }
}