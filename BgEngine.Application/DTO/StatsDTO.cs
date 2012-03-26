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

using BgEngine.Domain.EntityModel;

namespace BgEngine.Application.DTO
{
    /// <summary>
    /// DTO for Stats data transfer    
    /// </summary>
    public class StatsDTO
    {
        public int TotalPosts { get; set; }
        public int TotalComments { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRoles { get; set; }
        public int TotalTags { get; set; }
        public int TotalImages { get; set; }
        public int TotalAlbums { get; set; }
        public IEnumerable<Post> TopRatedPosts { get; set; }
        public IEnumerable<Post> MostVisitedPosts { get; set; }
        public IEnumerable<Post> MostCommentedPosts { get; set; }
        public IEnumerable<User> PostWritenByUser { get; set; }        
    }
}
