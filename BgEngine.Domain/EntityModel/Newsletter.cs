using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BgEngine.Domain.EntityModel
{
    public class Newsletter
    {
	
        /// <summary>
        /// ctor
        /// </summary>
        public Newsletter()
        {            
			NewsletterTasks = new HashSet<NewsletterTask>();
        }
		
        /// <summary>
        /// The Newsletter identity 
        /// </summary>
        [Key]        
        public int NewsletterId { get; set; }

        /// <summary>
        /// Name of the Newsletter
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "Name", Prompt = "Name_Prompt")]
        public string Name { get; set; }

        /// <summary>
        /// The date where the Newsletter was created
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "DateCreated", Prompt = "DateCreated_Prompt")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Html field
        /// </summary>  		
        public string Html { get; set; }

        /// <summary>
        /// If the newsletter is in process
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "InProcess", Prompt = "InProcess_Prompt")]
        public bool InProcess { get; set; }

        /// <summary>
        /// If any of the related tasks has errors
        /// </summary>
        public bool HasErrorTasks {
            get
            {
                return (this.NewsletterTasks.Any(nt => nt.Log != null) ? true : false);
            } 
        }

        /// <summary>
        /// If all the related Tasks are Completed
        /// </summary>
		public bool HasPendingTasks { 
			get {
                return (this.NewsletterTasks.Any() ? true : false);
			} 
		}
		
        /// <summary>
        /// The related tasks
        /// </summary>        
        public virtual ICollection<NewsletterTask> NewsletterTasks { get; set; }

        /// <summary>
        /// Add task
        /// </summary>
        /// <param name="newslettertask">The NewsletterTask</param>
        public void AddNewsletterTask(NewsletterTask newslettertask)
        {
            this.NewsletterTasks.Add(newslettertask);
        }

        /// <summary>
        /// Remove task
        /// </summary>
        /// <param name="newslettertask">The NewsletterTask</param>
        public void DeleteNewsletterTask(NewsletterTask newslettertask)
        {
            this.NewsletterTasks.Remove(newslettertask);
        }

    }
}
