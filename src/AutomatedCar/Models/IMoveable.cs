namespace AutomatedCar.Models
{
    using System;
    using System.Numerics;
    using Avalonia;

    public interface IMoveable
    {
        public void SetNextPosition(int x, int y);

        public void Move(Vector2 with);

        public void MoveX(int x);
        public void MoveY(int y);
    }
}