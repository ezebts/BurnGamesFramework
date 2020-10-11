using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BurnGames.DependencyInjection;

namespace BurnGames.DataAccess
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DataProviderAttribute : Attribute
    {

        public Type DataProvider { get; private set; }
        
        public DataProviderAttribute(Type providerType)
        {
            DataProvider = providerType;
        }

    }
}
