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

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Category Entity
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IBlogUnitOfWork currentunitofwork;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public CategoryRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) 
        {
            this.currentunitofwork = unitofwork;
        }

        /// <summary>
        /// Insert a Category
        /// creates new Category with current Date/Time
        /// </summary>
        /// <param name="entity">Category to Insert</param>
        public override void Insert(Category entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <exception cref="OperationCancelledException">
        ///     Launched when a Category is related to any Post.
        ///     In this case can´t be removed
        /// </exception>
        /// <param name="id">Identity of the Category to be removed</param>
        public override void Delete(object id)
        {
            Category category = currentunitofwork.Categories.Find(id);
            if (category.Posts.Any())
            {
                throw new OperationCanceledException();
            }
            else
            {
                base.Delete(id);
            }
        }
    }
}