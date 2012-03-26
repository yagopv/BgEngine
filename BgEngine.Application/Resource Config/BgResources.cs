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
using BgEngine.Application.DTO;

namespace BgEngine.Application.ResourceConfiguration
{
    /// <summary>
    /// Resources that can be modified by the user
    /// </summary>
    public static class BgResources
    {
        // Email Resources
        public static string Email_UserName { get; set; }
        public static string Email_Password { get; set; }
        public static string Email_SmtpPort { get; set; }
        public static string Email_Server { get; set; }
        public static bool   Email_SSL { get; set; }

        //Pagination Resources
        public static string Pager_PostPerPage { get; set; }
        public static string Pager_CommentsPerPage {get; set;}
        public static string Pager_CategoriesPerPage {get; set;}
        public static string Pager_RolesPerPage { get; set; }
        public static string Pager_TagsPerPage { get; set; }
        public static string Pager_UsersPerPage { get; set; }
        public static string Pager_SearchImagesPerPage { get; set; }
        public static string Pager_SearchVideosPerPage { get; set; }
        public static string Pager_HomeIndexPostsPerPage { get; set; }

        //Message Resources
        public static string Messages_Copyright { get; set; }
        public static string Messages_SiteTitle { get; set; }
        public static string Messages_SiteUrl { get; set; }

        //Theme Resources
        public static string Themes_DarkBackGround { get; set; }
        public static string Themes_DarkHeader { get; set; }
        public static string Themes_Default { get; set; }

        //Security Resources
        public static string Security_AdminRole {get; set;}
        public static string Security_PremiumRole { get; set; }

        //Directories
        public static string Folders_TempData { get; set; }
        public static string Folders_Logo { get; set; }
        public static string Folders_NoImage { get; set; }

        //Analytics
        public static string Analytics_GoogleAnalyticsCode { get; set; }

        //Recaptcha
        public static string Recaptcha_PrivateKeyHttp { get; set; }
        public static string Recaptcha_PrivateKeyLocalhost { get; set; }
        public static string Recaptcha_PublicKeyHttp { get; set; }
        public static string Recaptcha_PublicKeyLocalhost { get; set; }

        // Media
        public static string Media_ThumbnailHeight { get; set; }
        public static string Media_ThumbnailWidth { get; set; }
        public static string Media_VideoHeight { get; set; }
        public static string Media_VideoWidth { get; set; }

        // Twitter
        public static string Twitter_User { get; set; }
        public static string Twitter_Search_Query { get; set; }

        /// <summary>
        /// Generates a DTO object to be transfered to the Web Layer
        /// </summary>
        /// <returns>The DTO object</returns>
        public static ConfigOptionsDTO GenerateTransferDTO()
        {
            return new ConfigOptionsDTO
            {
                Email_UserName = Email_UserName,
                Email_Password = Email_Password,
                Email_SmtpPort = Email_SmtpPort,
                Email_Server = Email_Server,
                Email_SSL = Email_SSL,
                Pager_PostPerPage = Pager_PostPerPage,
                Pager_CommentsPerPage = Pager_CommentsPerPage,
                Pager_CategoriesPerPage = Pager_CategoriesPerPage,
                Pager_RolesPerPage = Pager_RolesPerPage,
                Pager_TagsPerPage = Pager_TagsPerPage,
                Pager_UsersPerPage = Pager_UsersPerPage,
                Pager_SearchImagesPerPage = Pager_SearchImagesPerPage,
                Pager_SearchVideosPerPage = Pager_SearchVideosPerPage,
                Pager_HomeIndexPostsPerPage = Pager_HomeIndexPostsPerPage,
                Messages_Copyright = Messages_Copyright, 
                Messages_SiteTitle = Messages_SiteTitle,
                Messages_SiteUrl = Messages_SiteUrl,
                Themes_Default = Themes_Default,
                Security_AdminRole = Security_AdminRole,
                Security_PremiumRole = Security_PremiumRole,
                Analytics_GoogleAnalyticsCode = Analytics_GoogleAnalyticsCode,
                Recaptcha_PrivateKeyHttp = Recaptcha_PrivateKeyHttp,
                Recaptcha_PublicKeyHttp = Recaptcha_PublicKeyHttp,
                Twitter_User = Twitter_User,
                Twitter_Search_Query = Twitter_Search_Query
            };
        }

