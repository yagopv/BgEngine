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
    /// Represents a type for the Posts grouped under this
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Category_CategoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the Category
        /// </summary>
        [StringLength(100)]
        [Display(ResourceType=typeof(Resources.AppMessages), Name="Name", Prompt="Name_Prompt")]
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Category
        /// </summary>
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Description", Prompt = "Description_Prompt")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Date the Category was created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The Posts associated with the Category
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        /// The Videos associated with the Category
        /// </summary>
        public virtual ICollection<Video> Videos { get; set; }

    }
}