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
using System.Web.Helpers;
using System.IO;

using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Web.Helpers
{
    public static class Utilities
    {
        public static void SendMail(string email,string mailsubject, string mailbody, bool isHtml)
        {
            WebMail.SmtpServer = BgResources.Email_Server;
            WebMail.SmtpPort = Int32.Parse(BgResources.Email_SmtpPort);
            WebMail.EnableSsl = BgResources.Email_SSL;
            WebMail.UserName = BgResources.Email_UserName;
            WebMail.Password = BgResources.Email_Password;
            WebMail.From = BgResources.Email_UserName;
            WebMail.Send(to: email,
                          subject: mailsubject,
                          body: mailbody,
                          isBodyHtml: isHtml);
        }

        public static bool DeleteImageFromServer(string path, string thumbnailpath)
        {
            FileInfo fi1 = new FileInfo(path);
            if (fi1.Exists)
            {
                fi1.Delete();
            }
            else
            {
                return false;
            }
            FileInfo fi2 = new FileInfo(thumbnailpath);
            if (fi2.Exists)
            {
                fi2.Delete();
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool IsEvenNumber(int number)
        {
            return (number % 2 == 0);
        }
    }
}