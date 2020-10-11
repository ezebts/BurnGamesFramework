using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace BurnGames.Events
{

    [AddComponentMenu("BurnGames/Events/Game Event Listener", 1)]
    public class GameEventListener : MonoBehaviour
    {

        [SerializeField] GameEvent _event = null;

        [SerializeField] private UnityEvent eventActions = new UnityEvent();

        public UnityEvent EventActions => eventActions;

        void OnEnable()
        {
            _event.AddListener(this);
        }

        void OnDisable()
        {
            _event.RemoveListener(this);
        }

    }

}
