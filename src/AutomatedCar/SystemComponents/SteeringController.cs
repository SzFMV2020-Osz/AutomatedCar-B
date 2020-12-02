namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Numerics;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class SteeringController : ISteeringController
    {
        private const float WheelBaseInPixels = 156;
        private const double SteeringWheelConversionConstant = 0.1; // 100 es -100 kozotti kormanyallas ertekeket feltetelezve
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

        public void UpdateSteeringProperties(IReadOnlyHMIPacket HMIpacket, IReadOnlyLaneKeepingPacket laneKeepingPacket)
        {
            this.SetVelocityPixelPerTick();
            if (this.velocityPixelPerTick != 0)
            {
                this.carPoint = new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
                this.carCurrentAngle = World.Instance.ControlledCar.Angle;
                this.steeringAngle = laneKeepingPacket.Active ? laneKeepingPacket.Steering * SteeringWheelConversionConstant : HMIpacket.Steering * SteeringWheelConversionConstant;
                this.isInReverseGear = HMIpacket.Gear == Gears.R;
                this.SetCarDirectionUnitVector();
                this.SetNewDirectionUnitVector();
                this.SetNewCarPosition();
                this.SetNewCarAngle();
            }
        }

        private void SetVelocityPixelPerTick()
        {
            this.velocityPixelPerTick = World.Instance.ControlledCar.Speed * this.deltaTime;
        }

        private void SetCarDirectionUnitVector()
        {
            this.carDirectionUnitVector = new Vector2((float)Math.Cos(this.carCurrentAngle * (Math.PI / 180)), (float)Math.Sin(this.carCurrentAngle * (Math.PI / 180)));
        }

        private void SetNewCarPosition()
        {
            if (this.isInReverseGear)
            {
                this.NewCarPosition = this.carPoint + (-1 * ((float)this.velocityPixelPerTick * this.ConvertToVisualizationCoordinates(this.newDirectionUnitVector)));
            }
            else
            {
                this.NewCarPosition = this.carPoint + ((float)this.velocityPixelPerTick * this.ConvertToVisualizationCoordinates(this.newDirectionUnitVector));
            }
        }

        private void SetNewDirectionUnitVector()
        {
            this.newDirectionUnitVector = Vector2.Transform(this.carDirectionUnitVector, Matrix3x2.CreateRotation((float)(this.steeringAngle * (Math.PI / 180))));
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
    }
}
