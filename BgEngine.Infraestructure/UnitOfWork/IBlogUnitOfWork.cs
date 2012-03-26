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

using System.Data.Entity;
using BgEngine.Domain.EntityModel;

namespace BgEngine.Infraestructure.UnitOfWork
{
    /// <summary>
    /// This is a contract for the Blog Context Unit Of Work,
    /// you can use this contract for implementing the real
    /// dependency to your O/RM or, for creating a mock... 
    /// Also, setting this abstraction adds more information about 
    /// the existent sets in non generic repository methods.
    /// 
    /// This is not the contract for switching  
    /// your persistent infrastructure, of course....
    /// </summary>
    public interface IBlogUnitOfWork : IQueryableUnitOfWork
    {
        IDbSet<Post> Posts {get; }
        IDbSet<User> Users {get; }
        IDbSet<Role> Roles {get; }
        IDbSet<Category> Categories {get; }
        IDbSet<Image> Images {get; }
        IDbSet<Comment> Comments { get; }
        IDbSet<Tag> Tags { get; }
        IDbSet<Album> Albums { get; }
        IDbSet<Rating> Ratings { get; }
        IDbSet<Video> Videos { get; }
        IDbSet<BlogResource> BlogResources { get; }
    }
}
