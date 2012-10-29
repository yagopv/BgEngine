using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

using Mvc.Mailer;
using AutoMapper;

using BgEngine.Application.Services;
using BgEngine.Web.ViewModels;
using BgEngine.Application.DTO;
using BgEngine.Application;
using BgEngine.Web.Mailers;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Domain.Types;
using BgEngine.Security.Services;
using BgEngine.Filters;
using BgEngine.Domain.EntityModel;
using Microsoft.Web.Helpers;


namespace BgEngine.Controllers
{
	public class SubscriptionController : BaseController
	{
		ISubscriptionServices SubscriptionServices;
		IUserMailer UserMailer;

		public SubscriptionController(ISubscriptionServices subscriptionservices )
		{
			this.UserMailer = new UserMailer();
			this.SubscriptionServices = subscriptionservices;
		}
		/// <summary>
		/// Show all Subscriptions
		/// </summary>
		/// <returns>List of Categories</returns>     
		[Authorize(Roles = "Admin")]
		[EnableCompression]
		public ViewResult Index(int? page, string sort, string sortdir)
		{
			ViewBag.RowsPerPage = BgResources.Pager_CategoriesPerPage;
			ViewBag.TotalCategories = SubscriptionServices.TotalNumberOfEntity();
			var pageIndex = page ?? 0;
			bool dir;
			if (sortdir == null)
			{
				dir = false;
			}
			else
			{
				dir = sortdir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? true : false;
			}
			if (sort == null)
			{
				return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, false));
			}
			else
			{
				switch (sort.ToLower())
				{
					case "subscribername":
						return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.SubscriberName, dir));
					case "subscriberemail":
						return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.SubscriberEmail, dir));
					case "isconfirmed":
						return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.IsConfirmed, dir));
					case "datecreated":
						return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, false));
					default:
						return View(SubscriptionServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_CategoriesPerPage), s => s.DateCreated, false));
				}
			}
		}
		/// <summary>
		/// Show Subscription details
		/// </summary>
		/// <param name="id">The Identity of the Subscription</param>
		/// <returns>The View for check Subscription details/returns>     
		[Authorize(Roles = "Admin")]
		[EnableCompression]
		public ViewResult Details(int id)
		{
			return View(SubscriptionServices.FindEntityByIdentity(id));
		}
		/// <summary>
		/// Renders a View for create a Subscription
		/// </summary>
		/// <returns>The View for check Subscription details/returns>  
		[Authorize(Roles = "Admin")]
		public ActionResult Create()
		{
			return View();
		}
		/// <summary>
		/// Create a new Subscription
		/// </summary>
		/// <param name="subscription">The Subscription</param>
		/// <returns>Index View or same View if error</returns>
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult Create(SubscriptionViewModel subscription)
		{
			if (ModelState.IsValid)
			{
				SubscriptionDTO subscriptiondto = Mapper.Map<SubscriptionViewModel, SubscriptionDTO>(subscription);
				try
				{
					SubscriptionServices.SubscribeToNewsletter(subscriptiondto);
				}
				catch (ApplicationValidationErrorsException ex)
				{
					ModelState.AddModelError("SubscriberEmail", ex.ValidationErrors.First());
					return View(subscription);
				}                
				return RedirectToAction("Index");
			}
			return View(subscription);
		}
		/// <summary>
		/// Edit Subscription
		/// </summary>
		/// <param name="id">The Subscription identity</param>
		/// <returns>Subscription View in Edit mode</returns>
		[Authorize(Roles = "Admin")]
		[EnableCompression]
		public ActionResult Edit(int id)
		{
			return View(SubscriptionServices.FindEntityByIdentity(id));
		}

		/// <summary>
		/// Save a edited Subscription
		/// </summary>
		/// <param name="subscription">The Subscription</param>
		/// <returns>Index or same View</returns>
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult Edit(Subscription subscription)
		{
			if (ModelState.IsValid)
			{
				try
				{
					SubscriptionServices.SaveEntity(subscription);
				}
				catch (ApplicationValidationErrorsException ex)
				{
					ModelState.AddModelError("SubscriberEmail", ex.ValidationErrors.First());
					return View(subscription);
				}                
				return RedirectToAction("Index");
			}
			return View(subscription);
		}

		/// <summary>
		/// Delete Subscription
		/// </summary>
		/// <param name="id">Subscription identity</param>
		/// <returns>The Delete Subscription View</returns>
		[Authorize(Roles = "Admin")]
		[EnableCompression]
		public ActionResult Delete(int id)
		{
			return View(SubscriptionServices.FindEntityByIdentity(id));
		}

		/// <summary>
		/// Delete Subscription
		/// </summary>
		/// <param name="id">The Subscription identity</param>
		/// <returns>Index or same View if error</returns>
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		[EnableCompression]
		[ValidateAntiForgeryToken]
		[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
		public ActionResult DeleteConfirmed(int id)
		{
			try
			{
                Subscription subscription = SubscriptionServices.FindEntityByIdentity(id);
				SubscriptionServices.DeleteSubscription(subscription.SubscriberEmail, SubscriptionType.Newsletter);
			}
			catch (OperationCanceledException ex)
			{
				ModelState.AddModelError("", Resources.AppMessages.Error_Category_With_Posts);
				return View(SubscriptionServices.FindEntityByIdentity(id));
			}
			return RedirectToAction("Index");
		}

		[ChildActionOnly]
		public ActionResult SubscribeToNewsletter()
		{
			if (CodeFirstSecurity.IsAuthenticated)
			{
				string username = CodeFirstSecurity.GetUserMail(CodeFirstSecurity.CurrentUserName);
				Subscription subscription = SubscriptionServices.FindAllEntities(s => s.SubscriberEmail == username,null,null).FirstOrDefault(s => s.SubscriptionType == SubscriptionType.Newsletter);
				if (subscription == null)
				{
					return PartialView();
				}
				else
				{
					return new EmptyResult();
				}
			}
			return PartialView(new SubscriptionViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SubscribeToNewsletter(SubscriptionViewModel subscription)
		{
			if (ModelState.IsValid)
			{	
				SubscriptionDTO subscriptiondto = Mapper.Map<SubscriptionViewModel, SubscriptionDTO>(subscription);
				if (CodeFirstSecurity.IsAuthenticated)
				{
					subscriptiondto.IsConfirmed = true;
				}
				try
				{
					string token = SubscriptionServices.SubscribeToNewsletter(subscriptiondto);
					if (!CodeFirstSecurity.IsAuthenticated)
					{
						SmtpClient client = new SmtpClient { Host = BgResources.Email_Server, Port = Int32.Parse(BgResources.Email_SmtpPort), EnableSsl = BgResources.Email_SSL, Credentials = new NetworkCredential(BgResources.Email_UserName, BgResources.Email_Password) };
						UserMailer.ConfirmSubscription(token, subscriptiondto.SubscriberEmail, subscriptiondto).Send(new SmtpClientWrapper { InnerSmtpClient = client });
						return Json(new { result = "ok", message = String.Format(Resources.AppMessages.Subscription_Newsletter_ConfirmationMail_Sent, subscription.SubscriberEmail) });
					}
					return Json(new { result = "ok", message = Resources.AppMessages.ConfirmSubscription_Thanks });                    
				}
				catch (ApplicationValidationErrorsException ex)
				{
					foreach (string str in ex.ValidationErrors)
					{
						ModelState.AddModelError("", str);
					}
					return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray(), modelerror = false });
				}
				catch (SmtpException ex)
				{
					SubscriptionServices.DeleteSubscription(subscription.SubscriberEmail, SubscriptionType.Newsletter);
					return Json(new { result = "error", errors = new List<KeyValuePair<string,string>> { new KeyValuePair<string, string>("", Resources.AppMessages.Error_SendMail) }, modelerror = false });                    
				}
			}
			return Json(new { result = "error", errors = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage)).ToArray(), modelerror = true });			
		}

		public ActionResult ConfirmSubscription(string token, string email)
		{
			try
			{
				SubscriptionServices.ConfirmSubscription(token, email, SubscriptionType.Newsletter);
			}
			catch (ApplicationValidationErrorsException ex)
			{
				throw new ApplicationException(ex.ValidationErrors.First());
			}
			return RedirectToAction("ConfirmSubscriptionSuccesfull");
		}

		[HttpGet]
		public ActionResult ConfirmSubscriptionForm()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ConfirmSubscriptionForm(ConfirmationSubscriptionModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					SubscriptionServices.ConfirmSubscription(model.Token, model.SubscriberEmail, SubscriptionType.Newsletter);
					return RedirectToAction("ConfirmSubscriptionSuccesfull");
				}
				catch (ApplicationValidationErrorsException ex)
				{
					foreach (string str in ex.ValidationErrors)
					{
						ModelState.AddModelError("", str);
					}                    
				}
			}
			return View(model);
		}

		public ActionResult ConfirmSubscriptionSuccesfull()
		{
			return View();
		}

		
		public ActionResult UnSubscribe()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UnSubscribe(UnsubscribeViewModel model)
		{
			string recaptchaprivatekey = BgResources.Recaptcha_PrivateKeyHttp;
			try
			{
				if (!ReCaptcha.Validate(privateKey: recaptchaprivatekey))
				{
					ModelState.AddModelError("recaptcha", Resources.AppMessages.Error_Recaptcha);
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError("recaptcha", Resources.AppMessages.Error_Recaptcha_Key);
			}
			if (ModelState.IsValid)
			{
				try
				{
					SubscriptionServices.Unsubscribe(model.Email);
				}
				catch (ApplicationValidationErrorsException ex)
				{
					foreach (string str in ex.ValidationErrors)
					{
						ModelState.AddModelError("", str);
					}                                        
				}
			}
			return View();
		}
	}	
}