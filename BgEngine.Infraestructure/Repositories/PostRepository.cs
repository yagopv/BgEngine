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
using BgEngine.Domain.EntityModel;
using BgEngine.Infraestructure.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Post Entity
    /// </summary>
    public class PostRepository : Repository<Post>, IPostRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IBlogUnitOfWork currentunitofwork;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public PostRepository(IBlogUnitOfWork unitofwork) : base(unitofwork)
        {
            this.currentunitofwork = unitofwork;
        }

        /// <summary>
        /// private attributte storing the paged count when filtered by search string
        /// </summary>
        private int pagedcount;

        /// <summary>
        /// Get Latest Post inserted in the Database. Get all public Posts
        /// </summary>
        /// <param name="howmany">Number of Posts to get</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetLatestPost(int howmany)
        {
            return currentunitofwork.Posts
                    .Include(p=>p.Category).Include(p=> p.User).Include(p=>p.Tags).Include(p=>p.Image).Include(p=>p.Comments)
                    .Where(p => p.IsHomePost && p.IsPublic && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated)
                    .Take(howmany).ToList();
        }

        /// <summary>
        /// Get the latest Posts for premium user. The premium users can get all the Posts
        /// </summary>
        /// <param name="howmany">Number of Posts to get</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetLatestPostPremium(int howmany)
        {
            return currentunitofwork.Posts
                    .Include(p => p.Category).Include(p => p.User).Include(p => p.Tags).Include(p => p.Image).Include(p => p.Comments)
                    .Where(p => p.IsHomePost && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated)
                    .Take(howmany).ToList();
        }

        /// <summary>
        /// Get Pots filtered by Category. Get all public Posts
        /// </summary>
        /// <param name="category">The Category name</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByCategory(string category)
        {
            return currentunitofwork.Posts
                    .Include(p => p.Category).Include(p => p.User).Include(p => p.Tags).Include(p => p.Image).Include(p => p.Comments)
                    .Where(p => p.Category.Name == category && p.IsPublic && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated).ToList();
        }

        /// <summary>
        /// Get Pots filtered by Category. Get all Posts because premium user have complete access
        /// </summary>
        /// <param name="category">The Category name</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByCategoryPremium(string category)
        {
            return currentunitofwork.Posts
                    .Include(p => p.Category).Include(p => p.User).Include(p => p.Tags).Include(p => p.Image).Include(p => p.Comments)
                    .Where(p => p.Category.Name == category && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated).ToList();
        }

        /// <summary>
        /// Get Pots filtered by date. Get all public Posts
        /// </summary>
        /// <param name="year">The Year</param>
        /// <param name="month">The Month</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByDate(int year, int month)
        {
            return currentunitofwork.Posts
                    .Include(p => p.Category).Include(p => p.User).Include(p => p.Tags).Include(p => p.Image).Include(p => p.Comments)
                    .Where(p => p.DateCreated.Year == year && p.DateCreated.Month == month && p.IsPublic && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated)
                    .ToList();    
        }

        /// <summary>
        /// Get Posts filtered by date. Get all the Posts because premium users have complete access
        /// </summary>
        /// <param name="year">The Year</param>
        /// <param name="month">The Month</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByDatePremium(int year, int month)
        {
            return currentunitofwork.Posts
                    .Include(p => p.Category).Include(p => p.User).Include(p => p.Tags).Include(p => p.Image).Include(p => p.Comments)
                    .Where(p => p.DateCreated.Year == year && p.DateCreated.Month == month && p.IsAboutMe == false)
                    .OrderByDescending(p => p.DateCreated)
                    .ToList();
        }

        /// <summary>
        /// Get Posts filtered by Tag. Get all public Posts
        /// </summary>
        /// <param name="tag">The Tag name</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByTag(string tag)
        {
            Tag tagged = currentunitofwork.Tags
                .Include(t=>t.Posts)
                .Single(t => t.TagName == tag);
            return tagged.Posts.Where(p => p.IsPublic && p.IsAboutMe == false).OrderByDescending(p => p.DateCreated).ToList();
        }

        /// <summary>
        /// Get Posts filtered by Tag. Get all the Posts because premium users have complete access
        /// </summary>
        /// <param name="tag">The Tag name</param>
        /// <returns>Collection of Posts</returns>
        public ICollection<Post> GetPostsByTagPremium(string tag)
        {
            Tag tagged = currentunitofwork.Tags
                .Include(t=>t.Posts)
                .Single(t => t.TagName == tag);
            return tagged.Posts.Where(p => p.IsAboutMe == false).OrderByDescending(p=>p.DateCreated).ToList();
        }

        /// <summary>
        /// Get all the Posts grouped  by date and with a counter indicating the number of Posts in 
        /// each year-month
        /// </summary>
        /// <returns>Enumerable of Posts</returns>
        public IEnumerable<PostByDate> GetAllPostsByDate(bool ispremium)
        {
            if (ispremium)
            {
                return currentunitofwork.Posts
                        .Where(p => p.IsAboutMe == false)
                        .GroupBy(p => new { p.DateCreated.Year, p.DateCreated.Month })
                        .Select(group => new PostByDate { Year = group.Key.Year, Month = group.Key.Month, Count = group.Count() })
                        .OrderBy(p => new { p.Year, p.Month }).ToList();
            }
            else
            {
                return currentunitofwork.Posts
                    .Where(p => p.IsPublic && p.IsAboutMe == false)
                    .GroupBy(p => new { p.DateCreated.Year, p.DateCreated.Month })
                    .Select(group => new PostByDate { Year = group.Key.Year, Month = group.Key.Month, Count = group.Count() })
                    .OrderBy(p => new { p.Year, p.Month }).ToList();
            }
        }

        /// <summary>
        /// Insert Post
        /// create Post with current Date/Time. Initialize visit Counter
        /// </summary>
        /// <param name="entity">The Post to Insert</param>
        public override void Insert(Post entity)
        {
            entity.Visits = 0;
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Update a Post
        /// Update a Post with current Date/Time
        /// </summary>
        /// <param name="entityToUpdate">The Post to Update</param>
        public override void Update(Post entityToUpdate)
        {
            entityToUpdate.DateUpdated = DateTime.Now;
            base.Update(entityToUpdate);
        }

        /// <summary>
        /// Get the Paged Count.
        /// </summary>
        /// <returns></returns>
        public int GetPagedCount()
        {
            return pagedcount;
        }

        /// <summary>
        /// Get Paged Posts
        /// </summary>
        /// <typeparam name="S">The generic for order expression</typeparam>
        /// <param name="pageIndex">Page Index to get</param>
        /// <param name="pageCount">Number of Posts to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="ascending">If the order is ascending or descending</param>
        /// <param name="searchstring">The search string</param>
        /// <returns>Enumerable of Posts</returns>
        public IEnumerable<Post> GetPagedPosts<S>(int pageIndex, int pageCount, Expression<Func<Post, S>> orderByExpression, bool ascending = true, string searchstring = "")
        {
            if (pageIndex < 1) { pageIndex = 1; }

            if (orderByExpression == (Expression<Func<Post, S>>)null)
                throw new ArgumentNullException();

            if (searchstring != String.Empty)
            {
                pagedcount = currentunitofwork.Posts.Where(p => p.Title.Contains(searchstring) || p.Description.Contains(searchstring)).Count();
                return (ascending)
                                ?
                            currentunitofwork.Posts.Where(p => p.Title.Contains(searchstring) || p.Description.Contains(searchstring))
                                .Include(p => p.User)
                                .Include(p => p.Category)
                                .OrderBy(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList()
                                :
                            currentunitofwork.Posts.Where(p => p.Title.Contains(searchstring) || p.Description.Contains(searchstring))
                                .Include(p => p.User)
                                .Include(p => p.Category)
                                .OrderByDescending(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList();
            }
            else
            {
                pagedcount = currentunitofwork.Posts.Count();
                return (ascending)
                                ?
                            currentunitofwork.Posts
                                .Include(p => p.User)
                                .Include(p => p.Category)
                                .OrderBy(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList()
                                :
                            currentunitofwork.Posts
                                .Include(p => p.User)
                                .Include(p => p.Category)
                                .OrderByDescending(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList();
            }
        }

        /// <summary>
        /// Add Tags to the selected Post
        /// </summary>
        /// <param name="post">The Post</param>
        /// <param name="tags">The Tags identities for add to the Collection of Posts</param>
        public void AddTagsToPost(Post post, int[] tags)
        {
            if (tags == null)
            {
                post.Tags = new List<Tag>();
                return;
            }
            else if (post.Tags == null)
            {
                post.Tags = new List<Tag>();
            }
            var selectedTagsHS = new HashSet<int>(tags);
            var postTags = new HashSet<int> (post.Tags.Select(t => t.TagId));
            foreach (Tag tag in currentunitofwork.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagId))
                {
                    if (!postTags.Contains(tag.TagId))
                    {
                        post.Tags.Add(tag);
                    }
                }
                else
                {
                    if (postTags.Contains(tag.TagId))
                    {
                        post.Tags.Remove(tag);
                    }                    
                }
            }
        }

        /// <summary>
        /// Delete a Post
        /// </summary>
        /// <param name="id">The Identity of the Post</param>
        public override void Delete(object id)
        {
            Post post = currentunitofwork.Posts.Find(id);
            foreach (Comment comment in post.Comments.ToList())
            {
                foreach (Comment relatedcomment in comment.RelatedComments.ToList())
                {
                    currentunitofwork.Comments.Remove(relatedcomment);
                }
                currentunitofwork.Comments.Remove(comment);
            }
            foreach (Rating rating in post.Ratings.ToList())
            {
                currentunitofwork.Ratings.Remove(rating);
            }
            base.Delete(id);
        }
    }
}