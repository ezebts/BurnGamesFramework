using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.DependencyInjection
{

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