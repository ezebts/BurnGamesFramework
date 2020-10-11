using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;
using BurnGames.IO.GestureRecognition;

namespace BurnGames.IO
{
    /// <summary>
    /// 
    /// Base class for generic crossplatform input system with some useful
    /// features.
    /// 
    /// </summary>
    public abstract class BaseGameInput : IGameInput
    {
        public IGestureBackend Gestures { get; private set; }

        public bool Debug { get; set; }

        public int HorizontalInput { get; protected set; }

        public int VerticalInput { get; protected set; }

        public abstract void UpdateInputSystem();

        public BaseGameInput()
        {
            Gestures = GestureFactory.GetBackend();
        }
    }
}
