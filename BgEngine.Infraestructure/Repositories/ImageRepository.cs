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
using System.Linq;
using System.Linq.Expressions;

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;


namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Image Entity
    /// </summary>
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IBlogUnitOfWork currentunitofwork;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public ImageRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) 
        {
            this.currentunitofwork = unitofwork;
        }

        /// <summary>
        /// Insert Image
        /// create Image with current Date/Time
        /// </summary>
        /// <param name="entity">The Image to Insert</param>
        public override void Insert(Image entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Get Images ordered and filtered by a search string passed
        /// </summary>
        /// <typeparam name="S">The generic type of the order expression</typeparam>
        /// <param name="orderByExpression">The order expression</param>
        /// <param name="searchstring">The search string</param>
        /// <returns>Enumerable of Images</returns>
        public IEnumerable<Image> GetImages<S>(Expression<Func<Image, S>> orderByExpression, string searchstring = "")
        {
            if (!String.IsNullOrEmpty(searchstring))
            {
                return currentunitofwork.Images.Where(image => image.FileName.Contains(searchstring) || image.Name.Contains(searchstring) || image.Description.Contains(searchstring))
                                   .OrderBy(orderByExpression)
                                   .ToList();
            }
            else
            {
                return currentunitofwork.Images.OrderBy(orderByExpression).ToList();
            }
        }
    }
}