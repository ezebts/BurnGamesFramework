using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnGames.IO.GestureRecognition
{
    public class GestureMatchEventArgs : EventArgs
    {
        public string MatchName { get; private set; }

        public float MatchedPercent { get; private set; }

        public GestureMatchEventArgs(GestureMatch match)
        {
            MatchName = match.Name;
            MatchedPercent = match.Score;
        }
    }

    public class GestureMatch
    {
        public string Name { get; set; }
        public float Score { get; set; }
    }
}