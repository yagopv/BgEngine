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
using System.Data.Entity;

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;
using BgEngine.Domain.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for generic methods for all the Entities
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Unit of Work implemented in this bounded context
        /// </summary>
        IBlogUnitOfWork unitofwork;

        /// <summary>
        /// The Unit of Work exposed to application services or whatever
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public IUnitOfWork UnitOfWork 
        { 
            get 
            {
                return this.unitofwork; 
            } 
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">current unit of work</param>
        public Repository(IBlogUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        /// <summary>
        /// Generic method to get a collection of Entities
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <returns>A Enumerable of Entities</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = unitofwork.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            { 
                return query.ToList();
            }
        }
        /// <summary>
        /// Generic Method to get an Entity by Identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        /// <returns>The Entity</returns>
        public virtual TEntity GetByID(object id)
        {
            return unitofwork.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Generic method for add an Entity to the context
        /// </summary>
        /// <param name="entity">The Entity to Add</param>
        public virtual void Insert(TEntity entity)
        {
            unitofwork.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = unitofwork.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Generic method for deleting a method in the context pasing the Entity
        /// </summary>
        /// <param name="entityToDelete">Entity to Delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            unitofwork.Attach<TEntity>(entityToDelete);
            unitofwork.Set<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Generic method for updating an Entity in the context
        /// </summary>
        /// <param name="entityToUpdate">The entity to Update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            unitofwork.SetModified<TEntity>(entityToUpdate);
        }

        /// <summary>
        /// Generic implementation for get Paged Entities
        /// </summary>
        /// <typeparam name="TKey">Key for order Expression</typeparam>
        /// <param name="pageIndex">Index of the Page</param>/// 
        /// <param name="pageCount">Number of Entities to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="ascending">If the order is ascending or descending</param>
        /// <returns>Enumerable of Entities matching the conditions</returns>
        public IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true)
        {
            if (pageIndex < 1) { pageIndex = 1; }

            if (orderByExpression == (Expression<Func<TEntity, TKey>>)null)
                    throw new ArgumentNullException();

            return (ascending) 
                            ?
                        unitofwork.Set<TEntity>().OrderBy(orderByExpression)                      
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList()
                            :
                        unitofwork.Set<TEntity>().OrderByDescending(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList();
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="sqlQuery">The Query to be executed</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>List of Entity</returns>
        public IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters)
        {
            return this.unitofwork.ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute a command in database
        /// </summary>
        /// <param name="sqlCommand">The sql query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>integer representing the sql code</returns>
        public int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters)
        {            
            return this.unitofwork.ExecuteCommand(sqlCommand,parameters);
        }

        /// <summary>
        /// Get count of Entities
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return unitofwork.Set<TEntity>().Count();
        }
    }
}