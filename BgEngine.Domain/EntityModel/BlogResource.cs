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

namespace BgEngine.Domain.EntityModel
{
    public class BlogResource
    {
        /// <summary>
        /// Resource Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "BlogResource_BlogResourceId")]
        public int BlogResourceId { get; set; }

        /// <summary>
        /// The name of the resource to be configurated
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Name_Prompt")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the resource to be configurated
        /// </summary>        
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Value", Prompt = "Value_Prompt")]
        public string Value { get; set; }
    }
}
