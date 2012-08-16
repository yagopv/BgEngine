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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Represent a User in the Domain
    /// </summary>
    public class User
    {
        /// <summary>
        /// User Identity
        /// </summary>
        [Key]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_UserId")]
        public  Guid UserId { get; set; }

        /// <summary>
        /// User name for the Site
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_UserId", Prompt = "User_UserName_Prompt")]
        public string Username { get; set; }
        
        /// <summary>
        /// Email where can communicate with this User
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Mail", Prompt = "User_Mail_Prompt")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "InvalidMail")]
        public string Email { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "PasswordLenght", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Password", Prompt = "User_Password_Prompt")]
        public string Password { get; set; }

        /// <summary>
        /// If the User is confirmed and can access with privileges
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_IsConfirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Password failures accumulated
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_PasswordFailures")]
        public int PasswordFailuresSinceLastSuccess { get; set; }

        /// <summary>
        /// Date for the last password failure
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_LastPasswordFailureDate")]
        public Nullable<DateTime> LastPasswordFailureDate { get; set; }
        
        /// <summary>
        /// A confirmation token for creating the Account
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        public string ConfirmationToken { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_CreateDate")]
        public Nullable<DateTime> CreateDate { get; set; }

        /// <summary>
        /// Informed on password change
        /// </summary>
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_PasswordChangedDate")]
        public Nullable<DateTime> PasswordChangedDate { get; set; }
        
        /// <summary>
        /// Verification Token to be sent when an User try to change the password
        /// </summary>
        public string PasswordVerificationToken { get; set; }

        /// <summary>
        /// The expiration date form the Token
        /// </summary>
        public Nullable<DateTime> PasswordVerificationTokenExpirationDate { get; set; }
        
        /// <summary>
        /// Navigation property with the Roles related to an User
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        
        /// <summary>
        /// The Comments an User wrote
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        
        /// <summary>
        /// The Posts an User wrote
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
        
        /// <summary>
        /// First name for the User
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_FirstName", Prompt = "User_FirstName_Prompt")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Last Name for the User
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_LastName", Prompt = "User_LastName_Prompt")]
        public string LastName { get; set; }

        /// <summary>
        /// TimeZone for the USer
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_TimeZone", Prompt = "User_TimeZone_Prompt")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Culture of the User
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.AppMessages), ErrorMessageResourceName = "ErrorLenght")]
        [Display(ResourceType = typeof(Resources.AppMessages), Name = "User_Culture", Prompt = "User_Culture_Prompt")]
        public string Culture { get; set; }
    }
}