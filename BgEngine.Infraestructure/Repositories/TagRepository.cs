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
using System.Linq;
using System.Data.Entity;

using BgEngine.Domain.EntityModel;
using BgEngine.Domain.RepositoryContracts;
using BgEngine.Infraestructure.UnitOfWork;

namespace BgEngine.Infraestructure.Repositories
{
    /// <summary>
    /// Implement a repository for non-generic methods for the Tag Entity
    /// </summary>
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
         IBlogUnitOfWork currentunitofwork;

         /// <summary>
         /// ctor
         /// </summary>
         /// <param name="unitofwork">The current Unit of Work</param>
         public TagRepository(IBlogUnitOfWork unitofwork) : base(unitofwork) 
         {
             this.currentunitofwork = unitofwork;
         }

         /// <summary>
         /// Get all the Tags and the Post counter for the Posts are related with any of them
         /// </summary>
         /// <returns>A Dictionary with TagName and related Post counter</returns>
         public IDictionary<string, int> GetTagCount(bool ispremium)
         {
             IDictionary<string, int> tagsCount = new Dictionary<string, int>();
             ICollection<Tag> allTags = currentunitofwork.Tags.Include(t => t.Posts).ToList();
             foreach (var tag in allTags)
             {
                 if (tag.Posts.Any())
                 {
                     if (ispremium)
                     {
                         tagsCount.Add(tag.TagName, tag.Posts.Count());
                     }
                     else
                     {
                         if (tag.Posts.Any(p => p.IsPublic))
                         {
                             tagsCount.Add(tag.TagName, tag.Posts.Count(p => p.IsPublic));
                         }                         
                     }                    
                 }                 
             }
             return tagsCount;
         }
    }
}