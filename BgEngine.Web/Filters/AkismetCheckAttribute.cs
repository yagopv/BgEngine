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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Joel.Net;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Filters
{
    public class AkismetCheckAttribute : ActionFilterAttribute
    {
        public AkismetCheckAttribute(
            string authorField,
            string emailField,
            string websiteField,
            string commentField)
        {
            this.AuthorField = authorField;
            this.EmailField = emailField;
            this.WebsiteField = websiteField;
            this.CommentField = commentField;
        }

        public string AuthorField { get; set; }
        public string EmailField { get; set; }
        public string WebsiteField { get; set; }
        public string CommentField { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Create a new instance of the Akismet API and verify your key is valid.
            Akismet api = new Akismet(BgResources.Akismet_API_key, filterContext.HttpContext.Request.Url.AbsoluteUri , filterContext.HttpContext.Request.UserAgent);
            if (!api.VerifyKey()) throw new Exception("Akismet API key invalid.");

            //Now create an instance of AkismetComment, populating it with values
            //from the POSTed form collection.
            AkismetComment akismetComment = new AkismetComment
            {
                Blog = filterContext.HttpContext.Request.Url.AbsoluteUri,
                UserIp = filterContext.HttpContext.Request.UserHostAddress,
                UserAgent = filterContext.HttpContext.Request.UserAgent,
                CommentContent = filterContext.HttpContext.Request[this.CommentField],
                CommentType = "comment",
                CommentAuthor = filterContext.HttpContext.Request[this.AuthorField],
                CommentAuthorEmail = filterContext.HttpContext.Request[this.EmailField],
                CommentAuthorUrl = filterContext.HttpContext.Request[this.WebsiteField]
            };

            //Check if Akismet thinks this comment is spam. Returns TRUE if spam.
            if (api.CommentCheck(akismetComment))
                //Comment is spam, add error to model state.
                filterContext.Controller.ViewData.ModelState.AddModelError("spam", "Comment identified as spam.");

            base.OnActionExecuting(filterContext);
        }
    }
}