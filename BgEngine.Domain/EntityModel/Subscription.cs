using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BgEngine.Domain.Types;

namespace BgEngine.Domain.EntityModel
{
    public class Subscription
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Subscription()
        {
            Posts = new HashSet<Post>();
            NewsletterTasks = new HashSet<NewsletterTask>();
        }

        /// <summary>
        /// Subscription identity
        /// </summary>
        [Key]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Subscriber name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Subscription_Subscribername_Prompt")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string SubscriberName { get; set; }

        /// <summary>
        /// Subscriber Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Mail", Prompt = "Subscriber_Email_Prompt")]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "InvalidMail")]
        public string SubscriberEmail { get; set; }

        /// <summary>
        /// If the Subscription is confirmed
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "IsConfirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// A confirmation token for creating the Subscription
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string ConfirmationToken { get; set; }

        /// <summary>
        /// The date the Video was created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Navigation property with related Posts
        /// This is only filled when the Subscription is for Comments
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        /// The Subscription Type
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Subscription_Type")]
        string Type { get; set; }

        /// <summary>
        /// Get or set the Subscription Type
        /// </summary>
        public SubscriptionType SubscriptionType
        {
            get
            {
                SubscriptionType result;
                if (Enum.TryParse<SubscriptionType>(Type, out result))
                {
                    return result;
                }
                else
                {
                    return SubscriptionType.None;
                }
            }
            set
            {
                Type = Enum.GetName(typeof(SubscriptionType), value);
            }
        }

        /// <summary>
        /// The related NewsletterTasks
        /// </summary>        
        public virtual ICollection<NewsletterTask> NewsletterTasks { get; set; }

        /// <summary>
        /// Add a Post to the Posts Collection
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            this.Posts.Add(post);
        }
        /// <summary>
        /// Remove a Post from the Post Collection
        /// </summary>
        /// <param name="post"></param>
        public void DeletePost(Post post)
        {
            this.Posts.Remove(post);
        }

        /// <summary>
        /// Add a NewsletterTask to the NewsletterTask Collection
        /// </summary>
        /// <param name="newsletter"></param>
        public void AddNewsletterTask(NewsletterTask newslettertask)
        {
            this.NewsletterTasks.Add(newslettertask);
        }
        /// <summary>
        /// Remove NewsletterTask from the NewsletterTasks Collection
        /// </summary>
        /// <param name="newslettertask"></param>
        public void DeleteNewsletterTask(NewsletterTask newslettertask)
        {
            this.NewsletterTasks.Remove(newslettertask);
        }
    }

}
