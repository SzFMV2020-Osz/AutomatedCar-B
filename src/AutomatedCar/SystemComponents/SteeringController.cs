namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Numerics;
    using AutomatedCar.Models;

    public class SteeringController : ISteeringController
    {
        private const int WheelBaseInPixels = 156;
        private const int CarWidth = 108;
        private const int MaximumSteeringAngle = 60;
        private const double SteeringWheelConversionConstant = 0.6; // 100 es -100 kozotti kormanyallas ertekeket feltetelezve
        private double turningCircleRadius = (WheelBaseInPixels / Math.Tan(MaximumSteeringAngle * Math.PI / 180)) + CarWidth;
        private bool isInReverseGear;
        private double deltaTime = 0.016;
        private Vector2 carPoint;
        private double steeringAngle;
        private double velocityPixelPerTick;
        private double carCurrentAngle;
        private Vector2 carDirectionUnitVector;
        private Vector2 newDirectionUnitVector;

        public Vector2 NewCarPosition { get; set; }

        public double NewCarAngle { get; set; }

        public void UpdateSteeringProperties(IReadOnlyHMIPacket packet)
        {
            this.SetVelocityPixelPerTick();
            if (this.velocityPixelPerTick != 0.0)
            {
                this.carPoint = new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
                this.carCurrentAngle = World.Instance.ControlledCar.Angle;
                this.steeringAngle = packet.Steering * SteeringWheelConversionConstant;
                this.isInReverseGear = packet.Gear == Gears.R;
                this.SetCarDirectionUnitVector();
                this.SetNewDirectionUnitVector();
                this.SetNewCarPosition();
                this.SetNewCarAngle();
            }
        }

        private void SetVelocityPixelPerTick()
        {
            this.velocityPixelPerTick = World.Instance.ControlledCar.speed * this.deltaTime;
        }

        private void SetCarDirectionUnitVector()
        {
            this.carDirectionUnitVector = new Vector2((float)Math.Cos(this.carCurrentAngle * (Math.PI / 180)), (float)Math.Sin(this.carCurrentAngle * (Math.PI / 180)));
        }

        private void SetNewCarPosition()
        {
            if (this.isInReverseGear)
            {
                this.NewCarPosition = this.carPoint + (-1 * ((float)this.LinearDisplacement() * this.ConvertToVisualizationCoordinates(this.newDirectionUnitVector)));
            }
            else
            {
                double temp = this.LinearDisplacement();
                this.NewCarPosition = this.carPoint + ((float)this.LinearDisplacement() * this.ConvertToVisualizationCoordinates(this.newDirectionUnitVector));
            }
        }

        private void SetNewDirectionUnitVector()
        {
            this.newDirectionUnitVector = Vector2.Transform(this.carDirectionUnitVector, Matrix3x2.CreateRotation((float)this.AnglularDisplacementInRadians()));
        }

        private void SetNewCarAngle()
        {
            if (this.newDirectionUnitVector != this.carDirectionUnitVector)
            {
                this.NewCarAngle = Math.Atan2(this.newDirectionUnitVector.Y, this.newDirectionUnitVector.X) * (180 / Math.PI);
            }
        }

        private Vector2 ConvertToVisualizationCoordinates(Vector2 vector)
        {
            return Vector2.Transform(this.newDirectionUnitVector, Matrix3x2.CreateRotation((float)(-90 * (Math.PI / 180))));
        }

        private double AnglularDisplacementInRadians() =>
            (this.velocityPixelPerTick / this.turningCircleRadius) * (this.steeringAngle / MaximumSteeringAngle);

        private double LinearDisplacement()
        {
            if (this.steeringAngle != 0)
            {
                return this.velocityPixelPerTick * (2 * Math.Sin(this.AnglularDisplacementInRadians() / 2)) / this.AnglularDisplacementInRadians();
            }
            else
            {
                return this.velocityPixelPerTick;
            }
        }
    }
}
