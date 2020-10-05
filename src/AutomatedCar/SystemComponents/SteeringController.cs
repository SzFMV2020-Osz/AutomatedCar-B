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

        public double SteeringAngle
        {
            get => this.SteeringAngle;
            set => this.SteeringAngle = value * SteeringWheelConversionConstant;
        }

        private double Velocity
        {
            get => World.Instance.ControlledCar.Speed;
        }

        private Vector2 FrontWheel { get; set; }

        private Vector2 BackWheel { get; set; }

        public void SetWheelPositions()
        {
            this.FrontWheel = Vector2.Multiply(this.CarLocation.Length() + (WheelBaseInPixels / 2), new Vector2((float)Math.Cos(this.SteeringAngle), (float)Math.Sin(this.SteeringAngle)));
            this.BackWheel = Vector2.Multiply(this.CarLocation.Length() - (WheelBaseInPixels / 2), new Vector2((float)Math.Cos(this.SteeringAngle), (float)Math.Sin(this.SteeringAngle)));
        }
    }
}
