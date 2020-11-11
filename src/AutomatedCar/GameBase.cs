namespace AutomatedCar
{
    using System;
    using Avalonia.Threading;

    public abstract class GameBase
    {
        public const int TicksPerSecond = 60;

        private readonly DispatcherTimer _timer =
            new DispatcherTimer() {Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond)};

        public long CurrentTick { get; private set; }

        void DoTick()
        {
            this.Tick();
            this.CurrentTick++;
        }

        protected abstract void Tick();

        protected GameBase()
        {
            this._timer.Tick += delegate { DoTick(); };
        }

        public void Start() => this._timer.IsEnabled = true;

        public void Stop() => this._timer.IsEnabled = false;
    }
}