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
using System.Data.Entity;

using BgEngine.Domain.EntityModel;
using BgEngine.Infraestructure.Security;
using BgEngine.Infraestructure.UnitOfWork;

namespace BgEngine.Infraestructure.DatabaseInitialization
{
    /// <summary>
    /// Test data for testing purposes
    /// </summary>
    public class TestModelContextInit : DropCreateDatabaseAlways<BlogUnitOfWork>
    {
        protected override void Seed(BlogUnitOfWork context)
        {
            var gethash = System.Data.Entity.Infrastructure.EdmMetadata.TryGetModelHash(context);
            //Users
            User usera = new User { UserId = Guid.NewGuid(), Username = "user1", Password = CodeFirstCrypto.HashPassword("user1"), Email = "user1@bgengine.com", IsConfirmed=true, CreateDate=DateTime.Now };
            User userb = new User { UserId = Guid.NewGuid(), Username = "user2", Password = CodeFirstCrypto.HashPassword("user2"), Email = "user2@bgengine.com", IsConfirmed = true, CreateDate = DateTime.Now };
            context.Set<User>().Add(usera);
            context.Set<User>().Add(userb);
            context.SaveChanges();            
            User user = context.Set<User>().Where(u => u.Username == "user1").Single();
            User user2 = context.Set<User>().Where(u => u.Username == "user2").Single();

            //Tags
            Tag tag1 = new Tag { TagName = "tag1", TagDescription = "Test Tag 1" };
            Tag tag2 = new Tag { TagName = "tag2", TagDescription = "Test Tag 2" };
            Tag tag3 = new Tag { TagName = "tag3", TagDescription = "Test Tag 3" };
            Tag tag4 = new Tag { TagName = "tag4", TagDescription = "Test Tag 4" };

            //Roles
            CodeFirstRoleProvider provider = new CodeFirstRoleProvider();
            provider.CreateRole("admin");
            provider.CreateRole("user");
            provider.CreateRole("premium");
            provider.AddUsersToRoles(new string[] { "user1" , "user2" }, new string[] { "admin", "user","premium" });

            //Categories
            context.Set<Category>().Add(new Category
            {
                Name = "Category 1",
                Description = "Test Category 1",
                DateCreated = DateTime.Now
            });            
            context.Set<Category>().Add(new Category
            {
                Name = "Category 2",
                Description = "Test Category 2",
                DateCreated = DateTime.Now
            });
            context.Set<Category>().Add(new Category
            {
                Name = "Category 3",
                Description = "Test Category 3",
                DateCreated = DateTime.Now
            });
            context.Set<Category>().Add(new Category
            {
                Name = "Category 4",
                Description = "Test Category 4",
                DateCreated = DateTime.Now
            });
            context.Set<Category>().Add(new Category
            {
                Name = "Category 5",
                Description = "Test Category 5",
                DateCreated = DateTime.Now
            });
            context.Commit();

            Category category1 = context.Set<Category>().Where(c => c.Name == "Category 1").Single();
            Category category2 = context.Set<Category>().Where(c => c.Name == "Category 2").Single();
            Category category3 = context.Set<Category>().Where(c => c.Name == "Category 3").Single();
            Category category4 = context.Set<Category>().Where(c => c.Name == "Category 4").Single();
            Category category5 = context.Set<Category>().Where(c => c.Name == "Category 5").Single();

            // Posts
            context.Set<Post>().Add(new Post
            {
                Code = "first_post",
                DateCreated = DateTime.Now.AddYears(-1),
                IsPublic = false,
                IsHomePost = true,
                Title = "My First Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",
                User = user,
                Category = category1,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now.AddDays(-1), Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now.AddHours(-1), Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now.AddMonths(-1), Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now.AddDays(-15), Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                 Tags = new List<Tag> { tag1, tag2 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "second_post",
                DateCreated = DateTime.Now,
                IsPublic = true,
                IsHomePost = true,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Title = "My second Post",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category2,
                Tags = new List<Tag> { tag1, tag2 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "third_post",
                DateCreated = DateTime.Now.AddMonths(-2),
                IsPublic = true,
                IsHomePost = true,
                Title = "My third Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category3,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag3, tag2 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "forth_post",
                DateCreated = DateTime.Now,
                IsPublic = true,
                IsHomePost = true,
                Title = "My forth Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category4,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag2, tag4 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "fifth_post",
                DateCreated = DateTime.Now,
                IsPublic = true,
                IsHomePost = true,
                Title = "My fifth Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category5,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                 Tags = new List<Tag> { tag1, tag2 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "six_post",
                DateCreated = DateTime.Now.AddYears(-1),
                IsPublic = true,
                IsHomePost = true,
                Title = "My Six Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",
                User = user,
                Category = category2,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2}, 
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag1, tag3 }               
            });

            context.Set<Post>().Add(new Post
            {
                Code = "seven_post",
                DateCreated = DateTime.Now.AddDays(-5),
                IsPublic = true,
                IsHomePost = true,
                Title = "My seven Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category3,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag3, tag1 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "eight_post",
                DateCreated = DateTime.Now.AddMonths(-2),
                IsPublic = true,
                IsHomePost = true,
                Title = "My eight Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. Lorem ipsum dolor sit amet,",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",
                User = user,
                Category = category1,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag1, tag4 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "nine_post",
                DateCreated = DateTime.Now.AddDays(-6),
                IsPublic = true,
                IsHomePost = true,
                Title = "My nine Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category5,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                 Tags = new List<Tag> { tag1, tag3 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "ten_post",
                DateCreated = DateTime.Now.AddDays(-4),
                IsPublic = true,
                IsHomePost = true,
                Title = "My ten Post",
                Description = "Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category4,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag1, tag3 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "thirteen_post",
                DateCreated = DateTime.Now.AddDays(-3),
                IsPublic = true,
                IsHomePost = true,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed Integer nec odio. Praesent libero. Sed ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed Integer nec odio. Praesent libero. Sed ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed Integer nec odio. Praesent libero. Sed ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed Integer nec odio. Praesent libero. Sed ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed Integer nec odio. Praesent libero. Sed",
                Title = "My thirteen Post",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category3,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag4, tag1 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "eleven_post",
                DateCreated = DateTime.Now.AddDays(-2),
                IsPublic = true,
                IsHomePost = true,
                Title = "My eleven Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sodales ligula in libero. ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category1,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag1, tag4 }
            });

            context.Set<Post>().Add(new Post
            {
                Code = "twelve_post",
                DateCreated = DateTime.Now.AddDays(-1),
                IsPublic = true,
                IsHomePost = true,
                Title = "My twelve Post",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed, Integer nec odio. Praesent libero. Sed  ",
                Text = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit</b>. Curabitur sodales ligula in libero. </p>" +
                       "<p>Sed dignissim lacinia nunc. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. <i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. </p>" +
                       "<p><i>Lorem ipsum dolor sit amet, consectetur adipiscing elit</i>. Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. </p>" +
                       "<p>Etiam ultrices. <b>Nam nec ante</b>. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. <b>Ut fringilla</b>. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. <b>Vestibulum sapien</b>. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. </p>" +
                       "<p>Morbi in dui quis est pulvinar ullamcorper. Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. <b>Curabitur sit amet mauris</b>. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. </p>",

                User = user,
                Category = category3,
                Comments = new List<Comment> {
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2 },
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2},
                    new Comment { DateCreated = DateTime.Now, Message= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user}
                },
                Tags = new List<Tag> { tag3, tag4 }
            });
            context.SaveChanges();

