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
using System.Web.Routing;
using AutoMapper;
using StructureMap;
using Microsoft.Web.Helpers;

using BgEngine.Application.Services;
using BgEngine.Web.ViewModels;
using BgEngine.Web.Helpers;
using BgEngine.Filters;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Application.DTO;
using BgEngine.Domain.EntityModel;
using BgEngine.Security.Services;

namespace BgEngine
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new IPHostValidationAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Important!! => Routes defined in Route Table are processed in sequence
            // Order is important
            routes.MapRoute(
                "Administration", // Route name
                "Admin/{controller}/{action}/{id}", // URL with parameters  
                 new { controller = "Post", action = "Admin", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "PostByDate", // Route name
                "{controller}/{action}/{year}/{month}", // URL with parameters
                new { controller = "Post", action = "GetPostsByDate" } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            // Load Resources
            var bgresource = ObjectFactory.GetInstance<IBlogResourceServices>();
            bgresource.LoadResources();

            // Check if any user exists. If not, create one
            CheckForAdminUser();

            //Register areas
            AreaRegistration.RegisterAllAreas();

            //Get BlackListed Ips
            BlackListRepository.GetAllIpsInBlackList(this.Server);

            //Create AutoMapper Maps
            Mapper.CreateMap<StatsDTO, StatsModel>();
            Mapper.CreateMap<ConfigOptionsDTO, ConfigOptionsModel>();
            Mapper.CreateMap<ConfigOptionsModel, ConfigOptionsDTO>();
            Mapper.CreateMap<AnonymousUser, AnonymousCommentViewModel>();
            Mapper.CreateMap<SubscriptionViewModel, SubscriptionDTO>();

            //Init Recaptcha helper
            ReCaptcha.PublicKey = BgResources.Recaptcha_PublicKeyHttp;

            //Register Global filters
            RegisterGlobalFilters(GlobalFilters.Filters);

            //Init route table
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        private void CheckForAdminUser()
        {            
             var roles = CodeFirstRoleServices.GetUsersInRole(BgResources.Security_AdminRole);
             if (roles.Length == 0)
             {
                CodeFirstSecurity.CreateAccount(BgResources.Security_AdminRole, "admin", BgResources.Email_UserName, false);
                CodeFirstRoleServices.AddUsersToRoles(new string[] { "admin" }, new string[] { BgResources.Security_AdminRole, BgResources.Security_PremiumRole });
             }
        }
    }
}