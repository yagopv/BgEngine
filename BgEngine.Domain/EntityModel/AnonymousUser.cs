using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    public class AnonymousUser
    {
        public AnonymousUser() { }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="username">The user name</param>
        /// <param name="email">Email for the user</param>
        /// <param name="web">Web if have any</param>
        public AnonymousUser(string username, string email, string web)
        {
            this.Username = username;
            this.Email = email;
            this.Web = web;
        }
        /// <summary>
        /// The AnonymousUser name
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Username")]
        public string Username { get; set; }
        /// <summary>
        /// The AnonymousUser Email
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Email")]
        public string Email { get; set; }
        /// <summary>
        /// The AnonymousUser Web
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "AnonymousUser_Web")]
        public string Web { get; set; }

        /// <summary>
        /// Complex type check for null values because Complex types are always required
        /// </summary>
        public bool HasValue
        {
            get
            {
                return (Username != null || Email != null || Web != null);
            }
        }
    }
}