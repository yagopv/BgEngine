using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BgEngine.Domain.Types;
using BgEngine.Domain.EntityModel;
using System.Security.Cryptography;

namespace BgEngine.Domain.Factories
{
    public static class SubscriptionFactory
    {
        private const int TOKEN_SIZE_IN_BYTES = 16;

        public static Subscription CreateSubscription(string name, string email, bool isconfirmed)
        {
            var subscription = new Subscription();
            subscription.SubscriberName = name;
            subscription.SubscriberEmail = email;
            subscription.IsConfirmed = isconfirmed;
            subscription.ConfirmationToken = GenerateToken();
            subscription.DateCreated = DateTime.UtcNow;
            subscription.SubscriptionType = SubscriptionType.Newsletter;            
            return subscription;
        }

        public static Subscription CreateSubscription(string name, string email, bool isconfirmed, Post post)
        {
            var subscription = new Subscription();
            subscription.SubscriberName = name;
            subscription.SubscriberEmail = email;
            subscription.IsConfirmed = isconfirmed;
            subscription.ConfirmationToken = GenerateToken();
            subscription.DateCreated = DateTime.UtcNow;
            subscription.SubscriptionType = SubscriptionType.Comments;
            subscription.AddPost(post);
            return subscription;
        }

        private static string GenerateToken()
        {
            byte[] tokenBytes = new byte[TOKEN_SIZE_IN_BYTES];
            using (RNGCryptoServiceProvider prng = new RNGCryptoServiceProvider())
            {
                prng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
    }
}
