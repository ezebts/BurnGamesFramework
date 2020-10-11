using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.DependencyInjection
{
    /// <summary>
    /// Specifies Unity to inject an object instance of specific Type,
    /// it could be a class or even an abstract Type. 
    /// </summary>
    public class Injectable : PropertyAttribute
    {

        public string DisplayName { get; set; } = null;

        public Type InjectedType { get; private set; }
        
        public bool IsValid(object reference)
        {
            return InjectedType.IsInstanceOfType(reference);
        }

        public Injectable(Type injectable)
        {
            InjectedType = injectable;
        }

    }

}