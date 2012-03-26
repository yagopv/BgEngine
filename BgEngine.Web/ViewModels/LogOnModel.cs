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
    public class LogOnModel
    {
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User", Prompt = "User_Prompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [UIHint("UserData_UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Password", Prompt="Password_Prompt")]
        [UIHint("UserData_Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Remember_Me")]
        public bool RememberMe { get; set; }

    }
}