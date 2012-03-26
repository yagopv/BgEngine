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

using System.Net.Mail;
using System;

using Mvc.Mailer;
using BgEngine.Domain.EntityModel;
using BgEngine.Application.ResourceConfiguration;

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
    }
}