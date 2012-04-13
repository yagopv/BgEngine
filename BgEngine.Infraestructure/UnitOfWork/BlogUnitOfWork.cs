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

using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using BgEngine.Domain.EntityModel;
using BgEngine.Infraestructure.EFExtensions;
using System.Data;

namespace BgEngine.Infraestructure.UnitOfWork
{
	/// <summary>
	/// This is the implementation of the Blog Context Unit Of Work
	/// Inherit from the Entity Framework DbContext 
	/// </summary>
	public class BlogUnitOfWork : DbContext, IBlogUnitOfWork
	{
		IDbSet<Post> posts;
		public IDbSet<Post> Posts
		{
			get
			{
				if (posts == null)
					posts = base.Set<Post>();

				return posts;
			}
		}

		IDbSet<User> users;
		public IDbSet<User> Users
		{
			get
			{
				if (users == null)
					users = base.Set<User>();

				return users;
			}
		}

		IDbSet<Role> roles;
		public IDbSet<Role> Roles
		{
			get
			{
				if (roles == null)
					roles = base.Set<Role>();

				return roles;
			}
		}

		IDbSet<Category> categories;
		public IDbSet<Category> Categories
		{
			get
			{
				if (categories == null)
					categories = base.Set<Category>();

				return categories;
			}
		}

		IDbSet<Image> images;
		public IDbSet<Image> Images
		{
			get
			{
				if (images == null)
					images = base.Set<Image>();

				return images;
			}
		}

		IDbSet<Comment> comments;
		public IDbSet<Comment> Comments
		{
			get
			{
				if (comments == null)
					comments = base.Set<Comment>();

				return comments;
			}
		}

		IDbSet<Tag> tags;
		public IDbSet<Tag> Tags
		{
			get
			{
				if (tags == null)
					tags = base.Set<Tag>();

				return tags;
			}
		}

		IDbSet<Album> albums;
		public IDbSet<Album> Albums
		{
			get
			{
				if (albums == null)
					albums = base.Set<Album>();

				return albums;
			}
		}

		IDbSet<Rating> ratings;
		public IDbSet<Rating> Ratings
		{
			get
			{
				if (ratings == null)
					ratings = base.Set<Rating>();

				return ratings;
			}
		}

		IDbSet<Video> videos;
		public IDbSet<Video> Videos
		{
			get
			{
				if (videos == null)
					videos = base.Set<Video>();

				return videos;
			}
		}

        IDbSet<BlogResource> blogresources;
        public IDbSet<BlogResource> BlogResources
        {
            get
            {
                if (blogresources == null)
                    blogresources = base.Set<BlogResource>();

                return blogresources;
            }
        }

		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class 
		{
			return base.Set<TEntity>();
		}

		public void Attach<TEntity>(TEntity entity) where TEntity: class
		{
			if (base.Entry<TEntity>(entity).State == EntityState.Detached)
			{
				base.Set<TEntity>().Attach(entity);
			}            
		}

		public void Commit()
		{
			base.SaveChanges();
		}

		public void SetModified<TEntity>(TEntity entity) where TEntity : class
		{
			base.Entry<TEntity>(entity).State = EntityState.Modified;
		}

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
       
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            this.Configuration.LazyLoadingEnabled = false;

			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Entity<Comment>()
					.HasMany(c => c.RelatedComments)
					.WithMany()
					.Map( map => map.ToTable("RelatedComments"));

			modelBuilder.Entity<Video>()
					.PropertyString<Video>("Type");

            modelBuilder.Entity<Post>().Property(po => po.Text).IsMaxLength();
		}

		public new void Dispose()
		{
			base.Dispose();
		}


	}
}