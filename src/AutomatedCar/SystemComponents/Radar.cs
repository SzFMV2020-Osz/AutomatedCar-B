namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;

    public class Radar : Sensor
    {
        
        private List<Type> types = new List<Type>()
        {
            typeof(Tree),
            typeof(Sign),
            typeof(NpcPedestrian),
            typeof(NpcCar),
            typeof(Garage),
        };

        private double dif;
        private double maxDetect;

        public double Distance { get; set; }

        public WorldObject LastSeenObject { get; set; }

        public SolidColorBrush LastSeenObjectBrush { get; set; }

        public Radar(VirtualFunctionBus virtualFunction, int offsetX, int offsetY, int rotate)
            : base(virtualFunction)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.rotate = rotate - 90;
            this.range = 150;
            this.angleOfView = 100;
            this.Brush = SolidColorBrush.Parse("red");
            this.dif = this.range * Math.Tan((double)this.angleOfView / 2 * (Math.PI / 180));
            this.maxDetect = Math.Sqrt(Math.Pow(this.range, 2) + Math.Pow(this.range, 2));
        }

        public override void Process()
        {
            this.Points = this.CalculatePoints();
            this.WorldObjects = this.GetWorldObjectsInRange();
            if (this.WorldObjects.Count > 0)
            {
                this.Distance = this.CalculateDistance();
            }
            else if (this.LastSeenObject != null)
            {
                this.ResetLastSeenObjectBrush();
            }
        }

        public double CalculateDistance()
        {
            double distance = this.maxDetect;
            double actual;
            WorldObject obj = null;
            foreach (WorldObject item in this.WorldObjects)
            {
                // A későbbiekben hozzáadott npc auto és gyalogos majd bekerül még, eddig ezt a 2-t találtam.
                if (this.types.Contains(item.GetType()))
                {
                    actual = Math.Sqrt(Math.Pow(this.Points[0].X - item.X, 2) + Math.Pow(this.Points[0].Y - item.Y, 2));
                    if (actual < distance)
                    {
                        distance = actual;
                        obj = item;
                    }
                }
            }

            if (obj != null)
            {
                this.SetClosestWorldObjectBrustToHighlighted(obj);
                return distance;
            }

            return 99999;
        }

        public void SetClosestWorldObjectBrustToHighlighted(WorldObject obj)
        {
            if (this.LastSeenObject != obj && this.LastSeenObject != null)
            {
                this.ResetLastSeenObjectBrush();
            }

            this.LastSeenObject = obj;
            this.LastSeenObjectBrush = obj.Brush;
            obj.Brush = new SolidColorBrush(Color.Parse("red"));
        }

        public void ResetLastSeenObjectBrush()
        {
            this.LastSeenObject.Brush = this.LastSeenObjectBrush;
        }
    }
}