using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.NLayer.Domain.EntityModel
{
	public class RelatedComment
	{
		[Key]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_CommentId")]
		public int RelatedCommentId { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
		[StringLength(1000, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
		[Display(ResourceType = typeof(Resources.AppMessages), Name = "Comment_Message", Prompt = "Comment_Message_Prompt")]
		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Message { get; set; }

		[Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
		public DateTime DateCreated { get; set; }

		[Display(ResourceType = typeof(Resources.AppMessages), Name = "DateUpdated", Prompt = "DateUpdated_Prompt")]
		public DateTime? DateUpdated { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
	}
}