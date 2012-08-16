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
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

using BgEngine.Domain.Types;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Represent the logical info of an Video
    /// </summary>
	public class Video
	{
        /// <summary>
        /// Identity of the Video
        /// </summary>
		[Key]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Video_VideoId")]
		public int VideoId { get; set; }

        /// <summary>
        /// Name of the Video
        /// </summary>
		[Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
		[StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Name_Prompt")]
		public string Name { get; set; }

        /// <summary>
        /// Description of the Video
        /// </summary>
		[StringLength(300, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Description", Prompt = "Description_Prompt")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		/// <summary>
        /// The identity of the related Category
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]        
        public int CategoryId { get; set; }

        /// <summary>
        /// Navigation property representing the related Category
        /// </summary>
        public virtual Category Category { get; set; }
		
        /// <summary>
        /// Path of the video. Indicates the Url of the Video
        /// When the path is informed is logical to include the Type of the Video
        /// </summary>
		[StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Video_Path", Prompt = "VideoPath_Prompt")]
		public string Path { get; set; }

        /// <summary>
        /// For setting the Video with an iframe or flash object
        /// </summary>
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		[StringLength(500, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Video_Html", Prompt = "VideoHtml_Prompt")]
		public string Html { get; set; }

        /// <summary>
        /// The date the Video was created
        /// </summary>
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
		public DateTime DateCreated { get; set; }

        /// <summary>
        /// If any User can see the Video
        /// </summary>
		[Required]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Video_IsPublic")]
		public bool IsPublic { get; set; }

        /// <summary>
        /// The Type of the Video. Can be Flash, HTML5, ...
        /// </summary>
		[Required]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Video_Type")]
		string Type { get; set; }

        /// <summary>
        /// Navigation property with related Tags
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Get or set the VideoType
        /// </summary>
		public VideoType VideoType
		{
			get
			{
				VideoType result;
				if (Enum.TryParse<VideoType>(Type, out result))
				{
					return result;
				}
				else
				{
					return VideoType.None;
				}
			}
			set
			{
				Type = Enum.GetName(typeof(VideoType), value);
			}
		}
	}
}