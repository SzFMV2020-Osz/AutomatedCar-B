using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;
using AutomatedCar.Models.RadarUtil;
using System.Linq;

namespace AutomatedCar.Models
{
    public class Radar
    {        
        List<NoticedObject> noticedObjects;
        Position carPreviousPosition;
        Point[] points;
        public int offset = 0;
        public List<NoticedObject> NoticedObjects { get => noticedObjects; set => noticedObjects = value; }
        public Position CarPreviousPosition { get => carPreviousPosition; set => carPreviousPosition = value; }
        public Point[] Points { get => points; set => points = value; }

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
        }

        public List<WorldObject> getDangeriousWorldObjects()
        {
            return null;
        }
    }
}
