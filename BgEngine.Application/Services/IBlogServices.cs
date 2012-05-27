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

using BgEngine.Application.DTO;
using BgEngine.Domain.EntityModel;
using PagedList;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Contract for Blog general purposes
    /// </summary>
    public interface IBlogServices
    {
        IEnumerable<Post> HomePostsForRole(bool ispremium, int howmany);
        IEnumerable<Post> SearchForPagedPostsByParam<TKey>(int pageindex, int pagecount,Expression<Func<Post, TKey>> orderByExpression, bool isascending, string searchstring = "");
        IEnumerable<PostByDate> FindAllPostsByDate(bool ispremium);
        IEnumerable<StringValueDTO> BuildPostsAutocompleteSuggestions(string searchstring);
        IPagedList<Post> FindPagedPostsByCategory(bool ispremium, string category, int pageindex, int pagecount);
        IPagedList<Post> FindPagedPostsByDate(bool ispremium, int year, int month, int pageindex, int pagecount);
        IPagedList<Post> FindPagedPostsByTag(bool ispremium, string tag, int pageindex, int pagecount);
        IEnumerable<Post> FindRSSPosts(bool ispremium, int howmany);
        void CreatePost(Post post, int[] tags);
        void UpdatePost(Post post, int[] tags);
        void CreateComment(Comment comment, User user);
        void SaveComment(Comment comment);
        void AddRelatedComment(Comment comment, int parent, User user);
        int GetPagedNumber();
        IDictionary<string, int> GetModelForTagCloud(bool ispremium);
        void DeleteComment(object id);
        Post FindAboutMe();
        Post FindPost(string postcode);
        Post FindPost(int postid);
    }
}