        /// <summary>
        /// This method receive all resources that can be configurated by the user and initialize the static class
        /// </summary>
        /// <param name="resources"></param>
        public static void InitialLoad(IEnumerable<BlogResource> resources)
        {
            foreach (BlogResource res in resources)
            {
                switch (res.Name)
                {
                    case "Admin_Role":
                        BgResources.Security_AdminRole = res.Value;
                        break;
                    case "Categories_Number_of_Categories_per_Page":
                        BgResources.Pager_CategoriesPerPage = res.Value;
                        break;
                    case "Comments_Number_of_Comments_per_Page":
                        BgResources.Pager_CommentsPerPage = res.Value;
                        break;
                    case "Copyright":
                        BgResources.Messages_Copyright = res.Value;
                        break;
                    case "Dark_Background_Themes":
                        BgResources.Themes_DarkBackGround = res.Value;
                        break;
                    case "Dark_Header_Themes":
                        BgResources.Themes_DarkHeader = res.Value;
                        break;
                    case "Default_Theme":
                        BgResources.Themes_Default = res.Value;
                        break;
                    case "Directories_Temp_Data":
                        BgResources.Folders_TempData = res.Value;
                        break;
                    case "Email_Password":
                        BgResources.Email_Password = res.Value;
                        break;
                    case "Email_UserName":
                        BgResources.Email_UserName = res.Value;
                        break;
                    case "Google_Analytics_Track_Code":
                        BgResources.Analytics_GoogleAnalyticsCode = res.Value;
                        break;
                    case "Index_Number_of_Posts":
                        BgResources.Pager_PostPerPage = res.Value;
                        break;
                    case "Logo":
                        BgResources.Folders_Logo = res.Value;
                        break;
                    case "No_Image":
                        BgResources.Folders_NoImage = res.Value;
                        break;
                    case "Posts_Number_of_Posts_per_Page":
                        BgResources.Pager_PostPerPage = res.Value;
                        break;
                    case "Premium_Role":
                        BgResources.Security_PremiumRole = res.Value;
                        break;
                    case "Recaptcha_Private_Key_Http":
                        BgResources.Recaptcha_PrivateKeyHttp = res.Value;
                        break;
                    case "Recaptcha_Private_Key_localhost":
                        BgResources.Recaptcha_PrivateKeyLocalhost = res.Value;
                        break;
                    case "Recaptcha_Public_Key_Http":
                        BgResources.Recaptcha_PublicKeyHttp = res.Value;
                        break;
                    case "Recaptcha_Public_Key_localhost":
                        BgResources.Recaptcha_PublicKeyLocalhost = res.Value;
                        break;
                    case "Roles_Number_of_Roles_per_Page":
                        BgResources.Pager_RolesPerPage = res.Value;
                        break;
                    case "SearchImages_Number_of_Images_per_Page":
                        BgResources.Pager_SearchImagesPerPage = res.Value;
                        break;
                    case "SearchVideos_Number_of_Videos_per_Page":
                        BgResources.Pager_SearchVideosPerPage = res.Value;
                        break;
                    case "SiteTitle":
                        BgResources.Messages_SiteTitle = res.Value;
                        break;
                    case "SiteUrl":
                        BgResources.Messages_SiteUrl = res.Value;
                        break;
                    case "Smtp_Port":
                        BgResources.Email_SmtpPort = res.Value;
                        break;
                    case "Smtp_Server":
                        BgResources.Email_Server = res.Value;
                        break;
                    case "Email_SSL":
                        BgResources.Email_SSL = bool.Parse(res.Value);
                        break;
                    case "Tags_Number_of_Tags_per_Page":
                        BgResources.Pager_TagsPerPage = res.Value;
                        break;
                    case "ThumbnailHeight":
                        BgResources.Media_ThumbnailHeight = res.Value;
                        break;
                    case "ThumbnailWidth":
                        BgResources.Media_ThumbnailWidth = res.Value;
                        break;
                    case "Users_Number_of_Users_per_Page":
                        BgResources.Pager_UsersPerPage = res.Value;
                        break;
                    case "Video_Container_Height":
                        BgResources.Media_VideoHeight = res.Value;
                        break;
                    case "Video_Container_Width":
                        BgResources.Media_VideoWidth = res.Value;
                        break;
                    case "Posts_HomeIndexPostsPerPage":
                        BgResources.Pager_HomeIndexPostsPerPage = res.Value;
                        break;
                    case "Twitter_User":
                        BgResources.Twitter_User = res.Value;
                        break;
                    case "Twitter_Search_Query":
                        BgResources.Twitter_Search_Query = res.Value;
                        break;
                }            
            }        
        }

        /// <summary>
        /// Update the options object
        /// </summary>
        /// <param name="options">The options DTO</param>
        public static void UpdateOptions(ConfigOptionsDTO options)
        {
            BgResources.Email_UserName = options.Email_UserName;
            BgResources.Email_SmtpPort = options.Email_SmtpPort;
            BgResources.Email_Server = options.Email_Server;
            BgResources.Email_Password = options.Email_Password;
            BgResources.Email_SSL = options.Email_SSL;
            BgResources.Analytics_GoogleAnalyticsCode = options.Analytics_GoogleAnalyticsCode;
            BgResources.Messages_Copyright = options.Messages_Copyright;
            BgResources.Messages_SiteTitle = options.Messages_SiteTitle;
            BgResources.Messages_SiteUrl = options.Messages_SiteUrl;
            BgResources.Pager_CategoriesPerPage = options.Pager_CategoriesPerPage;
            BgResources.Pager_CommentsPerPage = options.Pager_CommentsPerPage;
            BgResources.Pager_PostPerPage = options.Pager_PostPerPage;
            BgResources.Pager_RolesPerPage = options.Pager_RolesPerPage;
            BgResources.Pager_TagsPerPage = options.Pager_TagsPerPage;
            BgResources.Pager_UsersPerPage = options.Pager_UsersPerPage;
            BgResources.Pager_SearchImagesPerPage = options.Pager_SearchImagesPerPage;
            BgResources.Pager_SearchVideosPerPage = options.Pager_SearchVideosPerPage;
            BgResources.Recaptcha_PrivateKeyHttp = options.Recaptcha_PrivateKeyHttp;
            BgResources.Recaptcha_PublicKeyHttp = options.Recaptcha_PublicKeyHttp;
            BgResources.Security_PremiumRole = options.Security_PremiumRole;
            BgResources.Security_AdminRole = options.Security_AdminRole;
            BgResources.Pager_HomeIndexPostsPerPage = options.Pager_HomeIndexPostsPerPage;
            BgResources.Twitter_User = options.Twitter_User;
            BgResources.Twitter_Search_Query = options.Twitter_Search_Query;
            
        }
    }
}
