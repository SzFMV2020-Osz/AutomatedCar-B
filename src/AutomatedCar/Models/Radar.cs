using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;
using AutomatedCar.Models.RadarUtil;
using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;

namespace AutomatedCar.Models
{
    public class Radar : SystemComponent
    {        
        List<NoticedObject> noticedObjects;
        Point carPreviousPosition;
        Point[] points;
        public int offset = 0;

        public Radar(VirtualFunctionBus virtualFunctionBus = null)
            : base(virtualFunctionBus)
        {
            if (virtualFunctionBus != null)
            {
                virtualFunctionBus.RadarSensorPacket = this.RadarSensorPacket;
                virtualFunctionBus.RegisterComponent(this);
            }
        }

        public List<NoticedObject> NoticedObjects { get => noticedObjects; set => noticedObjects = value; }
        public Point CarPreviousPosition { get => carPreviousPosition; set => carPreviousPosition = value; }
        public Point[] Points { get => points; set => points = value; }

        public RadarSensorPacket RadarSensorPacket { get; set; } = new RadarSensorPacket();

        public List<NoticedObject> filterCollidables(List<WorldObject> paramWorldObjects)
        {
            List<NoticedObject> nwos = new List<NoticedObject>();
            foreach (var item in paramWorldObjects)
            {
               if(item.IsColliding) {
                nwos.Add(createNoticedObjectfromWorldObject(item));
               } 
            }
            return nwos;
        }

        private NoticedObject createNoticedObjectfromWorldObject(WorldObject wo){
            NoticedObject nwo = new NoticedObject();
            nwo.worldObject = wo;

            return nwo;
        }

        public void computeVector(NoticedObject paramNoticedObject)
        {
        }

        public void setAllSeen()
        {
        }

        public bool isInNoticedObjects(WorldObject paramWorldObject)
        {
            return true;
        }
        public void setHighlighted(WorldObject paramWorldObject)
        {    
        }

        public void updatePreviewXY()
        {
        }
        public void deleteLeftObjects()
        {
        }

        public NoticedObject newObjectIsDetected()
        {
            return null;
        }

        public Point[] computeTriangleInWorld()
        {
            RadarTriangleComputer RTC = new RadarTriangleComputer();

            RTC.offset = 120;
            RTC.distance = 200;
            RTC.angle = 60 / 2;
            RTC.rotate = (int)World.Instance.ControlledCar.Angle;
            RTC.carX = (int)World.Instance.ControlledCar.X;
            RTC.carY = (int)World.Instance.ControlledCar.Y;

            return RTC.computeTriangleInWorld();
        }

        public void updateBus()
        {
            this.RadarSensorPacket.Update(this.getDangerousWorldObjects());
        }

        public List<WorldObject> getDangerousWorldObjects()
        {
            List<WorldObject> dangerousList = new List<WorldObject>();
            Vector carVector = new Vector(
                World.Instance.ControlledCar.X - CarPreviousPosition.X,
                World.Instance.ControlledCar.Y - CarPreviousPosition.Y
            );

            double angle = World.Instance.ControlledCar.Angle;
            foreach (var item in noticedObjects)
            {
                if(
                    objectIsSlover(carVector, item) || approaching(angle, item)){
                    dangerousList.Add(item.worldObject);
                }
            }

            return dangerousList;
        }

        private Boolean objectIsSlover(Vector carVector, NoticedObject item) {
            return carVector.Length > ((Vector)item.Vector).Length;
        }

        private Boolean approaching(double angle, NoticedObject item) {
            return approaching0_90(angle, item) || approaching90_180(angle, item) || approaching180_270(angle, item) || approaching270_360(angle, item);
        }

        private Boolean approaching0_90(double angle, NoticedObject item){
            return (between(angle, 0, 90) && ((Vector)item.Vector).Y < 0 || ((Vector)item.Vector).X < 0);
        }

        private Boolean approaching90_180(double angle, NoticedObject item){
            return (between(angle, 90, 180) && ((Vector)item.Vector).Y > 0 || ((Vector)item.Vector).X < 0);
        }

        private Boolean approaching180_270(double angle, NoticedObject item){
            return (between(angle, 180, 270) && ((Vector)item.Vector).Y > 0 || ((Vector)item.Vector).X > 0);
        }

        private Boolean approaching270_360(double angle, NoticedObject item){
            return (between(angle, 270, 360) && ((Vector)item.Vector).Y < 0 || ((Vector)item.Vector).X > 0);
        }

        private Boolean between(double angle, int min, int max){
            return angle < max && angle >= min;
        }

        public override void Process()
        {
            this.updateBus();
        }
    }
}
