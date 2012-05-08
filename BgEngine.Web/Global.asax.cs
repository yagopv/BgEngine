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

using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using StructureMap;
using Microsoft.Web.Helpers;

using BgEngine.Application.Services;
using BgEngine.Web.ViewModels;
using BgEngine.Domain.DatabaseContracts;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Application.DTO;
using BgEngine.Domain.EntityModel;

namespace BgEngine
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
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
            // Init Database
            // Uncomment next lines to Init the database
            // You should change between Initializers in IoC.cs if you want to load test data or not
            // Drop the Database if exists before creating a empty or test one
            //var database = ObjectFactory.GetInstance<IDatabaseInitialize>();
            //database.Initialize();

            //Execute updates against Database
            //var service = ObjectFactory.GetInstance<IService<Tag>>();
            //int i = service.ExecuteInDatabaseByQuery("ALTER TABLE Categories ALTER COLUMN Name NVARCHAR ( 100 )");            
            
            // Load Resources
            var bgresource = ObjectFactory.GetInstance<IBlogResourceServices>();
            bgresource.LoadResources();

            //Register areas
            AreaRegistration.RegisterAllAreas();

            //Create AutoMapper Maps
            Mapper.CreateMap<StatsDTO, StatsModel>();
            Mapper.CreateMap<ConfigOptionsDTO, ConfigOptionsModel>();
            Mapper.CreateMap<ConfigOptionsModel, ConfigOptionsDTO>();
            Mapper.CreateMap<AnonymousUser, AnonymousCommentViewModel>();

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
    }
}