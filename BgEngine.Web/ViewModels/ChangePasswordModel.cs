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

using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Web.ViewModels
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "PasswordLenght", MinimumLength = 6)]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Password", Prompt = "User_Password_Prompt")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Confirm_Password", Prompt = "User_Confirm_Password_Prompt")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ConfirmationToken {get; set; }

        [Required]
        public string UserName { get; set; }
    }
}