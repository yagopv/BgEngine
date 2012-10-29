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

using AutoMapper;
using BgEngine.Web.ViewModels;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Application.DTO;
using BgEngine.Security.Services;

namespace BgEngine.Controllers
{
    /// <summary>
    /// This class get different stats from the website
    /// </summary>
    public class StatsController : BaseController
    {
        IStatsServices StatsServices;
        IBlogResourceServices BlogResourceServices;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="statsservices">Stats service class</param>
        /// <param name="blogresourceservices">Blos resource service class</param>
        public StatsController(IStatsServices statsservices, IBlogResourceServices blogresourceservices)
        {
            this.StatsServices = statsservices;
            this.BlogResourceServices = blogresourceservices;
        }

        /// <summary>
        /// Get stats from website
        /// </summary>
        /// <returns>Stats view</returns>
        [ChildActionOnly]
        public ActionResult Stats()
        {
            return PartialView("Stats", Mapper.Map<StatsDTO, StatsModel>(StatsServices.RetrieveBlogStats()));
        }

        /// <summary>
        /// Get the Config options View
        /// </summary>
        /// <returns>Config options View</returns>
        [ChildActionOnly]
        public ActionResult ConfigOptions()
        {
            return PartialView("ConfigOptions", Mapper.Map<ConfigOptionsDTO, ConfigOptionsModel>(BgResources.GenerateTransferDTO()));
        }

        /// <summary>
        /// Post action for saving website options resources
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A view containing a message with the result of the operation</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "AntiForgeryError")]
        public ActionResult SaveOptions(ConfigOptionsModel model)
        {
            if (ModelState.IsValid)
            {
                BlogResourceServices.UpdateConfigOptions(Mapper.Map<ConfigOptionsModel, ConfigOptionsDTO>(model));
                return PartialView("AjaxSuccess", Resources.AppMessages.Config_OptionsUpdated);
            }
            return PartialView("AjaxError", Resources.AppMessages.Config_OptionsError);
        }

        /// <summary>
        /// Get stats for the stats widget
        /// </summary>
        /// <returns>Stats for show in sidebar stats widget</returns>
        public ActionResult SidebarStats()
        {
            if ((CodeFirstSecurity.IsAuthenticated) && (CodeFirstRoleServices.IsUserInRole(CodeFirstSecurity.CurrentUserName, BgResources.Security_PremiumRole)))
            {
                return PartialView("SidebarStats", Mapper.Map<StatsDTO, StatsModel>(StatsServices.RetrieveSidebarStats(true)));
            }
            else
            {
                return PartialView("SidebarStats", Mapper.Map<StatsDTO, StatsModel>(StatsServices.RetrieveSidebarStats(false)));
            }            
        }
    }
}
