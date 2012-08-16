using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BgEngine.Domain.EntityModel
{
    public class NewsletterTask
    {
        /// <summary>
        /// The NewsletterTask identity 
        /// </summary>
        [Key]        
        public int NewsletterTaskId { get; set; }	
		
        /// <summary>
        /// The Subscription identity 
        /// </summary>		
		public int SubscriptionId { get; set; }
		
        /// <summary>
        /// The Subscription navigation property
        /// </summary>				
		public virtual Subscription Subscription { get; set; }
		
        /// <summary>
        /// The Newsletter identity 
        /// </summary>				
		public int  NewsletterId { get; set; }
		
        /// <summary>
        /// The Newsletter navigation property
        /// </summary>						
		public virtual Newsletter Newsletter { get; set; }
				
        /// <summary>
        /// The log for the Task
        /// </summary>	
		[StringLength(500, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]				
		public string Log {get; set;}

        public void SetSubscription(Subscription subscription)
        {
            this.SubscriptionId = subscription.SubscriptionId;
            this.Subscription = subscription;
        }

        public void SetNewsletter(Newsletter newsletter)
        {
            this.NewsletterId = newsletter.NewsletterId;
            this.Newsletter = newsletter;
        }		
	
	}
}