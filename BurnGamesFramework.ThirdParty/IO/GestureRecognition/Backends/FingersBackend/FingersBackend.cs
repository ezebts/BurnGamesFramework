using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BurnGames.IO.GestureRecognition;
using DigitalRubyShared;

namespace BurnGames.ThirdParty.IO.GestureRecognition.Backends.FingersBackend
{
    [AddComponentMenu("BurnGames/IO/FingersBackend", 1)]
    public class FingersBackend : FingersImageGestureHelperComponentScript
    {
        public event EventHandler<GestureMatchEventArgs> OnGestureMatched;

        [SerializeField] Camera renderLinesCamera;

        public Camera RenderLinesCamera
        {
            get => renderLinesCamera;
            set => renderLinesCamera = value;
        }

        public List<Gesture> LoadedGestures;

        public new void Awake()
        {

            GestureStateUpdated.AddListener(OnGestureUpdated);

            if(LoadedGestures != null)
            {
                foreach(var gesture in LoadedGestures)
                {
                    GestureImages.Add(GetGestureData(gesture));
                }
            }

            base.Awake();

            if(renderLinesCamera == null)
            {
                renderLinesCamera = Camera.main;
            }

        }

        private new void Update()
        {

            base.Update();

            if (LineRenderers != null && renderLinesCamera != null)
            {

                if (LineRenderers.Length > 0)
                {

                    foreach (var linerenderer in LineRenderers)
                    {
                        if (renderLinesCamera.transform.localPosition.z >= 0)
                        {
                            renderLinesCamera.transform.localPosition = renderLinesCamera.transform.localPosition - renderLinesCamera.transform.forward;
                        }
                    }

                    if (renderLinesCamera.depth > 0)
                    {
                        renderLinesCamera.depth = -renderLinesCamera.depth;
                    }

                    else if(renderLinesCamera.depth == 0)
                    {
                        renderLinesCamera.depth = -1;
                    }

                    if (!renderLinesCamera.orthographic)
                    {
                        renderLinesCamera.orthographic = true;
                    }

                }

            }

        }

        void OnGestureUpdated(DigitalRubyShared.GestureRecognizer gesture)
        {

            GestureCallback(gesture);

            if (gesture.State == GestureRecognizerState.Ended)
            {

                var match = CheckForImageMatch();

                if (match != null && OnGestureMatched != null)
                {

                    OnGestureMatched(this, new GestureMatchEventArgs(new GestureMatch() { Name = match.Name, Score = match.Score * 100 }));

                }

                if (match == null)
                {
                    ClearLineRenderers();
                    Gesture.Reset();
                }

            }
        
        }

        private void ClearLineRenderers()
        {

            foreach (LineRenderer lineRenderer in LineRenderers)
            {
                lineRenderer.positionCount = 0;
            }
            if (LinesCleared != null)
            {
                LinesCleared.Invoke(this, System.EventArgs.Empty);
            }

        }

        protected override void LateUpdate()
        {

            base.LateUpdate();

        }

        public new void Reset()
        {

            if(LineRenderers == null)
            {
                LineRenderers = new LineRenderer[0];
            }

            if(Gesture != null)
            {
                base.Reset();
            }

        }

        public ImageGestureRecognizerComponentScriptImageEntry GetGestureData(Gesture gesture)
        {
            return new ImageGestureRecognizerComponentScriptImageEntry()
            {
                Key = gesture.Key,
                ScorePadding = gesture.ScorePadding,
                Images = gesture.Images
            };
        }
    }
}
