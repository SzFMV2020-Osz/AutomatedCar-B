namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Numerics;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Markup.Xaml.Templates;

    public class SteeringController
    {
        private const float WheelBaseInPixels = 156;
        private const double SteeringWheelConversionConstant = 0.6; // 100 es -100 kozotti kormanyallas ertekeket feltetelezve
        private bool isInReverseGear;
        private float deltaTime = 1 / Game.TicksPerSecond;
        private Vector2 carCenterPoint;
        private Vector2 distanceFromCarCornerToCenter;

        public Vector2 NewCarPosition
        {
            get => ((this.FrontWheel + this.BackWheel) / 2) + (-1 * this.distanceFromCarCornerToCenter);
        }

        public double NewCarAngle
        {
            get => Math.Atan2(this.FrontWheel.Y - this.BackWheel.Y, this.FrontWheel.X - this.BackWheel.X);
        }

        private Vector2 CarCenterPoint
        {
            get => this.carCenterPoint;
            set
            {
                this.carCenterPoint = Vector2.Add(new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y), this.distanceFromCarCornerToCenter);
            }
        }

        private double CarCurrentAngle
        {
            get => World.Instance.ControlledCar.Angle;
        }

        private double SteeringAngle
        {
            get => this.SteeringAngle;
            set => this.SteeringAngle = value * SteeringWheelConversionConstant;
        }

        private float VelocityPixelPerTick
        {
            get
            {
                if (this.isInReverseGear)
                {
                    return (World.Instance.ControlledCar.Speed / this.deltaTime) * -1;
                }
                else
                {
                    return World.Instance.ControlledCar.Speed / this.deltaTime;
                }
            }
        }

        private Vector2 FrontWheel { get; set; }

        private Vector2 BackWheel { get; set; }

        private Vector2 CarDirectionUnitVector
        {
            get => new Vector2((float)Math.Cos(this.CarCurrentAngle), (float)Math.Sin(this.CarCurrentAngle));
        }

        private Vector2 FrontWheelDirectionUnitVector
        {
            get => new Vector2((float)Math.Cos(this.CarCurrentAngle+this.SteeringAngle), (float)Math.Sin(this.CarCurrentAngle + this.SteeringAngle));
        }

        public void UpdateSteeringProperties(IPowerTrainPacket packet)
        {
            this.SteeringAngle = packet.SteeringWheel;
            this.isInReverseGear = packet.GearShifterPosition == GearShifterPosition.R;
            this.SetDistanceFromCarCornerToCenter();
            this.SetWheelPositions();
            this.MoveWheelPositions();
        }

        private void SetWheelPositions()
        {
            this.FrontWheel = Vector2.Add(this.CarCenterPoint, Vector2.Multiply(WheelBaseInPixels / 2, this.CarDirectionUnitVector));
            this.BackWheel = Vector2.Subtract(this.CarCenterPoint, Vector2.Multiply(WheelBaseInPixels / 2, this.CarDirectionUnitVector));
        }

        private void MoveWheelPositions()
        {
            this.FrontWheel = Vector2.Add(this.FrontWheel, Vector2.Multiply(this.VelocityPixelPerTick, this.FrontWheelDirectionUnitVector));
            this.BackWheel = Vector2.Add(this.BackWheel, Vector2.Multiply(this.VelocityPixelPerTick, this.CarDirectionUnitVector));
        }

        private void SetDistanceFromCarCornerToCenter()
        {
            float distanceToCarCenterFromCorner = (float)Math.Sqrt(Math.Pow(World.Instance.ControlledCar.Width / 2, 2) + Math.Pow(World.Instance.ControlledCar.Height / 2, 2));
            double angleDifferentialToCarAngle = 180 - Math.Asin((World.Instance.ControlledCar.Width / 2) / distanceToCarCenterFromCorner);
            this.distanceFromCarCornerToCenter = Vector2.Multiply(distanceToCarCenterFromCorner, new Vector2((float)Math.Cos(this.CarCurrentAngle + angleDifferentialToCarAngle), (float)Math.Sin(this.CarCurrentAngle + angleDifferentialToCarAngle)));
        }
    }
}
