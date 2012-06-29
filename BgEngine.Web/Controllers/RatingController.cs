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
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BgEngine.Domain.EntityModel;
using BgEngine.Application.Services;

namespace BgEngine.Controllers
{
    public class RatingController : BaseController
    {
        IService<Rating> RatingServices;

        public RatingController(IService<Rating> ratingservices)
        {
            this.RatingServices = ratingservices;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = ("AntiForgeryError"))]
        public JsonResult SetRating(Rating rating)
        {
            if (Request.Cookies["rating_" + rating.PostId.ToString()] != null)
            {
                double storedrating = Double.Parse(Request.Cookies["rating_" + rating.PostId.ToString()].Value);
                Rating oldrating = RatingServices.FindAllEntities(r => r.PostId == rating.PostId && r.Value == storedrating,null,null).FirstOrDefault();
                if (oldrating != null)
                {
                    RatingServices.DeleteEntity(oldrating);
                }
                RatingServices.AddEntity(rating);                
                Response.Cookies["rating_" + rating.PostId.ToString()].Value = rating.Value.ToString();
                Response.Cookies["rating_" + rating.PostId.ToString()].Expires = DateTime.Now.AddYears(5);
            }
            else
            {
                HttpCookie cookie = new HttpCookie("rating_" + rating.PostId.ToString(), rating.Value.ToString());
                cookie.Expires = DateTime.Now.AddYears(5);
                Response.Cookies.Add(cookie);
                RatingServices.AddEntity(rating);
            }
            int totalvotes = RatingServices.FindAllEntities(r => r.PostId == rating.PostId,null,null).Count();
            double average = RatingServices.FindAllEntities(r => r.PostId == rating.PostId,null,null).Average(s => s.Value);
            double roundedaverage = Math.Round(average,1);
            return Json(new { roundedaverage, totalvotes });
        }
    }
}
