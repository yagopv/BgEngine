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
using System.Web;

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;
using BgEngine.Domain.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Album Entity
    /// </summary>
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public AlbumRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) { }

        /// <summary>
        /// Insert a Video
        /// creates a Video in the current Date/Time
        /// </summary>
        /// <param name="entity">The Video to Insert</param>
        public override void Insert(Album entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Update a Video
        /// update a Video changing the current Date/Time
        /// </summary>
        /// <param name="entityToUpdate">The Video to be updated</param>
        public override void Update(Album entityToUpdate)
        {
            entityToUpdate.DateUpdated = DateTime.Now;
            base.Update(entityToUpdate);
        }

    }
}