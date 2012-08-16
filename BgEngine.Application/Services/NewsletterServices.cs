using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Helpers;
using System.Net.Mail;

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Application.Services
{
    public class NewsletterServices: Service<Newsletter>,  INewsletterServices
    {
		INewsletterRepository NewsletterRepository;
        IRepository<Subscription> SubscriptionRepository;
        /// <summary>
        /// ctor
        /// </summary>
        public NewsletterServices(INewsletterRepository newsletterrepository): base(newsletterrepository)
        {
            this.NewsletterRepository = newsletterrepository;            
        }

        public NewsletterServices(INewsletterRepository newsletterrepository, IRepository<Subscription> subscriptionrepository)
            : base(newsletterrepository)
        {
            this.NewsletterRepository = newsletterrepository;    
			this.SubscriptionRepository = subscriptionrepository;
        }
		
		public void  CreateNewsletter(string name, string html)
		{
			IEnumerable<Subscription> subscriptions = SubscriptionRepository.Get(s => s.IsConfirmed == true,null,null);
			Newsletter newsletter = new Newsletter();
			newsletter.Name = name;
			newsletter.DateCreated = DateTime.UtcNow;
			newsletter.Html = html;
            newsletter.InProcess = false;
			foreach (Subscription s in subscriptions)
			{
				NewsletterTask newslettertask = new NewsletterTask();
				newslettertask.SetNewsletter(newsletter);
				newslettertask.SetSubscription(s);
				newsletter.AddNewsletterTask(newslettertask);
			}
            NewsletterRepository.Insert(newsletter);
			NewsletterRepository.UnitOfWork.Commit();
		}

        public void DeleteNewsletterTask(NewsletterTask task)
        {
            NewsletterRepository.DeleteNewsletterTask(task);
            NewsletterRepository.UnitOfWork.Commit();
        }

        public void UpdateNewsletterTask(NewsletterTask task)
        {
            NewsletterRepository.UpdateNewsletterTask(task);
            NewsletterRepository.UnitOfWork.Commit();
        }

        public override void DeleteEntity(object id)
        {
            Newsletter newsletter = NewsletterRepository.GetByID(id);
            foreach (var task in newsletter.NewsletterTasks.ToList())
            {                
                NewsletterRepository.DeleteNewsletterTask(task);
            }            
            base.DeleteEntity(newsletter);
        }
    }
}
