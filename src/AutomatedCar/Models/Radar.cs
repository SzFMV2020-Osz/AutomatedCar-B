using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;
using AutomatedCar.Models.RadarUtil;
using System.Linq;
using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Media;

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

        public List<WorldObject> filterCollidables(List<WorldObject> paramWorldObjects)
        {
            List<WorldObject> nwos = new List<WorldObject>();
            foreach (var item in paramWorldObjects)
            {
               if(item.IsColliding) {
                nwos.Add(item);
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
            double x = (int)(paramNoticedObject.worldObject.X - paramNoticedObject.PrevX);
            double y = (int)(paramNoticedObject.worldObject.Y - paramNoticedObject.PrevY);
            paramNoticedObject.Vector = new Vector(x, y);
        }

        public void setAllSeen()
        {
            foreach (NoticedObject noticeObj in noticedObjects)
            {
                noticeObj.Seen = false;
            }
        }

        public bool isInNoticedObjects(WorldObject paramWorldObject)
        {
            foreach (NoticedObject noticedObject in this.noticedObjects)
            {
                if (ReferenceEquals(noticedObject.worldObject, paramWorldObject))
                {
                    return true;
                }
            }
            return false;
        }

        public void setHighlighted(ref WorldObject paramWorldObject)
        {
            paramWorldObject.Brush = (SolidColorBrush)Brushes.Red;
        }

        public void updatePreviewXY(NoticedObject n)
        {
            n.PrevX = n.worldObject.X;
            n.PrevY = n.worldObject.Y;
        }

        public void deleteLeftObjects()
        {
            this.noticedObjects.RemoveAll(noticedObj => noticedObj.Seen == false);
        }

        public void newObjectIsDetected()
        {
            bool itemInNoticedObj;
            List<WorldObject> worldObjectsInsideTriangle = new List<WorldObject>();
            //A worldObjectsInsideTriangle-re majd a "World.GetWorldObjectsInsideTriangle(computeTriangleInWorld().ToList())"-t meg kell hívni

            foreach (WorldObject worldObj in worldObjectsInsideTriangle)
            {
                itemInNoticedObj = false;
                if (worldObj.IsColliding)
                {
                    foreach (NoticedObject noticedObj in noticedObjects)
                    {
                        if (Object.ReferenceEquals(noticedObj,worldObj))
                        {
                            itemInNoticedObj = true;
                            break;
                        }
                    }

                    if(!itemInNoticedObj)
                    {
                        NoticedObject newNoticedObj = new NoticedObject();
                        newNoticedObj.Seen = true;
                        newNoticedObj.PrevX = null;
                        newNoticedObj.PrevY = null;
                        this.noticedObjects.Add(newNoticedObj);
                    }
                }
            }
        }

        public Point[] computeTriangleInWorld(RadarTriangleComputer RTC = null)
        {
            if(RTC == null) {
                RTC = new RadarTriangleComputer();
                RTC.offset = 120;
                RTC.distance = 200;
                RTC.angle = 60 / 2;
                RTC.rotate = (int)World.Instance.ControlledCar.Angle-90;
                RTC.carX = (int)World.Instance.ControlledCar.X;
                RTC.carY = (int)World.Instance.ControlledCar.Y;
            }

            return RTC.computeTriangleInWorld();
        }

        public void updateBus()
        {
            this.RadarSensorPacket.Update(this.getDangerousWorldObjects());
        }

        public List<NoticedObject> getDangerousWorldObjects()
        {
            List<NoticedObject> dangerousList = new List<NoticedObject>();
            Vector carVector = new Vector(
                World.Instance.ControlledCar.X - CarPreviousPosition.X,
                World.Instance.ControlledCar.Y - CarPreviousPosition.Y
            );

            double angle = World.Instance.ControlledCar.Angle;
            foreach (var item in noticedObjects)
            {
                if(objectIsSlover(carVector, item) || approaching(angle, item)){
                    item.DistanceFromCar_inMeter = computeObjectDistanceFromCar(item);
                    dangerousList.Add(item);
                }
            }

            return dangerousList;
        }

        private double computeObjectDistanceFromCar(NoticedObject item){
            double x = item.worldObject.X-World.Instance.ControlledCar.X;
            double y = item.worldObject.Y-World.Instance.ControlledCar.Y;
            Vector V = new Vector(x, y);
            return (V.Length-(World.Instance.ControlledCar.Height/2))/50;
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
