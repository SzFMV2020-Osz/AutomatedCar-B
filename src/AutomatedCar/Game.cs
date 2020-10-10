namespace AutomatedCar
{
    using System;
    using System.Runtime.CompilerServices;
    using AutomatedCar.Models;
    using Avalonia.Input;

    public class Game : GameBase
    {
        private readonly World world;
        private TemporaryControlsForPowerTrain tempControl;

        public Game(World world)
        {
            this.world = world;
            this.tempControl = new TemporaryControlsForPowerTrain();
        }

        public World World { get => this.world; }

        private Random Random { get; } = new Random();

        protected override void Tick()
        {
            this.tempControl.ExecuteControls(); // Amig nincs HMI, hogy a PowerTrain tudja mozgatni az autot
        }
    }
}