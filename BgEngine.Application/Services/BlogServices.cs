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
using PagedList;

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Application.DTO;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Domain.Validation;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Application operations with Blog main entities
    /// </summary>
    public class BlogServices:IBlogServices
    {
        IPostRepository PostRepository;
        ICommentRepository CommentRepository;
        ITagRepository TagRepository;
        /// <summary>
        /// ctor
        /// </summary>
        public BlogServices(IPostRepository postrepository, ICommentRepository commentRepository, ITagRepository tagrepository)
        {
            this.PostRepository = postrepository;
            this.CommentRepository = commentRepository;
            this.TagRepository = tagrepository;
        }
        /// <summary>
        /// Get Posts for Home Index page
        /// </summary>
        /// <param name="ispremium">Identify users with premium account</param>
        /// <param name="howmany">Number of Posts to get</param>
        /// <returns>List of Posts</returns>
        public IEnumerable<Post> HomePostsForRole(bool ispremium, int howmany)
        {
            if (ispremium)
            {               
                return PostRepository.GetLatestPostPremium(howmany);
            }
            else
            {
                return PostRepository.GetLatestPost(howmany);
            }
        }
        /// <summary>
        /// Search for Posts 
        /// </summary>
        /// <typeparam name="TKey">The order key</typeparam>
        /// <param name="pageindex">Index of the page</param>
        /// <param name="pagecount">Total number of posts</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="isascending">Order direction</param>
        /// <param name="searchstring">The string to search in the Title and Description properties</param>
        /// <returns>List of Posts</returns>
        public IEnumerable<Post> SearchForPagedPostsByParam<TKey>(int pageindex, int pagecount, Expression<Func<Post, TKey>> orderByExpression, bool isascending, string searchstring = "")
        {
            return PostRepository.GetPagedPosts(pageindex, pagecount, orderByExpression, isascending, searchstring);
        }
        /// <summary>
        /// Get the total number of Post when the searchstring is filtered
        /// </summary>
        /// <returns>Number of Posts</returns>
        public int GetPagedNumber()
        {
            return PostRepository.GetPagedCount();
        }
        /// <summary>
        /// Get Posts by date
        /// </summary>
        /// <returns>List of Posts</returns>
        public IEnumerable<PostByDate> FindAllPostsByDate(bool ispremium)
        {
            return PostRepository.GetAllPostsByDate(ispremium);
        }
        /// <summary>
        /// Get Title of Posts for autocomplete jquery input box
        /// </summary>
        /// <param name="searchstring">The string to search in Post entity</param>
        /// <returns>List of Titles</returns>
        public IEnumerable<StringValueDTO> BuildPostsAutocompleteSuggestions(string searchstring)
        {
            List<StringValueDTO> postsJSON = new List<StringValueDTO>();
            foreach (Post post in PostRepository.Get(p => p.Title.Contains(searchstring) || p.Description.Contains(searchstring) || p.Code.Contains(searchstring)).ToList())
            {
                postsJSON.Add(new StringValueDTO(post.Title));
            }
            return postsJSON;
        }
        /// <summary>
        /// Add a Post to the Database
        /// </summary>
        /// <param name="post">Post</param>
        /// <param name="selectedtags">Tags relates with the Post entity</param>
        public void CreatePost(Post post, int[] selectedtags)
        {
            List<string> errors = new List<string>();

            if (PostRepository.Get(p => p.Code == post.Code).Any())
            {
                errors.Add(Resources.AppMessages.Post_Code_Repeated);
            }

            if (post.IsAboutMe == true && PostRepository.Get(p => p.IsAboutMe).Any())
            {
                errors.Add(String.Format(Resources.AppMessages.About_Me_Exists,PostRepository.Get(p => p.IsAboutMe).Select(p => p.Code).FirstOrDefault()));
            }

            if (errors.Any())
            {
                throw new ApplicationValidationErrorsException(errors);
            }

            PostRepository.Insert(post);
            PostRepository.AddTagsToPost(post, selectedtags);
            PostRepository.UnitOfWork.Commit(); ;
        }
        /// <summary>
        /// Update ans save Post to the Database
        /// </summary>
        /// <param name="post">The Post to save</param>
        /// <param name="tags">Related Tags with the Post entity</param>
        public void UpdatePost(Post post, int[] tags)
        {
            List<string> errors = new List<string>();

            if (PostRepository.Get(p => p.Code == post.Code && p.PostId != post.PostId).Any())
            {
                errors.Add(Resources.AppMessages.Post_Code_Repeated);
            }

            if (post.IsAboutMe == true && PostRepository.Get(p => p.IsAboutMe && p.PostId != post.PostId).Any())
            {
                errors.Add(String.Format(Resources.AppMessages.About_Me_Exists, PostRepository.Get(p => p.IsAboutMe && p.PostId != post.PostId).Select(p => p.Code).FirstOrDefault()));
            }

            if (errors.Any())
            {
                throw new ApplicationValidationErrorsException(errors);
            }            
            PostRepository.Update(post);
            PostRepository.AddTagsToPost(post, tags);
            PostRepository.UnitOfWork.Commit(); ;
        }
        /// <summary>
        /// Get a number of Post by Category
        /// </summary>
        /// <param name="ispremium">Identify Premium User account</param>
        /// <param name="category">Category title</param>
        /// <param name="pageindex">Index of the page to get</param>
        /// <param name="pagecount">Number of Posts to get</param>
        /// <returns>A Paged List of Posts</returns>
        public IPagedList<Post> FindPagedPostsByCategory(bool ispremium, string category, int pageindex, int pagecount)
        {
            if (ispremium)
            {
                return PostRepository.GetPostsByCategoryPremium(category).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }
            else
            {
                return PostRepository.GetPostsByCategory(category).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }            
        }
        /// <summary>
        /// Get a number of Post by date
        /// </summary>
        /// <param name="ispremium">Identify Premium User account</param>
        /// <param name="year">Year in Blog archive</param>
        /// <param name="month">Month in Blog archive</param>
        /// <param name="pageindex">Index of the page to get</param>
        /// <param name="pagecount">Number of Posts to get</param>
        /// <returns>A Paged List of Posts</returns>
        public IPagedList<Post> FindPagedPostsByDate(bool ispremium, int year, int month, int pageindex, int pagecount)
        {
            if (ispremium)
            {
                return PostRepository.GetPostsByDatePremium(year, month).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }
            else
            {
                return PostRepository.GetPostsByDate(year, month).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }                        
        }
        /// <summary>
        /// Get a number of Post by tag
        /// </summary>
        /// <param name="ispremium"></param>
        /// <param name="tag"></param>
        /// <param name="pageindex">Index of the page to get</param>
        /// <param name="pagecount">Number of Posts to get</param>
        /// <returns>A Paged List of Posts</returns>
        public IPagedList<Post> FindPagedPostsByTag(bool ispremium, string tag, int pageindex, int pagecount)
        {
            if (ispremium)
            {
                return PostRepository.GetPostsByTagPremium(tag).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }
            else
            {
                return PostRepository.GetPostsByTag(tag).ToPagedList(pageindex, Int32.Parse(BgResources.Pager_PostPerPage));
            }                                    
        }
        /// <summary>
        /// Saves a existing Comment in Database
        /// </summary>
        /// <param name="comment">Comment to save</param>
        public void SaveComment(Comment comment)
        {
            Comment c = CommentRepository.GetByID(comment.CommentId);
            c.Message = comment.Message;
            c.PostId = comment.PostId;
            CommentRepository.Update(c);
            CommentRepository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Ad new Comment to the Blog
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <param name="user">User who write the Comment</param>
        public void CreateComment(Comment comment, User user)
        {
            if (user != null)
            {
                comment.User = user;
            }
            if (comment.AnonymousUser == null)
            {
                comment.AnonymousUser = new AnonymousUser();
            }
            CommentRepository.Insert(comment);
            CommentRepository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Add RelatedComment to main Comment
        /// </summary>
        /// <param name="comment">The Comment</param>
        /// <param name="parent">The parent</param>
        /// <param name="user">The User who writes the Comment</param>
        public void AddRelatedComment(Comment comment, int parent, User user)
        {
            if (user != null)
            {
                comment.User = user;
            }
            if (comment.AnonymousUser == null)
            {
                comment.AnonymousUser = new AnonymousUser();
            }
            comment.isRelatedComment = true;
            CommentRepository.Insert(comment);
            Comment parentComment = CommentRepository.GetByID(parent);
            parentComment.AddRelatedComment(comment);
            CommentRepository.UnitOfWork.Commit();
        }
        /// <summary>
        /// Get Tags with weight to show in Tag Cloud
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, int> GetModelForTagCloud(bool ispremium)
        {
            return TagRepository.GetTagCount(ispremium);
        }
        /// <summary>
        /// Delete a comment and all related comments
        /// </summary>
        /// <param name="id">The identity of the comment to delete</param>
        public void DeleteComment(object id)
        {
            Comment comment = CommentRepository.GetByID(id);
            if (comment.isRelatedComment)
            {
                Comment parent = CommentRepository.Get(c => c.RelatedComments.Any(co => co.CommentId == comment.CommentId)).FirstOrDefault();
                parent.DeleteFromRelatedCollection(comment);
            }
            else
            {
                if (comment.RelatedComments.Any())
                {
                    foreach (Comment relatedcomment in comment.RelatedComments.ToList())
                    {
                        CommentRepository.Delete(relatedcomment);
                    }
                }
            }
            CommentRepository.Delete(comment);
            CommentRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Find the Post representing the About me view
        /// </summary>
        /// <returns>The About me View</returns>
        public Post FindAboutMe()
        {
            return PostRepository.Get(p => p.IsAboutMe && p.IsPublic, null, null).FirstOrDefault();
        }

        public Post FindPost(string postcode)
        {
            Post post = PostRepository.Get(p => p.Code == postcode, p => p.OrderBy(o => o.DateCreated), "Category,User,Tags,Ratings,Image,Comments").Single();
            post.IncreaseVisitCounter();
            PostRepository.Update(post);
            PostRepository.UnitOfWork.Commit();
            return post;
        }

        public Post FindPost(int  postid)
        {
            Post post = PostRepository.Get(p => p.PostId == postid, p => p.OrderBy(o => o.DateCreated), "Category,User,Tags,Ratings,Image,Comments").Single();
            post.IncreaseVisitCounter();
            PostRepository.Update(post);
            PostRepository.UnitOfWork.Commit();
            return post;
        }
    }
}
