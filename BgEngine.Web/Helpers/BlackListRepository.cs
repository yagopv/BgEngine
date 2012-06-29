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
using System.Web;
using System.IO;

namespace BgEngine.Web.Helpers
{
    public class BlackListRepository
    {
        public static IQueryable<string> Ips {get; set;}

        public static void  GetAllIpsInBlackList(HttpServerUtility server)
        {
            string reader = File.ReadAllText(server.MapPath(Resources.AppConfiguration.BlackListIpFile));
            Ips = reader.Split(new char[] { ';' }).AsQueryable();
        }
    }
}