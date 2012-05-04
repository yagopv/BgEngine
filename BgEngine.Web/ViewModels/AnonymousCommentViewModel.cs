using System.ComponentModel.DataAnnotations;

namespace BgEngine.Web.ViewModels
{
    public class AnonymousCommentViewModel
    {
        /// <summary>
        /// The Username
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Username")]
        public string Username { get; set; }
        /// <summary>
        /// The AnonymousUser Email
        /// </summary>
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "InvalidMail")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Email")]
        public string Email { get; set; }
        /// <summary>
        /// The AnonymousUser Web
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Web")]
        public string Web { get; set; }
    }
}