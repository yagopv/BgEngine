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
using System.Linq.Expressions;

using BgEngine.Domain.EntityModel;

namespace BgEngine.Domain.RepositoryContracts
{
    /// <summary>
    /// Contract for non-generic methods for Post
    /// </summary>
    public interface IPostRepository:IRepository<Post>
    {
        ICollection<Post> GetLatestPost(int howmany);
        ICollection<Post> GetLatestPostPremium(int howmany);
        ICollection<Post> GetPostsByCategory(string category);
        ICollection<Post> GetPostsByCategoryPremium(string category);
        ICollection<Post> GetPostsByDate(int year, int month);
        ICollection<Post> GetPostsByDatePremium(int year, int month);
        ICollection<Post> GetPostsByTag(string tag);
        ICollection<Post> GetPostsByTagPremium(string tag);
        IEnumerable<PostByDate> GetAllPostsByDate(bool ispremium);
        int GetPagedCount();
        IEnumerable<Post> GetPagedPosts<S>(int pageIndex, int pageCount, Expression<Func<Post, S>> orderByExpression, bool ascending = true, string searchstring = "");
        void AddTagsToPost(Post post, int[] tags);
    }
}