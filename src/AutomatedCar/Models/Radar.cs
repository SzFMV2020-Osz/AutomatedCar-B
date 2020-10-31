using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;
using AutomatedCar.Models.RadarUtil;

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
            return null;
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

            //RTC.offset = 0;

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
