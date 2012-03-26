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

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Domain.EntityModel;
using BgEngine.Security.Services;
using BgEngine.Application.ResourceConfiguration;

namespace BgEngine.Application.Services
{
    /// <summary>
    /// Control application operations with User and Role entities
    /// </summary>
    public class AccountServices : IAccountServices
    {
        IRepository<User> UserRepository;
        IRepository<Role> RoleRepository;
        /// <summary>
        /// ctor
        /// </summary>
        public AccountServices(IRepository<User> userrepository, IRepository<Role> rolerepository)
        {
            this.UserRepository = userrepository;
            this.RoleRepository = rolerepository;
        }
        /// <summary>
        /// Create new User in Blog
        /// </summary>
        /// <param name="username">Name of the user</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="firstname">The FirstName</param>
        /// <param name="lastname">The Lastname</param>
        /// <param name="timezone">The Time Zone for the user</param>
        /// <param name="culture">Culture</param>
        /// <param name="requireconfirmation">Always true because the acccount is confirmed inmediately</param>
        /// <param name="selectedroles">Bunch of roles for the user being created</param>
        public void CreateAccount(string username, string password, string email, string firstname, string lastname, string timezone, string culture, bool requireconfirmation, string[] selectedroles)
        {
            var token = CodeFirstSecurity.CreateAccount(username, password, email, firstname, lastname, timezone, culture, requireconfirmation);
            if (selectedroles.Length == 0)
            {
                CodeFirstRoleServices.RemoveUsersFromRoles(new string[1] { username }, CodeFirstRoleServices.GetAllRoles());
            }
            else
            {
                CodeFirstRoleServices.RemoveUsersFromRoles(new string[1] { username }, CodeFirstRoleServices.GetAllRoles());
                CodeFirstRoleServices.AddUsersToRoles(new string[1] { username }, selectedroles);
            }
        }
        /// <summary>
        /// Add new entity to the context
        /// </summary>
        /// <param name="user">The entity to Add</param>
        public void CreateNew<TEntity>(TEntity entity)
        {
            if (typeof(TEntity) == typeof(User))
            {
                UserRepository.Insert(entity as User);
                UserRepository.UnitOfWork.Commit();
            }
            else
            {
                RoleRepository.Insert(entity as Role);
                RoleRepository.UnitOfWork.Commit();
            }
        }
        /// <summary>
        /// Save changes made in User
        /// </summary>
        /// <param name="role">The User to save</param>
        public void SaveUser(User user, string[] selectedroles)
        {
            UserRepository.Update(user);
            UserRepository.UnitOfWork.Commit();
            if (selectedroles.Length == 0)
            {
                CodeFirstRoleServices.RemoveUsersFromRoles(new string[1] { user.Username }, CodeFirstRoleServices.GetAllRoles());
            }
            else
            {
                CodeFirstRoleServices.RemoveUsersFromRoles(new string[1] { user.Username }, CodeFirstRoleServices.GetAllRoles());
                CodeFirstRoleServices.AddUsersToRoles(new string[1] { user.Username }, selectedroles);
            }
        }
        /// <summary>
        /// Save changes in entity
        /// </summary>
        /// <param name="role">The entity to save</param>
        public void Save<TEntity>(TEntity entity)
        {
            if (typeof(TEntity) == typeof(User))
            {
                UserRepository.Update(entity as User);
                UserRepository.UnitOfWork.Commit();
            }
            else
            {
                RoleRepository.Update(entity as Role);
                RoleRepository.UnitOfWork.Commit();
            }
        }
        /// <summary>
        /// Delete entity from the context
        /// </summary>
        /// <param name="user">The entity to delete</param>
        public void Delete<TEntity>(object id)
        {
            if (typeof(TEntity) == typeof(User))
            {
                UserRepository.Delete(id);
                UserRepository.UnitOfWork.Commit();
            }
            else
            {
                RoleRepository.Delete(id);
                RoleRepository.UnitOfWork.Commit();
            }
        }
        /// <summary>
        /// Get a User
        /// </summary>
        /// <param name="expression">An Expression to find the User</param>
        /// <returns>The selected User</returns>
        public User FindUser(Expression<Func<User,bool>> expression)
        {
             return UserRepository.Get(expression).FirstOrDefault();
        }
        /// <summary>
        /// Find a role
        /// </summary>
        /// <param name="expression">Expression to find the Role</param>
        /// <returns>The selected role</returns>
        public Role FindRole(Expression<Func<Role, bool>> expression)
        {
            return RoleRepository.Get(expression).FirstOrDefault();
        }
        /// <summary>
        /// Retrieve an user by identity
        /// </summary>
        /// <param name="id">The identity</param>
        /// <returns>The User</returns>
        public User FindUserByIdentity(object id)
        {
            return UserRepository.GetByID(id);
        }
        /// <summary>
        /// Retrieve a role by identity
        /// </summary>
        /// <param name="id">The identity</param>
        /// <returns>The Role</returns>
        public Role FindRoleByIdentity(object id)
        {
            return RoleRepository.GetByID(id);
        }
        /// <summary>
        /// Get paged Users
        /// </summary>
        /// <param name="sort">The field to be sorted by</param>
        /// <param name="pageIndex">The number of page to be retrieved</param>
        /// <param name="dir">If the direction must be asccending or descending</param>
        /// <returns>A Collection of Users</returns>
        public IEnumerable<User> RetrievePagedUsers(string sort, int pageIndex, bool dir)
        {
            if (sort == null)
            {
                return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.CreateDate, false);
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "username":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.Username, dir);
                    case "isconfirmed ":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.IsConfirmed, dir);
                    case "email":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.Email, dir);
                    case "createdate":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.CreateDate, dir);
                    case "passwordchangeddate":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.PasswordChangedDate, dir);
                    case "timezone":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.TimeZone, dir);
                    case "culture":
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.Culture, dir);
                    default:
                        return UserRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_UsersPerPage), u => u.CreateDate, false);
                }
            }
        }
        /// <summary>
        /// Retrieve an enumerable of paged roles
        /// </summary>
        /// <param name="sort">The field to be sorted</param>
        /// <param name="pageIndex">the index of the page</param>
        /// <param name="dir">The direction of the sorted roles (Ascending, descending)</param>
        /// <returns></returns>
        public IEnumerable<Role> RetrievePagedRoles(string sort, int pageIndex, bool dir)
        {
            if (sort == null)
            {
                return RoleRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_RolesPerPage), r => r.RoleName, false);
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "rolename":
                        return RoleRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_RolesPerPage), r => r.RoleName, dir);
                    case "description":
                        return RoleRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_RolesPerPage), r => r.Description, dir);
                    default:
                        return RoleRepository.GetPagedElements(pageIndex, Int32.Parse(BgResources.Pager_RolesPerPage), r => r.RoleName, false);
                }
            }
        }
        /// <summary>
        /// Get the total number of TEntity in the Database
        /// </summary>
        /// <typeparam name="TEntity">The Type of the entity</typeparam>
        /// <returns>The number of TEntity in database</returns>
        public int TotalNumberOf<TEntity>()
        {
            return typeof(TEntity) == typeof(User) ? UserRepository.GetCount() : RoleRepository.GetCount();
        }
        /// <summary>
        /// Delete an user. For user deletion, the user must not have any relation with Post ans Comments
        /// </summary>
        /// <param name="id">The User identity</param>
        public void DeleteUser(object id)
        {
            User user = UserRepository.GetByID(id);
            if (user.Posts.Any())
            {
                throw new OperationCanceledException(Resources.AppMessages.Error_User_With_Posts);
            }
            else
            {
                if (user.Comments.Any())
                {
                    throw new OperationCanceledException(Resources.AppMessages.Error_User_With_Comments);
                }
                else
                { 
                    UserRepository.Delete(id);
                    UserRepository.UnitOfWork.Commit();
                }
            }
        }
        /// <summary>
        /// Delete a role. For role deletion, the role must not have any relation with Users
        /// </summary>
        /// <param name="id">The User identity</param>
        public void DeleteRole(object id)
        {
            Role role = RoleRepository.GetByID(id);
            if (role.Users.Any())
            {
                throw new OperationCanceledException(Resources.AppMessages.Error_Role_With_Users);
            }
            else
            {
                UserRepository.Delete(id);
                UserRepository.UnitOfWork.Commit();
            }            
        }

    }

}
