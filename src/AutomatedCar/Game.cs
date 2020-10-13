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
            int speed = 10;

            if (Keyboard.IsKeyDown(Key.Space))
            {
                speed = 300;
            }

            if (Keyboard.IsKeyDown(Key.Up))
            {
                this.world.ControlledCar.MoveY(-speed);
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {
                this.world.ControlledCar.MoveY(speed);
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                this.world.ControlledCar.MoveX(-speed);
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                this.world.ControlledCar.MoveX(speed);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                this.world.ControlledCar.Angle += 5;
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                this.world.ControlledCar.Angle -= 5;
            }

            World.Instance.Tick();
        }
    }
}