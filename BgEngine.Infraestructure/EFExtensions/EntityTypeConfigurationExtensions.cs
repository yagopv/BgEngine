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
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace BgEngine.Infraestructure.EFExtensions
{
    /// <summary>
    /// Extensions methods for EntityTypeConfiguration
    /// </summary>
    public static class EntityTypeConfigurationExtensions
    {
        /// <summary>
        /// Extension method for map private navigation property
        /// <example>
        ///  modelBuilder.Entity<Customer>()
        ///              .HasMany<Customer, Order>("Orders");
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of principal entity</typeparam>
        /// <typeparam name="TTargetEntityType">The type of target entity (without ICollection{})</typeparam>
        /// <param name="entityConfiguration">Associated EntityTypeConfiguration</param>
        /// <param name="navigationPropertyName">The navigation property name</param>
        /// <returns>The ManyNavigationPropertyConfiguration for this map</returns>
        public static ManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> HasMany<TEntityType, TTargetEntityType>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string navigationPropertyName)
            where TEntityType : class
            where TTargetEntityType : class
        {
            var propertyInfo = typeof(TEntityType).GetProperty(navigationPropertyName, 
                                                              BindingFlags.NonPublic |
                                                              BindingFlags.Instance  |
                                                              BindingFlags.Public);

            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType, ICollection<TTargetEntityType>>> expression = (Expression<Func<TEntityType, ICollection<TTargetEntityType>>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.HasMany<TTargetEntityType>(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }
        /// <summary>
        /// Extension method for map inverse of many navigation property
        /// </summary>
        /// <typeparam name="TEntityType">The parent of parent in relation and the inverse in this method</typeparam>
        /// <typeparam name="TTargetEntityType">The target in many:required</typeparam>
        /// <param name="manyNavigationPropertyConfiguration">The navigation property configuration</param>
        /// <param name="navigationPropertyName">The property name</param>
        /// <returns>A DependentNavigationPropertyConfiguration for this many:required navigation </returns>
        public static DependentNavigationPropertyConfiguration<TTargetEntityType> WithRequired<TEntityType, TTargetEntityType>(this ManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> manyNavigationPropertyConfiguration, string navigationPropertyName)
            where TEntityType : class
            where TTargetEntityType : class
        {
            var propertyInfo = typeof(TTargetEntityType).GetProperty(navigationPropertyName,
                                                              BindingFlags.NonPublic |
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public);

            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TTargetEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TTargetEntityType, TEntityType>> expression = (Expression<Func<TTargetEntityType, TEntityType>>)Expression.Lambda(memberExpression, arg);


                return manyNavigationPropertyConfiguration.WithRequired(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }

        /// <summary>
        /// Extension method for map private properties
        /// <example>
        /// modelBuilder.Entity{Customer}()
        ///             .Property{Customer,int}("Age")
        ///             .IsOptional()
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity to map</typeparam>
        /// <typeparam name="KProperty">The type of private property to map</typeparam>
        /// <param name="entityConfiguration">Asociated EntityTypeConfiguration</param>
        /// <param name="propertyName">The name of private property</param>
        /// <returns>A PrimitivePropertyConfiguration for this map</returns>
        public static PrimitivePropertyConfiguration Property<TEntityType, KProperty>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string propertyName)
            where TEntityType : class
            where KProperty : struct
        {

            var propertyInfo = typeof(TEntityType).GetProperty(propertyName, BindingFlags.NonPublic
                                                                        |
                                                                        BindingFlags.Instance
                                                                        |
                                                                        BindingFlags.Public);
            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType, KProperty>> expression = (Expression<Func<TEntityType, KProperty>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.Property(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }
        /// <summary>
        /// Extension method for map private decimal properties
        /// <example>
        /// modelBuilder.Entity{Customer}()
        ///             .Property{Customer}("Amount")
        ///             .HasScale(10,10)
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity to map</typeparam>
        /// <param name="entityConfiguration">Asociated EntityTypeConfiguration</param>
        /// <param name="propertyName">The name of private decimal property</param>
        /// <returns>A DecimalPropertyConfiguration for this map</returns>
        public static DecimalPropertyConfiguration PropertyDecimal<TEntityType>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string propertyName)
            where TEntityType:class
        {
            var propertyInfo = typeof(TEntityType).GetProperty(propertyName, BindingFlags.NonPublic
                                                                        |
                                                                        BindingFlags.Instance
                                                                        |
                                                                        BindingFlags.Public);
            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType,decimal>> expression = (Expression<Func<TEntityType, decimal>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.Property(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }

        /// <summary>
        /// Extension method for map private datetime properties
        /// <example>
        /// modelBuilder.Entity{Customer}()
        ///             .Property{Customer}("Date")
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity to map</typeparam>
        /// <param name="entityConfiguration">Asociated EntityTypeConfiguration</param>
        /// <param name="propertyName">The name of private datetime property</param>
        /// <returns>A DateTimePropertyConfiguration for this map</returns>
        public static DateTimePropertyConfiguration PropertyDateTime<TEntityType>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string propertyName)
            where TEntityType : class
        {
            var propertyInfo = typeof(TEntityType).GetProperty(propertyName, BindingFlags.NonPublic
                                                                        |
                                                                        BindingFlags.Instance
                                                                        |
                                                                        BindingFlags.Public);
            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType, DateTime>> expression = (Expression<Func<TEntityType, DateTime>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.Property(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }
        /// <summary>
        /// Extension method for map private binary properties
        /// <example>
        /// modelBuilder.Entity{Customer}()
        ///             .Property{Customer}("Image")
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity to map</typeparam>
        /// <param name="entityConfiguration">Asociated EntityTypeConfiguration</param>
        /// <param name="propertyName">The name of private binary property</param>
        /// <returns>A BinaryPropertyConfiguration for this map</returns>
        public static BinaryPropertyConfiguration PropertyBinary<TEntityType>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string propertyName)
           where TEntityType : class
        {
            var propertyInfo = typeof(TEntityType).GetProperty(propertyName, BindingFlags.NonPublic
                                                                        |
                                                                        BindingFlags.Instance
                                                                        |
                                                                        BindingFlags.Public);
            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType, byte[]>> expression = (Expression<Func<TEntityType, byte[]>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.Property(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }

        /// <summary>
        /// Extension method for map private binary properties
        /// <example>
        /// modelBuilder.Entity{Customer}()
        ///             .Property{Customer}("Image")
        /// </example>
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity to map</typeparam>
        /// <param name="entityConfiguration">Asociated EntityTypeConfiguration</param>
        /// <param name="propertyName">The name of private binary property</param>
        /// <returns>A StringPropertyConfiguration for this map</returns>
        public static StringPropertyConfiguration PropertyString<TEntityType>(this EntityTypeConfiguration<TEntityType> entityConfiguration, string propertyName)
           where TEntityType : class
        {
            var propertyInfo = typeof(TEntityType).GetProperty(propertyName, BindingFlags.NonPublic
                                                                        |
                                                                        BindingFlags.Instance
                                                                        |
                                                                        BindingFlags.Public);
            if (propertyInfo != null) // if private property exists
            {
                ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "parameterName");
                MemberExpression memberExpression = Expression.Property((Expression)arg, propertyInfo);

                //Create the expression to map
                Expression<Func<TEntityType,string>> expression = (Expression<Func<TEntityType, string>>)Expression.Lambda(memberExpression, arg);


                return entityConfiguration.Property(expression);
            }
            else
                throw new InvalidOperationException("The property not exist");
        }
    }
}
