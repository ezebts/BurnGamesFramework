using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace BurnGames.IO
{

    public class GameMobileInput : BaseGameInput
    {

        #pragma warning disable 0414
        private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown = false;
        #pragma warning restore 0414

        private bool isDraging = false;

        private Vector2 startTouch, swipeDelta;

        void DesktopInputTesting()
        {

            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }

            else if(Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                ResetInputSystem();
            }

        }

        void ResetInputSystem()
        {

            startTouch = swipeDelta = Vector2.zero;
            isDraging = false;

        }

        public override void UpdateInputSystem()
        {

            tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

            HorizontalInput = 0;
            VerticalInput = 0;

            if (Debug)
            {
                DesktopInputTesting();
            }

            if (Input.touches.Length > 0)
            {

                var firstTouch = Input.touches.First();

                if (firstTouch.phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = firstTouch.position;
                }

                else if (firstTouch.phase == TouchPhase.Ended || firstTouch.phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    ResetInputSystem();
                }

            }

            swipeDelta = Vector2.zero;

            if(isDraging)
            {

                if(Input.touches.Length > 0)
                {

                    var firstTouch = Input.touches.First();

                    swipeDelta = firstTouch.position - startTouch;

                }

                else if (Debug && Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }

            }

            if(swipeDelta.magnitude > 15)
            {

                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if(x < 0)
                    {
                        swipeLeft = true;
                        HorizontalInput = -1;
                    }
                    else if(x > 0)
                    {
                        swipeRight = true;
                        HorizontalInput = 1;
                    }
                }

                else
                {

                    if(y > 0)
                    {
                        swipeDown = true;
                        VerticalInput = 1;
                    }

                    else if(y < 0)
                    {
                        swipeUp = true;
                        VerticalInput = -1;
                    }

                }

                ResetInputSystem();

            }

        }

    }

    public class GameMobileInputFactory : IGameInputFactory
    {

        public static GameMobileInputFactory Instance { get; } = new GameMobileInputFactory();

        public IGameInput GetGameInputInstance()
        {
            return new GameMobileInput();
        }

        GameMobileInputFactory()
        {
            GameInputContext.AddGameInputProvider("mobile", this);
        }

    }

}
