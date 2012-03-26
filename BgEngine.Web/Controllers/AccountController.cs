//==============================================================================
// This file is part of BgEngine.
//
// BgEngine is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BgEngine is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BgEngine. If not, see <http://www.gnu.org/licenses/>.
//==============================================================================
// Copyright (c) 2011 Yago Pérez Vázquez
// Version: 1.0
//==============================================================================

using System;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.Helpers;
using Mvc.Mailer;

using BgEngine.Web.ViewModels;
using BgEngine.Security.Services;
using BgEngine.Web.Mailers;
using BgEngine.Application.Services;
using BgEngine.Domain.EntityModel;
using BgEngine.Domain.Filters;
using BgEngine.Application.ResourceConfiguration;
using System.Net.Mail;
using System.Net;

namespace BgEngine.Controllers
{
    /// <summary>
    /// This class contains the operations for authenticate user in the Web environment
    /// </summary>
    public class AccountController : BaseController
    {
        IUserMailer UserMailer;
        IAccountServices AccountServices;
        /// <summary>
        /// ctor
        /// </summary>
        public AccountController(IAccountServices accountservices)
        {
            UserMailer = new UserMailer();
            this.AccountServices =  accountservices;
        }

        /// <summary>
        /// Action to renders the LogOn view for being authenticathed
        /// </summary>
        /// <returns>LogOn view</returns>
        [EnableCompression]
        public ActionResult LogOn()
        {
            return View();
        }
        /// <summary>
        /// Post action with the input data filled by an user who try to authenticate
        /// </summary>
        /// <param name="model">The ViewModel for Logon</param>
        /// <param name="returnUrl">The target url for redirect once the authentication process finish ok</param>
        /// <returns></returns>    
        [HttpPost]
        [EnableCompression]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (CodeFirstSecurity.Login(model.UserName,model.Password,model.RememberMe))
                {
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (TempData["returnUrl"] != null)
                        {
                            string url = TempData["returnUrl"].ToString();
                            TempData.Remove("returnUrl");
                            return Redirect(url);
                        }
                        else
                        {
                            return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
                        }                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El usuario o contraseña son incorrectos");
                }
            }
           return View(model);
        }
        /// <summary>
        /// LogOff the user authenticathed
        /// </summary>
        [EnableCompression]
        public ActionResult LogOff()
        {            
            CodeFirstSecurity.Logout();
            return RedirectToRoute("Default", new { controller = "Home", action = "Index" }); 
        }
        /// <summary>
        /// Render the registration form
        /// </summary>
        [EnableCompression]
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Post the registration form for creating new user 
        /// </summary>
        [HttpPost]
        [EnableCompression]
        public ActionResult Register(RegisterModel model)
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
                string token = null;
                try
                {
                    token = CodeFirstSecurity.CreateAccount(model.UserName, model.Password, model.Email, model.FirstName, model.LastName, model.TimeZone, model.Culture, requireConfirmationToken: true);
                    SmtpClient client = new SmtpClient { Host = BgResources.Email_Server, Port = Int32.Parse(BgResources.Email_SmtpPort), EnableSsl = BgResources.Email_SSL, Credentials = new NetworkCredential(BgResources.Email_UserName,BgResources.Email_Password) } ;
                    UserMailer.Register(token, model.Email, AccountServices.FindUser(usr => usr.Username == model.UserName)).Send(new SmtpClientWrapper { InnerSmtpClient = client });
                    ViewBag.Email = model.Email;
                    return View("CompleteRegister");
                }
                catch (MembershipCreateUserException ex)
                {
                    if ((ex.StatusCode == MembershipCreateStatus.DuplicateUserName) || (ex.StatusCode == MembershipCreateStatus.InvalidUserName))
                    {
                        ModelState.AddModelError("UserName", ErrorCodeToString(ex.StatusCode));
                    }
                    else if ((ex.StatusCode == MembershipCreateStatus.DuplicateEmail) || (ex.StatusCode == MembershipCreateStatus.InvalidEmail))
                    {
                        ModelState.AddModelError("Email", ErrorCodeToString(ex.StatusCode));
                    }
                    else if (ex.StatusCode == MembershipCreateStatus.InvalidPassword)
                    {
                        ModelState.AddModelError("Password", ErrorCodeToString(ex.StatusCode));
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(Resources.AppMessages.Error_SendMail);
                }
            }
            return View(model);
        }
        /// <summary>
        /// Confirm account from an email
        /// </summary>
        [EnableCompression]
        public ActionResult ConfirmAccount(string id, string user)
        {
            //string confirmationToken = id.Replace("backslash", "/").Replace("percent", "%").Replace("ampersand", "&").Replace("space", " ");
            User userobj = AccountServices.FindUser(usr => usr.Username == user);
            if ((userobj != null) && (userobj.IsConfirmed == true))
            {
                throw new Exception(String.Format(Resources.AppMessages.Error_User_Is_Confirmed,user));
            }            
            try
            {
                if (CodeFirstSecurity.ConfirmAccount(id))
                {
                    FormsAuthentication.SetAuthCookie(user, false);
                    return RedirectToRoute("Default", new { controller = "Account", action = "RegistrationSuccesfull" });
                }
                else
                {
                    throw new Exception(Resources.AppMessages.Error_ConfirmAccount);
                }
            }
            catch (Exception)
            {
                
                throw new Exception(Resources.AppMessages.Error_ConfirmAccount);
            }
        }
        /// <summary>
        /// Renders a form for finish the registration process manually
        /// </summary>
        [EnableCompression]        
        public ActionResult ConfirmManually()
        {
            return View();
        }
        /// <summary>
        /// Finish the registration process manually from a form 
        /// </summary>
        [HttpPost]
        [EnableCompression]
        public ActionResult ConfirmManually(ConfirmationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = AccountServices.FindUser(usr => usr.Username == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", Resources.AppMessages.Error_User_Not_Exist);
                    return View(model);
                }
                else
                {
                    if (user.IsConfirmed)
                    {
                        ModelState.AddModelError("UserName", Resources.AppMessages.Error_User_Confirmed_Yet);
                        return View(model);
                    }
                }
                if (CodeFirstSecurity.ConfirmAccount(model.Token) == false)
                {
                    ModelState.AddModelError("Token", Resources.AppMessages.Error_ConfirmationToken);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToRoute("Default", new { controller = "Account", action = "RegistrationSuccesfull" });
                }
            }
            return View();
        }
        /// <summary>
        /// Renders an View for resetting an account
        /// </summary>
        [EnableCompression]
        public ActionResult ConfirmResetAccount(string id, string user)
        {
            ViewBag.Token = id;
            ViewBag.User = user;
            return View();
        }
        /// <summary>
        /// Finish the process resetting an account
        /// </summary>
        [HttpPost]
        [EnableCompression]
        public ActionResult ConfirmResetAccount(ChangePasswordModel model)
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
                if (CodeFirstSecurity.ResetPassword(model.ConfirmationToken, model.NewPassword))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToRoute("Default", new { controller = "Account", action = "ResetAccountSuccesfull" });
                }
                else
                {
                    ModelState.AddModelError("", Resources.AppMessages.Error_ResetAccount);
                }
                ViewBag.Token = model.ConfirmationToken;
                ViewBag.UserName = model.UserName; 
            }
            return View();
        }
        /// <summary>
        /// Renders the Complete register view
        /// </summary>
        /// <returns></returns>
        [EnableCompression]
        public ActionResult CompleteRegister()
        {
            return View();
        }
        /// <summary>
        /// Render the Complete reset account view
        /// </summary>
        [EnableCompression]
        public ActionResult CompleteResetAccount()
        {
            return View();
        }
        /// <summary>
        /// Renders a view with a message related to the registration process finished successfully
        /// </summary>
        [EnableCompression]
        public ActionResult RegistrationSuccesfull()
        {
            return View();
        }
        /// <summary>
        /// Renders a view with a message related to the reset account process finished successfully
        /// </summary>
        [EnableCompression]
        public ActionResult ResetAccountSuccesfull()
        {
            return View();
        }
        /// <summary>
        /// Renders a view for resetting an account
        /// </summary>
        [EnableCompression]
        public ActionResult ResetAccount()
        {
            return View();
        }
        /// <summary>
        /// Get the information for resetting an account
        /// </summary>
        [HttpPost]
        [EnableCompression]
        public ActionResult ResetAccount(ResetAccountModel model)
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
                User user = AccountServices.FindUser(usr => usr.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", Resources.AppMessages.Error_Email_Not_Exist);
                    return View(model);
                }
                try
                {
                    string token = null;
                    token = CodeFirstSecurity.GeneratePasswordResetToken(user.Username, tokenExpirationInMinutesFromNow: 1440);
                    SmtpClient client = new SmtpClient { Host = BgResources.Email_Server, Port = Int32.Parse(BgResources.Email_SmtpPort), EnableSsl = BgResources.Email_SSL, Credentials = new NetworkCredential(BgResources.Email_UserName, BgResources.Email_Password) };
                    UserMailer.PasswordReset(token, user).Send( new SmtpClientWrapper { InnerSmtpClient = client});
                    ViewBag.Email = model.Email;
                    return View("CompleteResetAccount");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("UserName", ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(Resources.AppMessages.Error_SendMail);
                }
            }
            return View(model);           
        }
        /// <summary>
        /// Error messages
        /// </summary>
        /// <param name="createStatus"></param>
        /// <returns></returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El usuario ya existe, por favor introduce otro nuevo";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un usuario con esa direccion de mail. Introduce una nueva direccion de mail";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña introducida no es válida";

                case MembershipCreateStatus.InvalidEmail:
                    return "La direccion de mail no es válida. Por favor revísala";

                case MembershipCreateStatus.InvalidUserName:
                    return "El usuario introducido no es un nombre de usuario válido";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
