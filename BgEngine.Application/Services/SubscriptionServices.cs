using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Application.DTO;
using BgEngine.Domain.EntityModel;
using BgEngine.Domain.Types;
using BgEngine.Domain.Factories;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Application operations with Subscriptions
    /// </summary>
    public class SubscriptionServices:  Service<Subscription>, ISubscriptionServices
    {
		IRepository<Subscription> SubscriptionRepository;
        INewsletterRepository NewsletterRepository;
        /// <summary>
        /// ctor
        /// </summary>
        public SubscriptionServices(IRepository<Subscription> subscriptionrepository, INewsletterRepository newsletterrepository) : base(subscriptionrepository)
        {
            this.SubscriptionRepository = subscriptionrepository;
            this.NewsletterRepository = newsletterrepository;
        }

		public string  SubscribeToNewsletter(SubscriptionDTO subscriptionDTO)
		{
            if (SubscriptionRepository.Get(s => s.SubscriberEmail == subscriptionDTO.SubscriberEmail).Any(s => s.SubscriptionType == SubscriptionType.Newsletter))
            {
                throw new ApplicationValidationErrorsException(new List<string> { Resources.AppMessages.Subscription_Exists });
            }
            Subscription subscription = SubscriptionFactory.CreateSubscription(subscriptionDTO.SubscriberName, subscriptionDTO.SubscriberEmail, subscriptionDTO.IsConfirmed);
            SubscriptionRepository.Insert(subscription);
            SubscriptionRepository.UnitOfWork.Commit();
            return subscription.ConfirmationToken;            
		}

        public void ConfirmSubscription(string token, string email, SubscriptionType type)
        {
            Subscription subscription = SubscriptionRepository.Get(s => s.SubscriberEmail == email && s.ConfirmationToken == token).FirstOrDefault(s => s.SubscriptionType == type);
            if (subscription != null)
            {
                subscription.IsConfirmed = true;
                SubscriptionRepository.Update(subscription);
                SubscriptionRepository.UnitOfWork.Commit();
            }
            else
            {
                throw new ApplicationValidationErrorsException(new List<string> { Resources.AppMessages.ConfirmSubscription_NotExists });
            }
        }

        public void DeleteSubscription(string subscriberemail, SubscriptionType type)
        {
            Subscription subscription = SubscriptionRepository.Get(s => s.SubscriberEmail == subscriberemail).FirstOrDefault(s => s.SubscriptionType == type);
            if (subscription != null)
            {
                foreach (NewsletterTask task in subscription.NewsletterTasks.ToList())
                {
                    NewsletterRepository.DeleteNewsletterTask(task);
                }
                SubscriptionRepository.Delete(subscription);
                SubscriptionRepository.UnitOfWork.Commit();
            }
        }

        public override void SaveEntity(Subscription entity)
        {
            if (SubscriptionRepository.Get(s => s.SubscriberEmail == entity.SubscriberEmail && s.SubscriptionId != entity.SubscriptionId).Any())
            {
                throw new ApplicationValidationErrorsException(new List<string> { Resources.AppMessages.Subscription_Exists });
            }
            base.SaveEntity(entity);
        }


        public void Unsubscribe(string email)
        {
            Subscription subscription = SubscriptionRepository.Get(s => s.SubscriberEmail == email, null, null).FirstOrDefault();
            if (subscription == null)
            {
                throw new ApplicationValidationErrorsException(new List<string> { Resources.AppMessages.Unsubscribe_NotExist });
            }
            foreach (NewsletterTask task in subscription.NewsletterTasks.ToList())
            {
                NewsletterRepository.DeleteNewsletterTask(task);                
            }            
            SubscriptionRepository.Delete(subscription);
            SubscriptionRepository.UnitOfWork.Commit();
        }
    }
}