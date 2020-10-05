using AutomatedCar.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AutomatedCar.SystemComponents
{
    public class SteeringController
    {
        private const int WheelBaseInPixels = 130;
        private const double SteeringWheelConversionConstant = 0.6;

        private Vector2 CarLocation
        {
            get => new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
        }

        private double CarCurrentAngle
        {
            get => World.Instance.ControlledCar.Angle;
        }

        public double SteeringWheelAngle
        {
            get => this.SteeringWheelAngle;
            set => this.SteeringWheelAngle = value * SteeringWheelConversionConstant;
        }

        private double Velocity
        {
            get => World.Instance.ControlledCar.Speed;
        }

        private Vector2 FrontWheel { get; set; }

        private Vector2 BackWheel { get; set; }

        public Vector2
    }
}
