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

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Comment Entity
    /// </summary>
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public CommentRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) { }

        /// <summary>
        /// Insert Comment
        /// create comment with current Date/Time
        /// </summary>
        /// <param name="entity">The Comment to Insert</param>
        public override void Insert(Comment entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Update a Comment
        /// Update a comment with current Date/Time
        /// </summary>
        /// <param name="entityToUpdate">The Comment to Update</param>
        public override void Update(Comment entityToUpdate)
        {
            entityToUpdate.DateUpdated = DateTime.Now;
            base.Update(entityToUpdate);
        }
    }
}