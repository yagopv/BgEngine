using BgEngine.Application.DTO;
using BgEngine.Domain.Types;
using BgEngine.Domain.EntityModel;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Interface for SubscriptionServices
    /// </summary>
    public interface INewsletterServices : IService<Newsletter>
    {
        void CreateNewsletter(string name, string html);
        void DeleteNewsletterTask(NewsletterTask task);
        void UpdateNewsletterTask(NewsletterTask task);
    }
}