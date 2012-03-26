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

using System.ComponentModel.DataAnnotations;

namespace BgEngine.Domain.EntityModel
{
    /// <summary>
    /// Represents  User´s ratings
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        public int RatingId { get; set; }

        /// <summary>
        /// Value (0..10)
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The identity of the related Post
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Navigation Property for the related Post
        /// </summary>
        public virtual Post Post { get; set; }

    }
}