using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BurnGames.Events.Editor
{

    [CustomEditor(typeof(GameEvent))]
    public class GameEventInspector : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            var gameEvent = (GameEvent)target;

            if (GUILayout.Button("Invoke Event"))
            {
                gameEvent.Invoke();
            }

        }

    }

}
