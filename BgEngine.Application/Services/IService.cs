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

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Contract for generic services for crud operations and data retrieval
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> FindAllEntities(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        TEntity FindEntityByIdentity(object id);
        void AddEntity(TEntity entity);
        void SaveEntity(TEntity entity);
        void DeleteEntity(object id);
        void DeleteEntity(TEntity entity);
        IEnumerable<TEntity> RetrievePaged<TKey>(int pageindex, int pagecount, Expression<Func<TEntity,TKey>> orderbyexpression, bool sortdirection);
        IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters);
        int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters);
        int TotalNumberOfEntity();
    }
}
