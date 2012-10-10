using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BgEngine.Web.Results;
using BgEngine.Domain.EntityModel;
using BgEngine.Application.Services;
using BgEngine.Application.ResourceConfiguration;
using BgEngine.Results;

namespace BgEngine.Controllers
{
    public class ApiController : Controller
    {
        private IBlogServices BlogServices;
        private IService<Post> PostServices;
        private IService<Tag> TagServices;
        private IService<Category> CategoryServices;

        public ApiController(IBlogServices blogservices, IService<Tag> tagservices, IService<Category> categoryservices, IService<Post> postservices)
        {
            this.BlogServices = blogservices;
            this.TagServices = tagservices;
            this.CategoryServices = categoryservices;
            this.PostServices = postservices;
        }

        public JsonpResult GetPosts(int? page)
        {                        
            var pageIndex = page ?? 0;            
            IEnumerable<Post> source = this.PostServices.RetrievePaged(pageIndex, 10, p => p.DateCreated, false).Where(p => p.IsPublic);
            var data = new
            {
                posts =
                    from p in source
                    select new
                    {
                        postid = p.PostId,
                        title = p.Title,
                        description = p.Description,
                        commentscount = p.Comments.Count<Comment>(),
                        date = p.DateCreated.ToShortDateString(),
                        category = p.Category.Name,
                        thumbnailpath = getImageUrl(p.Image),
                        user = p.User.Username
                    },
                pendingposts = source.Count() == 10  ? true : false
            };
            return this.Jsonp(data);
        }

        public JsonpResult GetCategories()
        {
            IEnumerable<Category> source = this.CategoryServices.FindAllEntities(null, o => o.OrderBy(c => c.Name) ,null);
            var data =
                from c in source
                select new
                {
                    categoryid = c.CategoryId,
                    name = c.Name,
                    description = c.Description
                };            
            return this.Jsonp(data);
        }

        public JsonpResult GetTags()
        {
            IEnumerable<Tag> source = this.TagServices.FindAllEntities(null, o => o.OrderBy(c => c.TagName), null);
            var data =
                from t in source
                select new
                {
                    tagid = t.TagId,
                    name = t.TagName,
                    description = t.TagDescription
                };
            return this.Jsonp(data);
        }

        public JsonpResult GetPost(int postid)
        {            
            Post post = this.BlogServices.FindPost(postid);            
            if (post.IsPublic)
            { 
                return this.Jsonp(new
                { 
                    title = post.Title,
                    description = post.Description,
                    text = post.Text.Replace("../../..", Request.Url.Scheme + "://" + Request.Url.Authority),
                    commentscount = post.Comments.Count<Comment>(),
                    date = post.DateCreated.ToShortDateString(),
                    category = post.Category.Name,
                    thumbnailpath = getImageUrl(post.Image),
                    user = post.User.Username,
                    tags =
                        from t in post.Tags
                        select new
                        {
                            name = t.TagName,
                            description = t.TagDescription
                        },
                    visits = post.Visits,
                    ratings =
                        from r in post.Ratings
                        select new
                        {
                            value = r.Value
                        },
                    comments =
                        from c in post.Comments
                        where ((c.IsSpam == false ) && (c.isRelatedComment == false))
                        select new
                        {
                            id = c.CommentId,
                            message = c.Message,
                            user = c.User != null ? c.User.Username : c.AnonymousUser.Username,
                            isrelated = c.isRelatedComment,
                            relatedcomments = 
                                from rc in c.RelatedComments
                                where (rc.IsSpam == false)
                                select new
                                {
                                     id = rc.CommentId,
                                     message = rc.Message,
                                     user = rc.User != null ? rc.User.Username : rc.AnonymousUser.Username,
                                }
                        }                        
                });
            }
            else
            {
                return this.Jsonp(new
                {
                    message = "error"
                });
            }
        }

        public JsonpResult GetPostsBy(string by, string id, int? page)
        {
            var pageIndex = page ?? 0;
            IEnumerable<Post> source;
            if (by == "category")
            {
                source = BlogServices.FindPagedPostsByCategory(false, id, pageIndex, 10).Where(p => p.IsPublic);
            }
            else
            {
                source = BlogServices.FindPagedPostsByTag(false, id, pageIndex, 10).Where(p => p.IsPublic);
            }
            var data = new {
                posts =   
                    from p in source
                    select new
                    {
                        postid = p.PostId,
                        title = p.Title,
                        description = p.Description,
                        commentscount = p.Comments.Count<Comment>(),
                        date = p.DateCreated.ToShortDateString(),
                        category = p.Category.Name,
                        thumbnailpath = getImageUrl(p.Image),
                        user = p.User.Username
                    },
                pendingposts = source.Count() == 10 ? true : false
            };
            return this.Jsonp(data);
        }

        public JsonpResult SearchPosts(string searchstring, int? page)
        {
            if (String.IsNullOrEmpty(searchstring)) {
                return this.Jsonp(new { });
            }

            var pageIndex = page ?? 0;
            IEnumerable<Post> source = this.PostServices.RetrievePaged(pageIndex, Int32.Parse(BgResources.Pager_HomeIndexPostsPerPage), p => p.DateCreated, false).Where(p => p.IsPublic && (p.Title.ToLower().Contains(searchstring.ToLower()) || p.Description.ToLower().Contains(searchstring.ToLower())));
            var data =
                from p in source
                select new
                {
                    postid = p.PostId,
                    title = p.Title,
                    description = p.Description,
                    commentscount = p.Comments.Count<Comment>(),
                    date = p.DateCreated.ToShortDateString(),
                    category = p.Category.Name,
                    thumbnailpath = getImageUrl(p.Image),
                    user = p.User.Username
                };
            return this.Jsonp(data);
        }


        private string getImageUrl(Image image)
        {            
            if (image == null)
            {
                return " ";
            }
            else
            {
                return Request.Url.Scheme + "://" + Request.Url.Authority + UrlHelper.GenerateContentUrl(image.ThumbnailPath, this.HttpContext);
            }
        }
    }
}
