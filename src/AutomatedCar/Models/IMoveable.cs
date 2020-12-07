namespace AutomatedCar.Models
{
    using System;
    using System.Numerics;
    using Avalonia;

    public interface IMoveable
    {
        /// <summary>
        /// Gets and sets the mass in kg used for collision dynamic calculations.
        /// </summary>
        public int Mass { get; set; }

        public void Move(Vector2 with);
    }
}