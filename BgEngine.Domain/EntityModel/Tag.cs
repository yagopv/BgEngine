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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Words for tag the different Posts
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Tag_TagId")]
        public int TagId { get; set; }

        /// <summary>
        /// Name of Tag
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Tag_TagId", Prompt = "Tag_TagName_Prompt")]
        public string TagName { get; set; }

        /// <summary>
        /// Description for the Tag
        /// </summary>
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Tag_TagDescription", Prompt = "Tag_TagDescription_Prompt")]
        [DataType(DataType.MultilineText)]
        public string TagDescription { get; set; }

        /// <summary>
        /// Navigation property for the Posts related with  this Tag
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        /// Navigation property for the Videos related with  this Tag
        /// </summary>
        public virtual ICollection<Video> Videos { get; set; }

    }
}