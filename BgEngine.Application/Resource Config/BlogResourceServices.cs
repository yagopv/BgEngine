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

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Application.DTO;

namespace BgEngine.Application.ResourceConfiguration
{
    /// <summary>
    /// The purpose of this class is to initialize the resources static class for being accessed in the web app
    /// </summary>
    public class BlogResourceServices : IBlogResourceServices
    {
        IRepository<BlogResource> BlogResourceRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="blogresourcerepository"></param>
        public BlogResourceServices(IRepository<BlogResource> blogresourcerepository)
        {
            this.BlogResourceRepository = blogresourcerepository;
        }

        /// <summary>
        /// Initial load
        /// </summary>
        public void LoadResources()
        {
            BgResources.InitialLoad(BlogResourceRepository.Get(null, null, null));
        }

        /// <summary>
        /// Update the configuration options
        /// </summary>
        /// <param name="options">The DTO object with options</param>
        public void UpdateConfigOptions(ConfigOptionsDTO options)
        {            
            IEnumerable<BlogResource> blogresources = BlogResourceRepository.Get(null, null, null);
            foreach (BlogResource res in blogresources)
            {
                switch (res.Name)
                {
                    case "Admin_Role":
                        res.Value = options.Security_AdminRole;
                        BgResources.Security_AdminRole = res.Value;
                        break;
                    case "Categories_Number_of_Categories_per_Page":
                        res.Value = options.Pager_CategoriesPerPage;
                        BgResources.Pager_CategoriesPerPage = res.Value;
                        break;
                    case "Comments_Number_of_Comments_per_Page":
                        res.Value = options.Pager_CommentsPerPage;
                        BgResources.Pager_CommentsPerPage = res.Value;
                        break;
                    case "Copyright":
                        res.Value = options.Messages_Copyright;
                        BgResources.Messages_Copyright = res.Value;
                        break;
                    case "Default_Theme":
                        res.Value = options.Themes_Default;
                        BgResources.Themes_Default = res.Value;
                        break;
                    case "Email_Password":
                        res.Value = options.Email_Password;
                        BgResources.Email_Password = res.Value;
                        break;
                    case "Email_UserName":
                        res.Value = options.Email_UserName;
                        BgResources.Email_UserName = res.Value;
                        break;
                    case "Google_Analytics_Track_Code":
                        res.Value = options.Analytics_GoogleAnalyticsCode;
                        BgResources.Analytics_GoogleAnalyticsCode = res.Value;
                        break;
                    case "Index_Number_of_Posts":
                        res.Value = options.Pager_PostPerPage;
                        BgResources.Pager_PostPerPage = res.Value;
                        break;
                    case "Posts_Number_of_Posts_per_Page":
                        res.Value = options.Pager_PostPerPage;
                        BgResources.Pager_PostPerPage = res.Value;
                        break;
                    case "Premium_Role":
                        res.Value = options.Security_PremiumRole;
                        BgResources.Security_PremiumRole = res.Value;
                        break;
                    case "Recaptcha_Private_Key_Http":
                        res.Value = options.Recaptcha_PrivateKeyHttp;
                        BgResources.Recaptcha_PrivateKeyHttp = res.Value;
                        break;
                    case "Recaptcha_Public_Key_Http":
                        res.Value = options.Recaptcha_PublicKeyHttp;
                        BgResources.Recaptcha_PublicKeyHttp = res.Value;
                        break;
                    case "Roles_Number_of_Roles_per_Page":
                        res.Value = options.Pager_RolesPerPage;
                        BgResources.Pager_RolesPerPage = res.Value;
                        break;
                    case "SearchImages_Number_of_Images_per_Page":
                        res.Value = options.Pager_SearchImagesPerPage;
                        BgResources.Pager_SearchImagesPerPage = res.Value;
                        break;
                    case "SearchVideos_Number_of_Videos_per_Page":
                        res.Value = options.Pager_SearchVideosPerPage;
                        BgResources.Pager_SearchVideosPerPage = res.Value;
                        break;
                    case "SiteTitle":
                        res.Value = options.Messages_SiteTitle;
                        BgResources.Messages_SiteTitle = res.Value;
                        break;
                    case "SiteUrl":
                        res.Value = options.Messages_SiteUrl;
                        BgResources.Messages_SiteUrl = res.Value;
                        break;
                    case "Smtp_Port":
                        res.Value = options.Email_SmtpPort;
                        BgResources.Email_SmtpPort = res.Value;
                        break;
                    case "Smtp_Server":
                        res.Value = options.Email_Server;
                        BgResources.Email_Server = res.Value;
                        break;
                    case "Email_SSL":
                        res.Value = options.Email_SSL.ToString();
                        BgResources.Email_SSL = bool.Parse(res.Value);
                        break;
                    case "Tags_Number_of_Tags_per_Page":
                        res.Value = options.Pager_TagsPerPage;
                        BgResources.Pager_TagsPerPage = res.Value;
                        break;
                    case "Users_Number_of_Users_per_Page":
                        res.Value = options.Pager_UsersPerPage;
                        BgResources.Pager_UsersPerPage = res.Value;
                        break;
                    case "Posts_HomeIndexPostsPerPage":
                        res.Value = options.Pager_HomeIndexPostsPerPage;
                        BgResources.Pager_HomeIndexPostsPerPage = res.Value;
                        break;
                    case "Twitter_User":
                        res.Value = options.Twitter_User;
                        BgResources.Twitter_User = res.Value;
                        break;
                    case "Twitter_Search_Query":
                        res.Value = options.Twitter_Search_Query;
                        BgResources.Twitter_Search_Query = res.Value;
                        break;
                }
                BlogResourceRepository.Update(res);
            }
            BlogResourceRepository.UnitOfWork.Commit();
        }        
    }
}
