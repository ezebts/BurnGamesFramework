using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BurnGames.IO.GestureRecognition;

namespace BurnGames.IO
{

    /// <summary>
    /// 
    /// A generic crossplatform input system with some useful
    /// features.
    /// 
    /// </summary>
    public interface IGameInput
    {

        int HorizontalInput { get; }

        int VerticalInput { get; }

        Gestures Gestures { get; }

        bool Debug { get; set; }

        void UpdateInputSystem();

    }

}
