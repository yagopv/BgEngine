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
    /// Represents messages. This messages are written by users 
    /// and references Posts
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_CommentId")]
        public int CommentId { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(1000, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_Message", Prompt = "Comment_Message_Prompt")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Message { get; set; }

        /// <summary>
        /// A Comment can have n related Comments
        /// This property identify if this Comment is root or related to another one
        /// </summary>
        public bool isRelatedComment { get; set; }

        /// <summary>
        /// The date the Comment was created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the Comment was updated last time
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateUpdated", Prompt = "DateUpdated_Prompt")]
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Identity of the related Post
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]        
        public int PostId { get; set; }

        /// <summary>
        /// If the Comment is Spam true, in the another case false
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_IsSpam", Prompt = "Comment_IsSpam_Prompt")]
        public bool IsSpam { get; set; }
        
        /// <summary>
        /// The Ip Adress of the comment
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_Ip", Prompt = "Comment_Ip_Prompt")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string Ip { get; set; }
        
        /// <summary>
        /// The User agent used to make the comment
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_UserAgent", Prompt = "Comment_UserAgent_Prompt")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string UserAgent { get; set; }

        /// <summary>
        /// Navigation property representing the related Post
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Navigation property representing the related User who writes the Post
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// The anonymous user will be set when comments anonymous
        /// </summary>
        public AnonymousUser AnonymousUser { get; set; }

        /// <summary>
        /// The collection of related Comments if any
        /// </summary>
        public virtual ICollection<Comment> RelatedComments { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public Comment()
        {
            RelatedComments = new HashSet<Comment>();
        }

        /// <summary>
        /// Removes a related Comment from the Colecction 
        /// </summary>
        /// <param name="relatedComment"></param>
        public void DeleteFromRelatedCollection(Comment relatedComment)
        {
            this.RelatedComments.Remove(relatedComment);
        }

        /// <summary>
        /// Adds a related Comment
        /// </summary>
        /// <param name="comment"></param>
        public void AddRelatedComment(Comment comment)
        {
            this.RelatedComments.Add(comment);
        }
    }
}