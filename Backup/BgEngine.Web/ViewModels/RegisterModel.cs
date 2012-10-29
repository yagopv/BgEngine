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
using System.Web.Mvc;

namespace BgEngine.Web.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_UserId", Prompt = "User_UserName_Prompt")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Mail", Prompt = "User_Mail_Prompt")]
        [RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "InvalidMail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "PasswordLenght",MinimumLength=6)]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Password", Prompt = "User_Password_Prompt")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Confirm_Password", Prompt = "User_Confirm_Password_Prompt")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_FirstName", Prompt = "User_FirstName_Prompt")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_LastName", Prompt = "User_LastName_Prompt")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings=false, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_TimeZone", Prompt = "User_TimeZone_Prompt")]
        [StringLength(100)]
        public string TimeZone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Culture", Prompt = "User_Culture_Prompt")]
        [StringLength(100)]
        public string Culture { get; set; }
    }
}