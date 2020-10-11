using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace BurnGames.IO.GestureRecognition
{
    public interface IGestureBackend
    {
        GameObject GameObject { get; set; }
    }

    public static class GestureFactory
    {
        public static IGestureBackend GetBackend()
        {
            IGestureBackend Gestures = null;

            var gestures = MonoBehaviour.FindObjectsOfType(typeof(IGestureBackend));

            if (gestures.Length > 1)
            {

                UnityEngine.Debug.LogWarning("GameInput: Please make sure that just one 'BurnGames/IO/Gestures' component is on scene, all will be deleted to prevent errors");

                foreach (var gesture in gestures)
                {

                    MonoBehaviour.Destroy(gesture);

                }

            }

            if (gestures.Length < 1)
            {
                UnityEngine.Debug.LogWarning("GameInput: Gestures features won't be avaiable because 'BurnGames/IO/Gestures' component isn't on scene");
            }

            if (gestures.Length == 1)
            {

                Gestures = gestures.First() as IGestureBackend;

                var events = MonoBehaviour.FindObjectOfType<EventSystem>();

                if (events == null)
                {
                    Gestures.GameObject.AddComponent<EventSystem>();
                }

                var inputs = MonoBehaviour.FindObjectOfType<BaseInputModule>();

                if (inputs == null)
                {
                    Gestures.GameObject.AddComponent<StandaloneInputModule>();
                }

            }

            return Gestures;
        }
    }
}
