using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;

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
            Point[] points = new Point[3];

            points[0] = new Point(0,this.offset);
            points[1] = new Point(0,this.offset);
            points[2] = new Point(0,this.offset);

            return points;
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
