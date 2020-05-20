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

        public Gestures Gestures { get; private set; }

        public bool Debug { get; set; }

        public int HorizontalInput { get; protected set; }

        public int VerticalInput { get; protected set; }

        public abstract void UpdateInputSystem();

        public BaseGameInput()
        {

            var gestures = GameObject.FindObjectsOfType<Gestures>();

            if (gestures.Length > 1)
            {

                UnityEngine.Debug.LogWarning("GameInput: Please make sure that just one 'BurnGames/IO/Gestures' component is on scene, all will be deleted to prevent errors");

                foreach (var gesture in gestures)
                {

                    MonoBehaviour.Destroy(gesture);

                }

            }

            if(gestures.Length < 1)
            {
                UnityEngine.Debug.LogWarning("GameInput: Gestures features won't be avaiable because 'BurnGames/IO/Gestures' component isn't on scene");
            }

            if (gestures.Length == 1)
            {

                Gestures = gestures.First();

                var events = MonoBehaviour.FindObjectOfType<EventSystem>();

                if(events == null)
                {
                    Gestures.gameObject.AddComponent<EventSystem>();
                }

                var inputs = GameObject.FindObjectOfType<BaseInputModule>();

                if (inputs == null)
                {
                    Gestures.gameObject.AddComponent<StandaloneInputModule>();
                }

            }

        }

    }

}