            Comment comment1 = context.Set<Comment>().Where(c => c.Post.Code == "twelve_post").First();
            comment1.RelatedComments.Add(new Comment { DateCreated = DateTime.Now, Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2, Post = comment1.Post, isRelatedComment = true});
            comment1.RelatedComments.Add(new Comment { DateCreated = DateTime.Now, Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2, Post = comment1.Post, isRelatedComment = true });
            comment1.RelatedComments.Add(new Comment { DateCreated = DateTime.Now, Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Lorem ipsum dolor sit amet, consectetur adip", User = user2, Post = comment1.Post, isRelatedComment = true });
            context.SaveChanges();

            // Create indexes
            context.Database.ExecuteSqlCommand("CREATE INDEX IDX_Posts_Code ON Posts (Code);");
            context.Database.ExecuteSqlCommand("CREATE INDEX IDX_Posts_DateCreated ON Posts (DateCreated DESC);");

            //Resources
            context.Set<BlogResource>().Add(new BlogResource { Name = "Admin_Role", Value = "admin" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Categories_Number_of_Categories_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Comments_Number_of_Comments_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Copyright", Value = "©MyCopyright 2XXX" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Dark_Background_Themes", Value = "" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Dark_Header_Themes", Value = "Aristo;Rocket;Cobalt" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Default_Theme", Value = "Cobalt" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Directories_Temp_Data", Value = "~/Content/Files/Temp_Data/" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_Password", Value = "xxxxxx" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_UserName", Value = "admin@mydomain.com" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Google_Analytics_Track_Code", Value = "xx-xxxxxxxx-x" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Index_Number_of_Posts", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Logo", Value = "~/Content/Icons/logo.jpg" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "No_Image", Value = "~/Content/Icons/no_image.jpg" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Posts_Number_of_Posts_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Premium_Role", Value = "premium" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Private_Key_Http", Value = "6LdofcQSAAAAAMGb119SbYgX18pwtPnPbBNk1tMH" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Private_Key_localhost", Value = "6LdqfcQSAAAAAKqgoeIUNlYbOJFHM3rhia2kpbRW" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Public_Key_Http", Value = "6LdofcQSAAAAAEw0fhwatfkTAJJlYEyjlCTrgKKm" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Recaptcha_Public_Key_localhost", Value = "6LdqfcQSAAAAAHP4vF9uTfxmao2C4e8oYOoT1_dK" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Roles_Number_of_Roles_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SearchImages_Number_of_Images_per_Page", Value = "15" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SearchVideos_Number_of_Videos_per_Page", Value = "12" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SiteTitle", Value = "MySiteName.com" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "SiteUrl", Value = "http://www.MySiteDomain.com" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Smtp_Port", Value = "25" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Smtp_Server", Value = "smtp.live.com" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Email_SSL", Value = "true" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Tags_Number_of_Tags_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "ThumbnailHeight", Value = "150" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "ThumbnailWidth", Value = "200" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Users_Number_of_Users_per_Page", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Video_Container_Height", Value = "160" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Video_Container_Width", Value = "250" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Posts_HomeIndexPostsPerPage", Value = "10" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Twitter_User", Value = "" });
            context.Set<BlogResource>().Add(new BlogResource { Name = "Twitter_Search_Query", Value = "" });
            context.SaveChanges();
        }
    }
}