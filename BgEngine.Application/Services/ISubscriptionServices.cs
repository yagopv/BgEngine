using BgEngine.Application.DTO;
using BgEngine.Domain.Types;
using BgEngine.Domain.EntityModel;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Interface for SubscriptionServices
    /// </summary>
    public interface ISubscriptionServices : IService<Subscription>
    {		
        string SubscribeToNewsletter(SubscriptionDTO subscription);
        void ConfirmSubscription(string token, string email, SubscriptionType type);
        void DeleteSubscription(string subscriberemail, SubscriptionType type);
        void Unsubscribe(string email);
	}
}