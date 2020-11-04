namespace AutomatedCar
{
    using System;
    using System.Linq;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Input;

    public class Game : GameBase
    {
        private readonly World world;

        public Game(World world)
        {
            this.world = world;
        }

        private Random Random { get; } = new Random();

        protected override void Tick()
        {
            this.world.ControlledCar.InputHandler();
            World.Instance.Tick();
        }
    }
}