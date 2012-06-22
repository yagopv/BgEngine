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


using BgEngine.Infraestructure.Security;

namespace BgEngine.Security.Services
{
    public static class CodeFirstRoleServices
    {
        public static bool IsUserInRole(string user, string role)
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            return rp.IsUserInRole(user, role);
        }

        public static string[] GetAllRoles()
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            return rp.GetAllRoles();
        }

        public static void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            rp.RemoveUsersFromRoles(usernames, rolenames);
        }

        public static void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            rp.AddUsersToRoles(usernames, rolenames);
        }

        public static string[] GetUsersInRole(string role)
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            return rp.GetUsersInRole(role);
        }

        public static bool RoleExist(string role)
        {
            CodeFirstRoleProvider rp = new CodeFirstRoleProvider();
            return rp.RoleExists(role);
        }
    }
}
