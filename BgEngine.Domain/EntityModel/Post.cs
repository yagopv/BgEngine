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
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Post entity. Store the data of an article
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The identity of a Post
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_PostId")]
        public int PostId { get; set; }

        /// <summary>
        /// Title of the Post
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_Title", Prompt = "Post_Title_Prompt")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the Post
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(1000, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_Description", Prompt = "Post_Description_Prompt")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        /// <summary>
        /// Post´s contain. Allow HTML for using with rich text editors
        /// </summary>
        [AllowHtml]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_Text", Prompt = "Post_Text_Prompt")]
        public string Text { get; set; }

        /// <summary>
        /// The date the Post was created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }
        
        /// <summary>
        /// The date the Post was updated
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateUpdated", Prompt = "DateUpdated_Prompt")]
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Code for accesing the post by url
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_Code", Prompt = "Post_Code_Prompt")]
        public string Code {get; set;}

        /// <summary>
        /// If any user can access the Post info
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsPublic")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Indicates if the Post can be retrieved for showing in main view
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsHomePost")]
        public bool IsHomePost { get; set; }

        /// <summary>
        /// If true this Post Text is the data in About Me view
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsAboutMe")]
        public bool IsAboutMe { get; set; }

        /// <summary>
        /// If true the Post allow comments from not authenticathed Users
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_AllowAnonymousComments")]
        public bool AllowAnonymousComments { get; set; }

        /// <summary>
        /// If true, the Post don´t allow Comments
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_IsPostCommentsClosed")]
        public bool IsPostCommentsClosed { get; set; }

        /// <summary>
        /// Number of visits of teh Post
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Post_Visits")]
        public int Visits { get; set; }

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
        /// Identity of the related Image
        /// </summary>        
        public int? ImageId { get; set; }

        /// <summary>
        /// Navigation property representing the info for the related Image
        /// </summary>
        public virtual Image Image { get; set; }

        /// <summary>
        /// Identity of the related User
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]        
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation Property representing the User who wrote the Post
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Navigation property with related Comments
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Navigation property with related Tags
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Navigation Property with all the Ratings of the Post
        /// </summary>
        public virtual ICollection<Rating> Ratings { get; set; }

        /// <summary>
        /// Increase visit counter each time a Post is visited
        /// </summary>
        public void IncreaseVisitCounter()
        {
            this.Visits++;
        }
    }
}