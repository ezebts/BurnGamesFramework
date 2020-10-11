using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.DependencyInjection
{

    public static class ObjectExtensions
    {

        /// <summary>
        /// Creates an instance using the default constructor if possible and returns it as
        /// a specific TInstance type.
        /// </summary>
        /// <typeparam name="TInstance">Type to cast instance to</typeparam>
        /// <param name="type">Type instance to be constructed</param>
        /// <returns>New instance casted as TInstance type or default value</returns>
        public static TInstance InstantiateAs<TInstance>(this Type type)
        {

            try
            {
                return (TInstance)type.GetConstructor(new Type[0]).Invoke(new object[0]);
            }

            catch (NullReferenceException) { }

            catch (InvalidCastException) { }

            return default;

        }

        /// <summary>
        /// Used to get a single custom attribute of type TAttribute
        /// </summary>
        /// <typeparam name="TAttribute">Attribute's type to look for</typeparam>
        /// <param name="inherit">Specifies search on childrens</param>
        /// <returns>Finded TAttribute instance or default</returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this Type type, bool inherit = true)
        {

            var attr = type.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault();

            if(attr != null)
            {
                return (TAttribute)attr;
            }
            else
            {
                return default;
            }

        }

    }

}