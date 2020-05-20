using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using DigitalRubyShared;

namespace BurnGames.IO.GestureRecognition
{

    [CreateAssetMenu(menuName = "BurnGames/IO/Gestures", fileName = "new-gesture")]
    public class Gesture : ScriptableObject
    {

        [Tooltip("Key")]
        public string Key;

        [Tooltip("Score padding, makes it easier to match")]
        [Range(0.0f, 0.5f)]
        public float ScorePadding;

        [Range(0.0f, 100.0f)]
        public float MinPercentMatch;

        [Tooltip("This field is just in case you need to get an image that represents the gesture")]
        public Texture gestureTexture;

        [TextArea(1, 8)]
        [Tooltip("Comma separated list of hex format ulong for each row, separated by newlines.")]
        public string Images;

        public ImageGestureRecognizerComponentScriptImageEntry GetGestureData()
        {

            return new ImageGestureRecognizerComponentScriptImageEntry()
            {
                Key = Key,
                ScorePadding = ScorePadding,
                Images = Images
            };

        }

    }

}
