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
        public ApiController(IBlogServices blogservices)
        {
            this.BlogServices = blogservices;
        }
        public JsonpResult GetPosts()
        {
            System.Collections.Generic.IEnumerable<Post> source = this.BlogServices.HomePostsForRole(false, int.Parse(BgResources.Pager_HomeIndexPostsPerPage));
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
                    thumbnailpath = (p.Image != null) ? p.Image.ThumbnailPath : " ",
                    user = p.User.Username
                };
            return this.Jsonp(data);
        }
        public JsonpResult GetPost(int id)
        {            
            Post post = this.BlogServices.FindPost(id);
            return this.Jsonp(new
            {
                title = post.Title,
                description = post.Description,
                commentscount = post.Comments.Count<Comment>(),
                date = post.DateCreated.ToShortDateString(),
                category = post.Category.Name,
                thumbnailpath = (post.Image != null) ? post.Image.ThumbnailPath : " ",
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
                    }
            });
        }
    }
}
