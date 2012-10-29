using System.Linq;
using System.Web.Mvc;

using BgEngine.Application.Services;
using BgEngine.Domain.EntityModel;
using BgEngine.Web.Mailers;
using BgEngine.Web.Helpers;
using System.Net.Mail;
using System.Threading.Tasks;
using BgEngine.Application.ResourceConfiguration;
using System.Net;

namespace BgEngine.Controllers
{
    public class BackgroundWorkerController : AsyncController
    {
        INewsletterServices NewsletterServices;
        IUserMailer UserMailer;

        public BackgroundWorkerController(INewsletterServices newsletterservices)
        {
            this.NewsletterServices = newsletterservices;            
            this.UserMailer = new UserMailer();
        }

        [Authorize(Roles = "Admin")]
        public void SendNewsletterAsync(int id)
        {
			AsyncManager.OutstandingOperations.Increment();			
			Task.Factory.StartNew(() =>	{
				Newsletter newsletter = NewsletterServices.FindAllEntities(n => n.NewsletterId == id, null, "NewsletterTasks").FirstOrDefault();
                NewsletterInProcess(newsletter, true);                
                NetworkCredential credentials = new NetworkCredential(BgResources.Email_UserName, BgResources.Email_Password);
                SmtpClient client = new SmtpClient(BgResources.Email_Server, 25);
                client.EnableSsl = BgResources.Email_SSL;
                client.Credentials = credentials;                
				foreach (var task in newsletter.NewsletterTasks.ToList())
				{                    
					try
					{
                        MailMessage message = new MailMessage(BgResources.Email_UserName, task.Subscription.SubscriberEmail, task.Newsletter.Name, task.Newsletter.Html);
                        message.IsBodyHtml = true;
                        client.Send(message);						
						NewsletterServices.DeleteNewsletterTask(task);
					}
					catch (SmtpException ex)
					{
						task.Log = ex.Message;
						NewsletterServices.UpdateNewsletterTask(task);
					}
				}
                NewsletterInProcess(newsletter, false);
				AsyncManager.OutstandingOperations.Decrement();
			});                            		
		}
		
		public ActionResult SendNewsletterCompleted()
		{
            return RedirectToAction("Index", "Newsletter");
		}

        private void NewsletterInProcess(Newsletter newsletter, bool state)
        {
            newsletter.InProcess = state;
            NewsletterServices.SaveEntity(newsletter);
        }
    }			
}
