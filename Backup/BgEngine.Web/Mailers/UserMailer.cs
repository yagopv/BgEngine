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
using System.Net.Mail;
using System.Collections.Generic;

using Mvc.Mailer;

using BgEngine.Domain.EntityModel;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Application.DTO;
using BgEngine.Extensions;
using BgEngine.Application.Services;
using System.Linq;



namespace BgEngine.Web.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		public UserMailer():
			base()
		{
			MasterName="_Layout";
		}
		
        public virtual MailMessage PasswordReset(string token, User user)
		{
			var mailMessage = new MailMessage {Subject = String.Format(Resources.EmailTemplates.PasswordReset_Subject, BgResources.Messages_SiteTitle)};
            ViewBag.Token = token;
            ViewBag.UserName = user.Username;
            ViewBag.Email = user.Email;
            mailMessage.To.Add(user.Email);
            mailMessage.From = new MailAddress(BgResources.Email_UserName);
            PopulateBody(mailMessage, viewName: "PasswordReset");
			return mailMessage;
		}

        public virtual MailMessage Register(string token, string to, User user)
        {
            var mailMessage = new MailMessage { Subject = String.Format(Resources.EmailTemplates.RegisterMail_Subject, BgResources.Messages_SiteTitle) };
            ViewBag.Token = token;
            ViewBag.UserName = user.Username;
            ViewBag.Email = user.Email;
            ViewBag.ConfirmationToken = user.ConfirmationToken;
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress(BgResources.Email_UserName);
            PopulateBody(mailMessage, viewName: "Register");
            return mailMessage;
        }

        public virtual MailMessage ConfirmSubscription(string token, string to, SubscriptionDTO subscriptionDTO)
        {
            var mailMessage = new MailMessage { Subject = String.Format(Resources.EmailTemplates.SubscriptionMail_Subject, BgResources.Messages_SiteTitle) };
            ViewBag.Token = token;
            ViewBag.Name = subscriptionDTO.SubscriberName;
            ViewBag.Email = subscriptionDTO.SubscriberEmail;
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress(BgResources.Email_UserName);
            PopulateBody(mailMessage, viewName: "ConfirmSubscription");
            return mailMessage;
        }

        public virtual MailMessage GetNewsletterHtml(List<Post> newsletterPosts, string newslettername)
        {
            MasterName = "_Layout_Newsletter";
            var mailMessage = new MailMessage();
            ViewBag.Posts = newsletterPosts;
            ViewBag.Newsletter = newslettername;
            PopulateBody(mailMessage, viewName: "Newsletter");
            return mailMessage;
        }
    }
}