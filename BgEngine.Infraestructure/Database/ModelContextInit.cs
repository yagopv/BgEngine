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
using System.Linq;
using System.Data.Entity;

using BgEngine.Infraestructure.UnitOfWork;
using BgEngine.Domain.EntityModel;
using BgEngine.Infraestructure.Security;


namespace BgEngine.Infraestructure.DatabaseInitialization
{
    /// <summary>
    /// Creates a clean Database
    /// </summary>
    public class ModelContextInit : CreateDatabaseIfNotExists<BlogUnitOfWork>
    {
        protected override void Seed(BlogUnitOfWork context)
        {            
            //Roles 
            CodeFirstRoleProvider provider = new CodeFirstRoleProvider();
            provider.CreateRole("admin");
            provider.CreateRole("user");
            provider.CreateRole("premium");

            // Create indexes
            context.Database.ExecuteSqlCommand("CREATE INDEX IDX_Posts_Code ON Posts (Code);");
            context.Database.ExecuteSqlCommand("CREATE INDEX IDX_Posts_DateCreated ON Posts (DateCreated DESC);");

            //Resources
            context.Set<BlogResource>().Add(new BlogResource { Name = "Admin_Role", Value="admin" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Categories_Number_of_Categories_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Comments_Number_of_Comments_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Copyright", Value = "©MyCopyright 2XXX" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Dark_Background_Themes", Value = "" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Dark_Header_Themes", Value = "Aristo;Rocket;Cobalt" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Default_Theme", Value = "Cobalt" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Directories_Temp_Data", Value = "~/Content/Files/Temp_Data/" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_Password", Value = "xxxxxx" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_UserName", Value = "admin@mydomain.com"  });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Google_Analytics_Track_Code", Value = "xx-xxxxxxxx-x" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Index_Number_of_Posts", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Logo", Value = "~/Content/Icons/logo.jpg" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "No_Image", Value = "~/Content/Icons/no_image.jpg" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Posts_Number_of_Posts_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Premium_Role", Value = "premium" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Private_Key_Http", Value = "6LdofcQSAAAAAMGb119SbYgX18pwtPnPbBNk1tMH" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Private_Key_localhost", Value = "6LdqfcQSAAAAAKqgoeIUNlYbOJFHM3rhia2kpbRW" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Public_Key_Http", Value = "6LdofcQSAAAAAEw0fhwatfkTAJJlYEyjlCTrgKKm" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Public_Key_localhost", Value = "6LdqfcQSAAAAAHP4vF9uTfxmao2C4e8oYOoT1_dK" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Roles_Number_of_Roles_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SearchImages_Number_of_Images_per_Page", Value = "15" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SearchVideos_Number_of_Videos_per_Page", Value = "12" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SiteTitle", Value = "MySiteName.com"});
            context.Set<BlogResource>().Add(new BlogResource { Name = "SiteUrl", Value = "http://www.MySiteDomain.com"});
            context.Set<BlogResource>().Add(new BlogResource { Name = "Smtp_Port", Value = "25"});
            context.Set<BlogResource>().Add(new BlogResource { Name = "Smtp_Server", Value = "smtp.live.com" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_SSL", Value = "true" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Tags_Number_of_Tags_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "ThumbnailHeight", Value = "150" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "ThumbnailWidth", Value = "200" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Users_Number_of_Users_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Video_Container_Height", Value = "160" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Video_Container_Width", Value = "250" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Posts_HomeIndexPostsPerPage", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Twitter_User", Value = "" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Twitter_Search_Query", Value = "" });
            context.SaveChanges();
        }
    }
}
