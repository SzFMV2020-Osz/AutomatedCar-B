namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Numerics;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Markup.Xaml.Templates;

    public class SteeringControllerB : ISteeringController
    {
        private const float WheelBaseInPixels = 156;
        private const double SteeringWheelConversionConstant = 0.6; // 100 es -100 kozotti kormanyallas ertekeket feltetelezve (kb 35°)
        private bool isInReverseGear;
        private double deltaTime = 0.016;
        private Vector2 carCenterPoint;
        private Vector2 displacementFromCarCornerToCenter;
        public Vector2 NewCarPosition { get; set; }
        private double steeringAngle;
        private double velocityPixelPerTick;
        private double carCurrentAngle;

        public void SetNewCarPosition()
        {
            this.NewCarPosition = ((this.FrontWheel + this.BackWheel) / 2); // + (-1 * this.displacementFromCarCornerToCenter);
        }

        public double NewCarAngle { get; set; }

        private double SteeringAngle
        {
            get => this.steeringAngle;
            set => this.steeringAngle = value * SteeringWheelConversionConstant;
        }

        private void SetVelocityPixelPerTick()
        {
            if (this.isInReverseGear)
            {
                this.velocityPixelPerTick = (World.Instance.ControlledCar.Speed * this.deltaTime) * -1;
            }
            else
            {
                this.velocityPixelPerTick = World.Instance.ControlledCar.Speed * this.deltaTime;
            }
        }

        private Vector2 FrontWheel { get; set; }

        private Vector2 BackWheel { get; set; }

        private Vector2 CarDirectionUnitVector
        {
            get => new Vector2((float)Math.Cos(this.carCurrentAngle), (float)Math.Sin(this.carCurrentAngle));
        }

        private Vector2 FrontWheelDirectionUnitVector
        {
            get => new Vector2((float)Math.Cos(this.carCurrentAngle + this.SteeringAngle), (float)Math.Sin(this.carCurrentAngle + this.SteeringAngle));
        }


        private float calculateTurningCircleRadius()
        {
            double turningCircleRadius = Math.Tan(Math.PI / 2 - SteeringAngle) * WheelBaseInPixels;
            return (float)turningCircleRadius;  
        }

        private Vector2 calculateTurningCircleUnitVector() //kocsi hatsotengelyebol a kanyarodás kozeppontjaba mutato egysegvektor
        {
            if(steeringAngle > 0)
            {
                return RotateVectorToRight(CarDirectionUnitVector);
            }
            else
            {
                return RotateVectorToLeft(CarDirectionUnitVector);
            }
        }

        private Vector2 RotateVectorToRight(Vector2 v)
        {
            return new Vector2(v.Y,-(v.X));
             //TODO: vektor elforgatás
        }

        private Vector2 RotateVectorToLeft(Vector2 v)
        {
            return new Vector2(-(v.Y),v.X);
        }

        private Vector2 calculateTurningCircleCenterPos()
        {
            Vector2 TurningCircleUnitVector = calculateTurningCircleUnitVector();
            float turningCircleRadius = calculateTurningCircleRadius();

            Vector2 CircleCenterPosition = new Vector2(carCenterPoint.X + TurningCircleUnitVector.X * turningCircleRadius,
                                                       carCenterPoint.X + TurningCircleUnitVector.Y * turningCircleRadius);
            return CircleCenterPosition;
        }

        private float calculateTurningAngle(float driveDistance, float turningRadius)
        {
             return driveDistance / (turningRadius);
        }

        private Vector2 calculateNewCarPos()
        {
            Vector2 turningCircleCenterPosition = calculateTurningCircleCenterPos();
            float turningRadius = calculateTurningCircleRadius();
            float turningAngle = calculateTurningAngle((float)velocityPixelPerTick,turningRadius);

            Vector2 newCarPosition = new Vector2((float)(turningCircleCenterPosition.X + Math.Cos(Math.PI - carCurrentAngle - turningAngle)),
                                                 (float)(turningCircleCenterPosition.Y + Math.Sin(Math.PI - carCurrentAngle - turningAngle)));
            return newCarPosition;                                                 
        }

        public void UpdateSteeringProperties(IPowerTrainPacket packet)
        {
            this.SetDistanceFromCarCornerToCenter();
            this.carCurrentAngle = World.Instance.ControlledCar.Angle;
            this.SteeringAngle = packet.SteeringWheel;
            this.isInReverseGear = packet.GearShifterPosition == GearShifterPosition.R;
            this.SetVelocityPixelPerTick();
            this.SetCarCenterPoint();
            this.SetWheelPositions();
            this.SetNewCarPosition();
            this.SetNewCarAngle();
        }

        private void SetWheelPositions()
        {
            this.FrontWheel = this.carCenterPoint + ((WheelBaseInPixels / 2) * this.CarDirectionUnitVector);
            this.BackWheel = this.carCenterPoint - ((WheelBaseInPixels / 2) * this.CarDirectionUnitVector);
            this.FrontWheel += (float)this.velocityPixelPerTick * this.FrontWheelDirectionUnitVector;
            this.BackWheel += (float)this.velocityPixelPerTick * this.CarDirectionUnitVector;
        }

        private void SetDistanceFromCarCornerToCenter()
        {
            float distanceToCarCenterFromCorner = (float)Math.Sqrt(Math.Pow(World.Instance.ControlledCar.Width / 2, 2) + Math.Pow(World.Instance.ControlledCar.Height / 2, 2));
            double angleDifferentialToCarAngle = 180 - Math.Asin((World.Instance.ControlledCar.Width / 2) / distanceToCarCenterFromCorner);
            this.displacementFromCarCornerToCenter = distanceToCarCenterFromCorner * new Vector2((float)Math.Cos(this.carCurrentAngle + angleDifferentialToCarAngle), (float)Math.Sin(this.carCurrentAngle + angleDifferentialToCarAngle));
        }

        private void SetNewCarAngle()
        {
            this.NewCarAngle = Math.Atan2(this.FrontWheel.Y - this.BackWheel.Y, this.FrontWheel.X - this.BackWheel.X) * (180 / Math.PI);
        }

        private void SetCarCenterPoint()
        {
            this.carCenterPoint = new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y) + this.displacementFromCarCornerToCenter;
        }
    }
}
