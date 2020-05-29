using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.Events
{

    /// <summary>
    /// 
    /// Game event system based on scriptable objects
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "BurnGames/Events/Game Event", fileName = "New Game Event")]
    public class GameEvent : ScriptableObject
    {

        private List<GameEventListener> listeners = new List<GameEventListener>();

        [SerializeField] bool enabled = true;

        public bool Enabled { get => enabled; set => enabled = value; }

        public void AddListener(GameEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void RemoveListener(GameEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }

        public void Invoke()
        {
            if (Enabled)
            {

                foreach(var listener in listeners)
                {
                    listener.EventActions.Invoke();
                }

            }

        }

    }

}