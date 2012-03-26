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
    /// This entity represents the logical data of a physical image stored in Content folder
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Identity of the Image
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Image_ImageId")]
        public int ImageId { get; set; }

        /// <summary>
        /// Name of the Image
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Name_Prompt")]
        public string Name { get; set; }

        /// <summary>
        /// The file name
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]        
        public string FileName { get; set; }

        /// <summary>
        /// Description of the Image
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Description", Prompt = "Description_Prompt")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// The Path in Content/Images folder
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Path { get; set; }

        /// <summary>
        /// The path of the thumbnail if any in Content/Images/Thumbnails
        /// </summary>
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// If the Image is public and can be accesed for any User
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsPublic")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// The date the Image was Created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The identity of the Album the Image is related to
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Navigation property representing the Album container
        /// </summary>
        public virtual Album Album { get; set; }

        /// <summary>
        /// Navigation property representing the Posts the Image is related to
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
        
    }
}