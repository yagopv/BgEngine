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

using StructureMap;

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.Repositories;
using BgEngine.Domain.EntityModel;
using BgEngine.Domain.DatabaseContracts;
using BgEngine.Infraestructure.DatabaseInitialization;
using BgEngine.Infraestructure.UnitOfWork;
using BgEngine.Application.Services;
using BgEngine.Infraestructure.Validation;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.DependencyResolution {
    /// <summary>
    /// Dependency resolution container. Here  make the mappings os the different contracts 
    /// towards the related objects
    /// </summary>
    public static class IoC {
        /// <summary>
        /// Initialize mappings
        /// </summary>
        /// <returns>A container</returns>
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            // Service dependencies
                            x.For<IBlogServices>().HttpContextScoped().Use<BlogServices>();
                            x.For<IAccountServices>().HttpContextScoped().Use<AccountServices>();
                            x.For<IMediaServices>().HttpContextScoped().Use<MediaServices>();
                            x.For<IStatsServices>().HttpContextScoped().Use<StatsServices>();
                            x.For<IService<Category>>().HttpContextScoped().Use<Service<Category>>();
                            x.For<IService<Rating>>().HttpContextScoped().Use<Service<Rating>>();
                            x.For<IService<Image>>().HttpContextScoped().Use<Service<Image>>();
                            x.For<IService<Album>>().HttpContextScoped().Use<Service<Album>>();
                            x.For<IService<User>>().HttpContextScoped().Use<Service<User>>();
                            x.For<IService<Role>>().HttpContextScoped().Use<Service<Role>>();
                            x.For<IService<Tag>>().HttpContextScoped().Use<Service<Tag>>();
                            x.For<IService<Video>>().HttpContextScoped().Use<Service<Video>>();
                            x.For<IService<Post>>().HttpContextScoped().Use<Service<Post>>();
                            x.For<IService<Comment>>().HttpContextScoped().Use<Service<Comment>>();
                            x.For<IService<Rating>>().HttpContextScoped().Use<Service<Rating>>();
                            //Unit of work dependencies
                            x.For<IBlogUnitOfWork>().HttpContextScoped().Use<BlogUnitOfWork>();
                            //Repository dependencies
                            x.For<IPostRepository>().HttpContextScoped().Use<PostRepository>();
                            x.For<ICategoryRepository>().HttpContextScoped().Use<CategoryRepository>();
                            x.For<IImageRepository>().HttpContextScoped().Use<ImageRepository>();
                            x.For<ICommentRepository>().HttpContextScoped().Use<CommentRepository>();
                            x.For<IAlbumRepository>().HttpContextScoped().Use<AlbumRepository>();
                            x.For<IRepository<Album>>().HttpContextScoped().Use<AlbumRepository>();
                            x.For<IRepository<Image>>().HttpContextScoped().Use<ImageRepository>();
                            x.For<IVideoRepository>().HttpContextScoped().Use<VideoRepository>();
                            x.For<ITagRepository>().HttpContextScoped().Use<TagRepository>();
                            x.For<IRepository<User>>().HttpContextScoped().Use<Repository<User>>();
                            x.For<IRepository<Post>>().HttpContextScoped().Use<PostRepository>();
                            x.For<IRepository<Comment>>().HttpContextScoped().Use<CommentRepository>();
                            x.For<IRepository<Category>>().HttpContextScoped().Use<CategoryRepository>();
                            x.For<IRepository<Role>>().HttpContextScoped().Use<Repository<Role>>();
                            x.For<IRepository<Tag>>().HttpContextScoped().Use<TagRepository>();
                            x.For<IRepository<Rating>>().HttpContextScoped().Use<Repository<Rating>>();
                            x.For<IRepository<Video>>().HttpContextScoped().Use<VideoRepository>();
                            x.For<IRepository<BlogResource>>().HttpContextScoped().Use<Repository<BlogResource>>();
                            //Database initialization dependencies
                            //You can change between:
                            //  - DatabaseInitialize (Create empty database wit admin user and roles)
                            //  - TestDatabaseInitialize (Create database with test data)
                            x.For<IDatabaseInitialize>().HttpContextScoped().Use<TestDatabaseInitialize>();
                            //Initialize validator
                            x.For<IEntityValidator>().HttpContextScoped().Use<EntityValidator>();
                            //Resources
                            x.For<IBlogResourceServices>().HttpContextScoped().Use<BlogResourceServices>();

                        });
            return ObjectFactory.Container;
        }
    }
}