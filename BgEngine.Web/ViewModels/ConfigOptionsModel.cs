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

using System.ComponentModel.DataAnnotations;

namespace BgEngine.Web.ViewModels
{
    public class ConfigOptionsModel
    {
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_UserName", Prompt="Config_UserNamePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "InvalidMail")]       
        public string Email_UserName { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_UserPassword", Prompt = "Config_UserPasswordPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Email_Password { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_EmailSmtpPort", Prompt = "Config_EmailSmtpPortPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Email_SmtpPort { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_EmailServer", Prompt = "Config_EmailServerPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Email_Server { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_SSL")]
        public bool Email_SSL {get; set;}

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_PostPerPage", Prompt = "Config_PostPerPagePrompt")]
        public string Pager_PostPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_CommentsPerPage", Prompt = "Config_CommentsPerPagePrompt")]
        public string Pager_CommentsPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_CategoriesPerPage", Prompt = "Config_CategoriesPerPagePrompt")]
        public string Pager_CategoriesPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_RolesPerPage", Prompt = "Config_RolesPerPagePrompt")]
        public string Pager_RolesPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_TagsPerPage", Prompt = "Config_TagsPerPagePrompt")]
        public string Pager_TagsPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_UsersPerPage", Prompt = "Config_UsersPerPagePrompt")]
        public string Pager_UsersPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_SearchImagesPerPage", Prompt = "Config_SearchImagesPerPagePrompt")]
        public string Pager_SearchImagesPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_SearchVideosPerPage", Prompt = "Config_SearchVideosPerPagePrompt")]
        public string Pager_SearchVideosPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Pager_HomeIndexPostsPerPage", Prompt = "Pager_HomeIndexPostsPerPagePrompt")]
        public string Pager_HomeIndexPostsPerPage { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_Copyright", Prompt = "Config_CopyrightPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Messages_Copyright { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_SiteTitle", Prompt = "Config_SiteTitlePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Messages_SiteTitle { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_SiteUrl", Prompt = "Config_SiteUrlPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Messages_SiteUrl { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_DefaultTheme", Prompt = "Config_DefaultThemePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Themes_Default { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_AdminRole", Prompt = "Config_AdminRolePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Security_AdminRole { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_PremiumRole", Prompt = "Config_PremiumRolePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Security_PremiumRole { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_GoogleAnalytics", Prompt = "Config_GoogleAnalyticsPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Analytics_GoogleAnalyticsCode { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_RecaptchaPrivate", Prompt = "Config_RecaptchaPrivatePrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Recaptcha_PrivateKeyHttp { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_RecaptchaPublic", Prompt = "Config_RecaptchaPublicPrompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Recaptcha_PublicKeyHttp { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_Akismet", Prompt = "Config_AkismetPrompt")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Akismet_API_key { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_Twitter_User", Prompt = "Config_Twitter_User_Prompt")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Twitter_User { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Config_Twitter_Search_Query", Prompt = "Config_Twitter_Search_Query_Prompt")]        
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Twitter_Search_Query { get; set; }
    }
}