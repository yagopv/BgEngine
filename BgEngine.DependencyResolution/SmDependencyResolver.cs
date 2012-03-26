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
using System.Web.Mvc;
using StructureMap;

namespace BgEngine.DependencyResolution
{
    /// <summary>
    /// Class for the resolution of the diferent requests
    /// </summary>
    public class SmDependencyResolver : IDependencyResolver {

        private readonly IContainer _container;

        public SmDependencyResolver(IContainer container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType == null) return null;
            try {
                  return serviceType.IsAbstract || serviceType.IsInterface
                           ? _container.TryGetInstance(serviceType)
                           : _container.GetInstance(serviceType);
            }
            catch {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}