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

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;


namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Video Entity
    /// </summary>
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IBlogUnitOfWork currentunitofwork;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitofwork">The current Unit of Work</param>
        public VideoRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) 
        {
            this.currentunitofwork = unitofwork;
        }

        /// <summary>
        /// Insert Video
        /// creates the Video with current timestamp
        /// </summary>
        /// <param name="entity">Video to Insert</param>
        public override void Insert(Video entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Insert(entity);
        }

        /// <summary>
        /// Add Tags to the selected Video
        /// </summary>
        /// <param name="video">The Post</param>
        /// <param name="tags">The Tags identities for add to the Collection of Video</param>
        public void AddTagsToVideo(Video video, int[] tags)
        {
            if (tags == null)
            {
                video.Tags = new List<Tag>();
                return;
            }
            else if (video.Tags == null)
            {
                video.Tags = new List<Tag>();
            }
            var selectedTagsHS = new HashSet<int>(tags);
            var videoTags = new HashSet<int>(video.Tags.Select(t => t.TagId));
            foreach (Tag tag in currentunitofwork.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagId))
                {
                    if (!videoTags.Contains(tag.TagId))
                    {
                        video.Tags.Add(tag);
                    }
                }
                else
                {
                    if (videoTags.Contains(tag.TagId))
                    {
                        video.Tags.Remove(tag);
                    }
                }
            }
        }
    }
}
