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
    /// Entity Album represents a collection of images.
    /// Is useful for store the images in folders
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Album_AlbumId")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Album Name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Name_Prompt")]
        public string Name { get; set; }

        /// <summary>
        /// Short description of the Album
        /// </summary>
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Description", Prompt = "Description_Prompt")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// If the Album is public can be accesed for all the people. If not, only premium roles can access to the album´s photos
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsPublic")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// The date when the Album was created
        /// </summary>        
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Last time the Album was updated
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateUpdated", Prompt = "DateUpdated_Prompt")]
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// The Image collection associatted to the Album object
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }
    }
}