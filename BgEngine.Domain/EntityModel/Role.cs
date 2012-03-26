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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Represent different roles the User can play
    /// </summary>
    public class Role
    {       
        /// <summary>
        /// Identity for Role
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Role_RoleId")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Name of the Role
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Role_RoleId", Prompt="Role_RoleName_Prompt")]
        public string RoleName { get; set; }

        /// <summary>
        /// Navigation property. Users in the Role
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Description for the Role
        /// </summary>
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Role_RoleDescription", Prompt = "Role_RoleDescription_Prompt")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}