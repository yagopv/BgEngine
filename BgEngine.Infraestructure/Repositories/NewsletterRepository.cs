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
using BgEngine.Infraestructure.DatabaseInitialization;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Newsletter Entity
    /// </summary>
    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IBlogUnitOfWork currentunitofwork;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public NewsletterRepository(IBlogUnitOfWork unitofwork)
            : base(unitofwork)
        {
            this.currentunitofwork = unitofwork;
        }

        /// <summary>
        /// Delete NewsletterTask from context
        /// </summary>
        /// <param name="task"></param>
        public void DeleteNewsletterTask(NewsletterTask task)
        {
            currentunitofwork.Set<NewsletterTask>().Remove(task);
        }

        /// <summary>
        /// Update NewsletterTask from context
        /// </summary>
        /// <param name="task"></param>
        public void UpdateNewsletterTask(NewsletterTask task)
        {
            currentunitofwork.SetModified<NewsletterTask>(task);
        }
    }
}