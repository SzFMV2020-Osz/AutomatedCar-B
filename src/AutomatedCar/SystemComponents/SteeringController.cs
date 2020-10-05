namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Numerics;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class SteeringController
    {
        private const int WheelBaseInPixels = 156;
        private const double SteeringWheelConversionConstant = 0.6; // 100 es -100 kozotti kormanyallas ertekeket feltetelezve
        private bool isInReverseGear;
        private float deltaTime = 1 / Game.TicksPerSecond;

        public Vector2 NewCarPosition
        {
            get => Vector2.Divide(Vector2.Add(this.FrontWheel, this.BackWheel), 2);
        }

        public double NewCarAngle
        {
            get => Math.Atan2(this.FrontWheel.Y - this.BackWheel.Y, this.FrontWheel.X - this.BackWheel.X);
        }

        private Vector2 CarLocation
        {
            get => new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
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

        private float FrontWheelVectorLength
        {
            get => this.CarLocation.Length() + (WheelBaseInPixels / 2);
        }

        private float BackWheelVectorLength
        {
            get => this.CarLocation.Length() - (WheelBaseInPixels / 2);
        }

        public void UpdateSteeringProperties(IPowerTrainPacket packet)
        {
            this.SteeringAngle = packet.SteeringWheel;
            this.isInReverseGear = packet.GearShifterPosition == GearShifterPosition.R;
            this.SetWheelPositions();
            this.MoveWheelPositions();
        }

        private void SetWheelPositions()
        {
            this.FrontWheel = Vector2.Multiply(this.FrontWheelVectorLength, this.CarDirectionUnitVector);
            this.BackWheel = Vector2.Multiply(this.BackWheelVectorLength, this.CarDirectionUnitVector);
        }

        private void MoveWheelPositions()
        {
            this.FrontWheel = Vector2.Add(this.FrontWheel, Vector2.Multiply(this.VelocityPixelPerTick, this.FrontWheelDirectionUnitVector));
            this.BackWheel = Vector2.Add(this.BackWheel, Vector2.Multiply(this.VelocityPixelPerTick, this.CarDirectionUnitVector));
        }
    }
}
