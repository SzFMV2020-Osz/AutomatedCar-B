namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    enum SensorOrientation
    {
        //front
        FrontLeft,
        FrontRight,
        //rightSide
        RightSideFront,
        RightSideRear,
        //rear
        RearRight,
        RearLeft,
        //leftSide
        LeftSideRear,
        LeftSideFront
    }

    public class ParkingPilot : SystemComponent
    {

        private IReadOnlyHMIPacket HMIPacket { get; set; }

        private IReadOnlyPowerTrainPacket PowerTrainPacket { get; set; }

        private IReadOnlyRadarSensorPacket RadarSensorPacket { get; set; }

        private IReadOnlyDebugPacket DebugPacket { get; set; }

        private Ultrasound[] Sensors;

        private SensorOrientation[] sensorOrientationTags = 
        {
            SensorOrientation.FrontRight,
            SensorOrientation.RightSideFront,
            SensorOrientation.FrontLeft,
            SensorOrientation.LeftSideFront,
            SensorOrientation.RearRight,
            SensorOrientation.RightSideRear,
            SensorOrientation.RearLeft,
            SensorOrientation.LeftSideRear
        };

        private double sensorMaxRange;

        private WorldObject lastSeen;

        private double parkingStartAngle;

        private bool ableToPark;
    
        private const double requiredParkingSpace = 300;
        public ParkingPilot(VirtualFunctionBus functionBus, Ultrasound[] sensors)
          : base(functionBus)
        {
            this.PowerTrainPacket = this.virtualFunctionBus.PowerTrainPacket;
            this.HMIPacket = this.virtualFunctionBus.HMIPacket;
            this.RadarSensorPacket = this.virtualFunctionBus.RadarSensorPacket;
            this.DebugPacket = this.virtualFunctionBus.DebugPacket;
            this.Sensors = sensors;

            this.sensorMaxRange = sensors[0].range + 50;

            this.ableToPark = false;
        }

        public ParkingPilot()
        {

        }

        public override void Process()
        {
            if (HMIPacket.ParkingPilot)
            {
                SearchParkingSpace();
            }
        }

        private void SearchParkingSpace()
        {
            CheckObjectBeside();

            if(!CheckAngleRange(parkingStartAngle,10,World.Instance.ControlledCar.Angle))
            {
                lastSeen = null;
                System.Console.WriteLine("abort");
            }

            if(requiredParkingSpace < this.CalculateSpace())
            {
                ableToPark = true;
                System.Console.WriteLine("CanPark");
            }

            System.Console.WriteLine("-----------------------");

        }

        private void CheckObjectBeside()
        {
            WorldObject frontWiewObject = Sensors[Array.IndexOf(sensorOrientationTags,SensorOrientation.RightSideFront)].LastSeenObject;
            double distFront  = Sensors[Array.IndexOf(sensorOrientationTags,SensorOrientation.RightSideFront)].Distance;

            WorldObject rearWiewObject  = Sensors[Array.IndexOf(sensorOrientationTags,SensorOrientation.RightSideRear)].LastSeenObject;
            double distRear  = Sensors[Array.IndexOf(sensorOrientationTags,SensorOrientation.RightSideRear)].Distance;

            if(distFront <= sensorMaxRange && distRear <= sensorMaxRange && frontWiewObject == rearWiewObject)
            {
                RegisterObject(frontWiewObject);
                System.Console.WriteLine("regobj");
            }
        }

        private void RegisterObject(WorldObject objectInFocus)
        {
            if(lastSeen != objectInFocus)
            {
                lastSeen = objectInFocus;
                parkingStartAngle = World.Instance.ControlledCar.Angle;
                ableToPark = false;
            }         
        }

        private bool CheckAngleRange(double compareTo, double range, double angle)
        {
            angle = angle >= 0 ? angle : 360 + angle;

            if(compareTo + range > 360)
            {
                return (angle > compareTo - range && (angle <= 360 || angle < compareTo + range - 360 ));
            }
            else if (compareTo - range < 0)
            {
                return (angle < compareTo + range && (angle >= 0 || angle > compareTo - range + 360 )); 
            }
            else
            {
                return (angle > compareTo - range && angle < compareTo + range); 
            }

        }

        private double CalculateSpace()
        {
            if(lastSeen != null)
            {
                Vector2 carCenter = new Vector2(World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
                Vector2 objectClosestCorner = new Vector2(this.lastSeen.X - (this.lastSeen.Width / 2), this.lastSeen.Y - (this.lastSeen.Height / 2));
                Vector2 straightLineFromObjectToCar = Vector2.Subtract(carCenter, objectClosestCorner);
                double angleOfCarDirectionAndStraightLine = ((World.Instance.ControlledCar.Angle-90) * (Math.PI / 180)) - Math.Atan2(straightLineFromObjectToCar.Y, straightLineFromObjectToCar.X);
                double parallelDistanceFromObjectToCar = Math.Cos(angleOfCarDirectionAndStraightLine) * straightLineFromObjectToCar.Length();

                return Math.Abs(parallelDistanceFromObjectToCar);
            }
            else
            {
                return 0;
            }
        }

        public double CalculateSpace(Vector2 carCenter, Vector2 lastSeenObjectClosestCorner, double carAngle)
        {
            Vector2 straightLineFromObjectToCar = Vector2.Subtract(carCenter, lastSeenObjectClosestCorner);
            double angleOfCarDirectionAndStraightLine = ((carAngle-90) * (Math.PI / 180)) - Math.Atan2(straightLineFromObjectToCar.Y, straightLineFromObjectToCar.X);
            double parallelDistanceFromObjectToCar = Math.Cos(angleOfCarDirectionAndStraightLine) * straightLineFromObjectToCar.Length();

            return Math.Abs(parallelDistanceFromObjectToCar);
        }

        private void StartParking()
        {

        }

        private void FinishParking()
        {
            
        }
    }
}
