namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
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
            world.ControlledCar.InputHandler();
            World.Instance.Tick();
        }
    }
}