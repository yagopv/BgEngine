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

namespace BgEngine.Application.DTO
{
    /// <summary>
    /// Data tranfer object for Stats retrieval
    /// </summary>
    public class ConfigOptionsDTO
    {
        public string Email_UserName { get; set; }
        public string Email_Password { get; set; }
        public string Email_SmtpPort { get; set; }
        public string Email_Server { get; set; }
        public bool   Email_SSL { get; set; }
        public string Pager_PostPerPage { get; set; }
        public string Pager_CommentsPerPage { get; set; }
        public string Pager_CategoriesPerPage { get; set; }
        public string Pager_RolesPerPage { get; set; }
        public string Pager_TagsPerPage { get; set; }
        public string Pager_UsersPerPage { get; set; }
        public string Pager_SearchImagesPerPage { get; set; }
        public string Pager_SearchVideosPerPage { get; set; }
        public string Pager_HomeIndexPostsPerPage { get; set; }
        public string Messages_Copyright { get; set; }
        public string Messages_SiteTitle { get; set; }
        public string Messages_SiteUrl { get; set; }
        public string Themes_Default { get; set; }
        public string Security_AdminRole { get; set; }
        public string Security_PremiumRole { get; set; }
        public string Analytics_GoogleAnalyticsCode { get; set; }
        public string Recaptcha_PrivateKeyHttp { get; set; }
        public string Recaptcha_PublicKeyHttp { get; set; }
        public string Akismet_API_key { get; set; }
        public string Twitter_User { get; set; }
        public string Twitter_Search_Query { get; set; }
    }
}
