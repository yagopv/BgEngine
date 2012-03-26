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

using System.Linq;

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Domain.EntityModel;
using BgEngine.Application.DTO;


namespace BgEngine.Application.Services
{
    public class StatsServices : BgEngine.Application.Services.IStatsServices
    {
        IRepository<Post> PostRepository;
        IRepository<Tag> TagRepository;
        IRepository<Album> AlbumRepository;
        IRepository<Category> CategoryRepository;
        IRepository<Image> ImageRepository;
        IRepository<User> UserRepository;
        IRepository<Role> RoleRepository;
        IRepository<Comment> CommentRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public StatsServices(IRepository<Post> postrepository,
                                          IRepository<Tag> tagrepository,
                                          IRepository<Album> albumrepository,
                                          IRepository<Category> categoryrepository,
                                          IRepository<Image> imagerepository,
                                          IRepository<User> userrepository,
                                          IRepository<Role> rolerepository,
                                          IRepository<Comment> commentrepository)
        {
            this.PostRepository = postrepository;
            this.TagRepository = tagrepository;
            this.AlbumRepository = albumrepository;
            this.ImageRepository = imagerepository;
            this.CategoryRepository = categoryrepository;
            this.UserRepository = userrepository;
            this.RoleRepository = rolerepository;
            this.CommentRepository = commentrepository;
        }

        /// <summary>
        /// Get Stats from all entities in Model
        /// </summary>
        /// <returns>DTO object with the Stats</returns>
        public StatsDTO RetrieveBlogStats()
        {            
            StatsDTO model = new StatsDTO();
            model.TotalPosts = PostRepository.GetCount();
            model.TotalTags = TagRepository.GetCount();
            model.TotalAlbums = AlbumRepository.GetCount();
            model.TotalCategories = CategoryRepository.GetCount();
            model.TotalImages = ImageRepository.GetCount();
            model.TotalUsers = UserRepository.GetCount();
            model.TotalComments = CommentRepository.GetCount();
            model.TotalRoles = RoleRepository.GetCount();
            model.TopRatedPosts = PostRepository.Get(p => p.Ratings.Any(), o => o.OrderByDescending(p => p.Ratings.Average(r => r.Value)), "Ratings").Take(10);
            model.MostVisitedPosts = PostRepository.Get(p=>p.Visits > 0, o => o.OrderByDescending(p => p.Visits)).Take(10); 
            model.MostCommentedPosts = PostRepository.Get(p => p.Comments.Any(), o => o.OrderByDescending(c => c.Comments.Count()), "Comments").Take(10);
            model.PostWritenByUser = UserRepository.Get(u => u.Roles.Any(r => r.RoleName == "admin"));
            return model;
        }

        /// <summary>
        /// Get Stats for Sidebar Widget
        /// </summary>
        /// <returns>DTO object with the Stats</returns>
        public StatsDTO RetrieveSidebarStats(bool ispremium)
        {
            StatsDTO model = new StatsDTO();
            if (ispremium)
            {
                model.TopRatedPosts = PostRepository.Get(p => p.Ratings.Any() && p.IsAboutMe == false, o => o.OrderByDescending(p => p.Ratings.Average(r => r.Value)), "Ratings").Take(10);
                model.MostVisitedPosts = PostRepository.Get(p => p.Visits > 0 && p.IsAboutMe == false, o => o.OrderByDescending(p => p.Visits)).Take(10);
                model.MostCommentedPosts = PostRepository.Get(p => p.Comments.Any() && p.IsAboutMe == false, o => o.OrderByDescending(c => c.Comments.Count()), "Comments").Take(10);
            }
            else
            {
                model.TopRatedPosts = PostRepository.Get(p => p.Ratings.Any() && p.IsPublic && p.IsAboutMe == false, o => o.OrderByDescending(p => p.Ratings.Average(r => r.Value)), "Ratings,Image").Take(10);
                model.MostVisitedPosts = PostRepository.Get(p => p.Visits > 0 && p.IsPublic && p.IsAboutMe == false, o => o.OrderByDescending(p => p.Visits), "Image").Take(10);
                model.MostCommentedPosts = PostRepository.Get(p => p.Comments.Any() && p.IsPublic && p.IsAboutMe == false, o => o.OrderByDescending(c => c.Comments.Count()), "Comments,Image").Take(10);
            }
            return model;
        }
    }
}
