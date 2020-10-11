using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.Events
{

    /// <summary>
    /// Base game event handler
    /// </summary>
    /// <param name="e">Triggered event instance</param>
    public delegate void GameEventHandler(GameEvent e); 

    /// <summary>
    /// 
    /// Game event system based on scriptable objects
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "BurnGames/Events/Game Event", fileName = "New Game Event")]
    public class GameEvent : ScriptableObject
    {

        private List<GameEventListener> listeners = new List<GameEventListener>();

        private List<GameEventHandler> listenersHandlers = new List<GameEventHandler>();

        [SerializeField] bool enabled = true;

        public bool Enabled { get => enabled; set => enabled = value; }

        public void AddListener(GameEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void AddListener(GameEventHandler eventHandler)
        {
            listenersHandlers.Add(eventHandler);
        }

        public void RemoveListener(GameEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }

        public void RemoveListener(GameEventHandler eventHandler)
        {
            listenersHandlers.Remove(eventHandler);
        }

        public void Invoke()
        {

            if (Enabled)
            {

                foreach(var listener in listeners)
                {
                    listener.EventActions.Invoke();
                }

                foreach (var listener in listenersHandlers)
                {
                    listener.Invoke(this);
                }

            }

        }

    }

}