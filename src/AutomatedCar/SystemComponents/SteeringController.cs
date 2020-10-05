namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System.Numerics;

    public class SteeringController
    {
        private const int WheelBaseInPixels = 156;
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
    }
}
