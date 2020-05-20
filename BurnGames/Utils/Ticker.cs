using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace BurnGames.Utils
{

    /// <summary>
    /// 
    /// A method to be excecuted on every ticker cycle
    /// 
    /// </summary>
    /// <param name="ticker">Ticker instance that handles the method</param>
    public delegate void TickedAction(Ticker ticker);

    /// <summary>
    /// 
    /// Represents a periodic tick, it's just a clock used to
    /// control ticker's method behabiour. Ticks are used inside Ticker instances.
    /// 
    /// </summary>
    [Serializable]
    public class Tick : ITicker
    {

        [SerializeField] private float duration;

        public float Duration { get => duration; set => duration = value; }

        public float? StartTime { get; private set; }

        public float? EndTime { get; private set; }

        public float Elapsed { get; private set; } = 0;

        public bool Finish { get; private set; }

        public bool Starting { get; private set; }

        public bool Stopped { get; private set; } = false;

        public bool Finished => Started && (EndTime != null);
        public bool Started => StartTime != null;
        public bool Running => !Stopped && Started && (Elapsed < Duration);

        public float ElapsedNormalized => Elapsed / Duration;

        private void UpdateTick(float time)
        {

            Finish = false;

            if (Stopped && Finished)
            {

                Stopped = false;

            }

            if (!Stopped && !Finished)
            {

                if (Running)
                {

                    Elapsed += time;

                }

                else if (Started && EndTime == null)
                {

                    End();

                }

            }

            Starting = false;

        }

        public void DoTick()
        {
            UpdateTick(Time.deltaTime);
        }

        public void DoFixedTick()
        {
            UpdateTick(Time.fixedDeltaTime);
        }

        public void ResetState()
        {

            StartTime = null;

            EndTime = null;

            Elapsed = 0;

            Stopped = false;

            Starting = false;

            Finish = false;

        }

        public void Start()
        {

            if (duration < 0)
            {
                duration = 0;
            }

            ResetState();

            StartTime = Time.time;

            Starting = true;

        }

        public void Stop()
        {

            Stopped = true;

        }

        public void End()
        {

            Elapsed = Duration;

            EndTime = Time.time;

            Finish = true;

        }

        public void Continue()
        {
            Stopped = false;
        }

        public Tick()
        {
            StartTime = null;
        }

    }

    /// <summary>
    /// 
    /// This class is used to perform update or fixed update cycles
    /// using a coroutine-like method used to control the behabiour of
    /// every tick in the ticker.
    /// 
    /// </summary>
    [Serializable]
    public class Ticker : ITicker
    {

        [SerializeField] private List<Tick> ticks = new List<Tick>();

        TickedAction action;

        private bool started = false;

        private void UpdateTick(Func<Tick, Action> useTick)
        {

            if (started)
            {

                action?.Invoke(this);

                bool tickRunning = false;

                for (int tickIndex = 0; tickIndex < ticks.Count; tickIndex++)
                {

                    var tick = ticks[tickIndex];

                    if (tick != null)
                    {

                        if (!tick.Started)
                        {
                            tick.Start();
                            tickRunning = true;
                            break;
                        }

                        else if (!tick.Finished || tick.Finish)
                        {
                            useTick(tick).Invoke();
                            tickRunning = true;
                            break;
                        }

                    }

                }

                if (!tickRunning)
                {
                    End();
                }

            }

        }

        public void DoTick()
        {
            UpdateTick(tick => tick.DoTick);
        }

        public void DoFixedTick()
        {
            UpdateTick(tick => tick.DoFixedTick);
        }

        public void Start()
        {

            var firstTick = ticks.FirstOrDefault();

            foreach (var tick in ticks)
            {
                tick.ResetState();
            }

            if (firstTick != null)
            {
                firstTick.Start();
            }

            started = true;

        }

        public void End()
        {

            foreach (var tick in ticks)
            {
                tick.End();
            }

            started = false;

        }

        public Tick Tick(int tickNumber)
        {

            var tick = tickNumber <= 0 ? 1 : tickNumber;

            tick = tick - 1;

            if (tick > (ticks.Count - 1))
            {

                throw new ArgumentException($"Tick number {tickNumber} doesn't exist", nameof(tickNumber));

            }

            return ticks[tick];

        }

        public void AddTick(Tick tick)
        {
            ticks.Add(tick);
        }

        public void RemoveTick(Tick tick)
        {
            ticks.Remove(tick);
        }

        /// <summary>
        /// 
        /// Sets method to be ticked
        /// 
        /// </summary>
        /// <param name="action">Method delegate to be executed on every ticker's cycle</param>
        public void SetAction(TickedAction action)
        {
            this.action = action;
        }

        public static Ticker operator +(Ticker ticker, Tick tick)
        {

            ticker.AddTick(tick);

            return ticker;

        }

        public static Ticker operator -(Ticker ticker, Tick tick)
        {

            ticker.RemoveTick(tick);

            return ticker;

        }

        public Ticker() { }

        public Ticker(TickedAction action) : this()
        {

            this.action = action;

        }

    }

}
