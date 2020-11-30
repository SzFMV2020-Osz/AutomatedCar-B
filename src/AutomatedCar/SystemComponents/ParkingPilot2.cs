namespace AutomatedCar.SystemComponents
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class ParkingPilot2 : SystemComponent
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

        private WorldObject firstSeen;

        private WorldObject lastSeen;

        private double parkingStartAngle;

        private bool ableToPark;

        private const double requiredParkingSpace = 300;
        public ParkingPilot2(VirtualFunctionBus functionBus, Ultrasound[] sensors)
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

        public override void Process()
        {
            if (HMIPacket.ParkingPilot)
            {
                //SearchParkingSpace();
            }
        }

        private void SearchParkingSpace()
        {
            CheckObjectBeside();

            if (!CheckAngleRange(parkingStartAngle, 10, World.Instance.ControlledCar.Angle))
            {
                lastSeen = null;
                System.Console.WriteLine("abort");
            }

            if (requiredParkingSpace < CalculateSpace())
            {
                ableToPark = true;
                System.Console.WriteLine("CanPark");
            }

            System.Console.WriteLine("-----------------------");

        }

        private void CheckObjectBeside()
        {
            WorldObject frontWiewObject = Sensors[Array.IndexOf(sensorOrientationTags, SensorOrientation.RightSideFront)].LastSeenObject;
            double distFront = Sensors[Array.IndexOf(sensorOrientationTags, SensorOrientation.RightSideFront)].Distance;

            WorldObject rearWiewObject = Sensors[Array.IndexOf(sensorOrientationTags, SensorOrientation.RightSideRear)].LastSeenObject;
            double distRear = Sensors[Array.IndexOf(sensorOrientationTags, SensorOrientation.RightSideRear)].Distance;

            if (distFront <= sensorMaxRange && distRear <= sensorMaxRange && frontWiewObject == rearWiewObject)
            {
                RegisterObject(frontWiewObject);
                System.Console.WriteLine("regobj");
            }
        }

        private void RegisterObject(WorldObject objectInFocus)
        {
            if (firstSeen == null)
            {
                firstSeen = objectInFocus;
            }
            else if (firstSeen != objectInFocus && lastSeen == null)
            {
                lastSeen = objectInFocus;
                parkingStartAngle = World.Instance.ControlledCar.Angle;
            }
            else if (lastSeen != objectInFocus)
            {
                firstSeen = null;
                lastSeen = null;
            }
        }

        private bool CheckAngleRange(double compareTo, double range, double angle)
        {
            angle = angle >= 0 ? angle : 360 + angle;

            if (compareTo + range > 360)
            {
                return (angle > compareTo - range && (angle <= 360 || angle < compareTo + range - 360));
            }
            else if (compareTo - range < 0)
            {
                return (angle < compareTo + range && (angle >= 0 || angle > compareTo - range + 360));
            }
            else
            {
                return (angle > compareTo - range && angle < compareTo + range);
            }

        }

        private double CalculateSpace()
        {
            if (lastSeen != null && firstSeen != null)
            {
                int firstX = firstSeen.X;
                int firstY = firstSeen.Y;
                int lastX = lastSeen.X;
                int lastY = lastSeen.Y;

                return firstY - lastY - World.Instance.ControlledCar.Height;
            }
            else
            {
                return 0;
            }
        }

        private void StartParking()
        {

        }

        private void FinishParking()
        {

        }
    }
}

