namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.Visualization;
    using Avalonia.Input;
    using Avalonia;

    public static class ScreenPositioner
    {
        public static Vector CalcCameraPosition()
        {
            return new Vector(World.Instance.ControlledCar.X - (World.Instance.VisibleWidth / 2), World.Instance.ControlledCar.Y - (World.Instance.VisibleHeight / 2));
        }
    }
}