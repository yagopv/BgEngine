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
using BgEngine.Domain.RepositoryContracts;
using System.Linq.Expressions;

namespace BgEngine.Application.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        IRepository<TEntity> Repository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public Service(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }
        /// <summary>
        /// Get Entities
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="orderBy">Order expression</param>
        /// <param name="includeProperties">Included entities</param>
        /// <returns>List of Entities</returns>
        public virtual IEnumerable<TEntity> FindAllEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return Repository.Get(filter, orderBy, includeProperties);
        }
        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id">The identity of the Entity</param>
        /// <returns>The Entity</returns>
        public virtual TEntity FindEntityByIdentity(object id)
        {
            return Repository.GetByID(id);
        }
        /// <summary>
        /// Add Entity to Database
        /// </summary>
        /// <param name="entity">The Entity</param>
        public virtual void AddEntity(TEntity entity)
        {
            Repository.Insert(entity);
            Repository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Modify Entity in Database
        /// </summary>
        /// <param name="entity">The Entity</param>
        public virtual void SaveEntity(TEntity entity)
        {
            Repository.Update(entity);
            Repository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Delete Entity from Database
        /// </summary>
        /// <param name="id">The identity of the Entity</param>
        public virtual void DeleteEntity(object id)
        {
            Repository.Delete(id);
            Repository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Delete Entity from Database
        /// </summary>
        /// <param name="id">The Entity</param>
        public virtual void DeleteEntity(TEntity entity)
        {
            Repository.Delete(entity);
            Repository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Get List of Entity Paged
        /// </summary>
        /// <typeparam name="TKey">Order Key</typeparam>
        /// <param name="pageindex">The index of the page to get</param>
        /// <param name="pagecount">The total number of Entities to page</param>
        /// <param name="orderbyexpression">The order Expression</param>
        /// <param name="sortdirection">The Sort direction</param>
        /// <returns>List of Entities</returns>
        public virtual IEnumerable<TEntity> RetrievePaged<TKey>(int pageindex, int pagecount, Expression<Func<TEntity,TKey>> orderbyexpression, bool sortdirection)
        {
            return Repository.GetPagedElements(pageindex, pagecount, orderbyexpression, sortdirection);
        }
        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="sqlQuery">The Query to be executed</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>List of Entity</returns>
        public IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters)
        {
            return Repository.GetFromDatabaseWithQuery(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute a command in database
        /// </summary>
        /// <param name="sqlCommand">The sql query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>integer representing the sql code</returns>
        public int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters)
        {
            return Repository.ExecuteInDatabaseByQuery(sqlCommand, parameters);
        }

        /// <summary>
        /// Get the total number of Entities of a type
        /// </summary>
        /// <returns>Total number of entities</returns>
        public virtual int TotalNumberOfEntity()
        {
            return Repository.GetCount();
        }
    }
}
