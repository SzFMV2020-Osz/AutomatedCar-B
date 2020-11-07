namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using System.Text;
    using Avalonia;
    using Avalonia.Media;
    using Newtonsoft.Json;

    public class NpcCar : WorldObject, INPC
    {
        public NpcCar(string filename, int width, int height, List<List<Point>> polyPoints, string jsonRoute)
        : base(0, 0, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polyPoints)
        {
            this.LoadNpcRoute(jsonRoute);
            this.toReach = Route[1];
            speedLimit = 10;
            routeIndex = 1;
        }

        private int routeIndex;
        private List<NpcRoute> Route;
        private NpcRoute toReach;
        private bool isthreesixty = false;
        public int Mass { get; set; } = 5;
        private int speedLimit;

        public void LoadNpcRoute(string jsonRoute)
        {
            this.Route = ReadJson(jsonRoute);
        }

        public void SetStartPosition()
        {
            this.X = (this.Route.First().x);
            this.Y = (this.Route.First().y);
        }

        public void Move(object sender, EventArgs args)
        {
            speedLimit = CheckSpeedLimit(speedLimit);

            Vector2 movementDirection = new Vector2(toReach.x - X, toReach.y - Y);
            Vector2 moveWith = movementDirection / movementDirection.Length() * Convert.ToSingle(speedLimit);
            
            if(movementDirection.Length() == 0 || movementDirection.Length() < moveWith.Length())
            {
                if(toReach.tag == "finish")
                {
                    routeIndex = -1;
                }

                toReach = Route[++routeIndex];
                movementDirection = new Vector2(toReach.x - X, toReach.y - Y);
                moveWith = movementDirection / movementDirection.Length() * Convert.ToSingle(speedLimit);
            }

            this.CalculateNpcAngle(movementDirection);

            Move(moveWith);
        }

        private void CalculateNpcAngle(Vector2 direction)
        {
            double angle = Math.Atan2(direction.X, direction.Y * -1) * 180 / Math.PI;
            if (angle < 0)
            {
                angle += 360;
            }

            if (angle < 55)
            {
                isthreesixty = false;
            }

            if (angle > this.Angle && !isthreesixty)
            {
                this.Angle += 5;
            }

            if (this.Angle >= 360 && !isthreesixty)
            {

                isthreesixty = true;
                this.Angle = 0;
            }
        }

        public void Move(Vector2 with)
        {
            this.X = this.X + (int)with.X;
            this.Y = this.Y + (int)with.Y;
        }

        public void SetNextPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private int CheckSpeedLimit(int currentLimit)
        {
            List<Point> viewTrianglePoints = CalculateNpcViewTriangle();
            List<WorldObject> visibleObjects = World.Instance.GetWorldObjectsInsideTriangle(viewTrianglePoints);

            if(visibleObjects.Where(o => o is Sign).Where(o => o.Filename.Contains("roadsign_speed")).Count() != 0)
            {
                string speedLimitSignName = visibleObjects.Where(o => o.Filename.Contains("roadsign_speed")).First().Filename;
                return SpeedLimitFromSignName(speedLimitSignName, currentLimit);
            }

            return currentLimit;
        }

        private int SpeedLimitFromSignName(string type, int currentLimit)
        { 
            switch(type)
                {
                    case "roadsign_speed_40": 
                        return 10;
                    case "roadsign_speed_50":
                        return 15;
                    case "roadsign_speed_60":
                        return 20;
                    default:
                        return currentLimit;
                }
        }

        private List<Point> CalculateNpcViewTriangle()
        {
            Vector2 carPosition = new Vector2(this.X,this.Y);

            Vector2 carDirection = new Vector2((float)Math.Cos(this.Angle), (float)Math.Sin(this.Angle));
            Vector2 carNormalRight = new Vector2((float)Math.Cos(this.Angle - (Math.PI / 2)), (float)Math.Sin(this.Angle - (Math.PI / 2)));
            Vector2 carNormalLeft = new Vector2((float)Math.Cos(this.Angle + (Math.PI / 2)), (float)Math.Sin(this.Angle + (Math.PI / 2)));

            carDirection = carDirection / carDirection.Length();
            carNormalRight = carNormalRight / carNormalRight.Length();
            carNormalLeft = carNormalLeft / carNormalLeft.Length();

            float triangleHeight = 200;
            float trinagleBaseHalf = 231 / 2;

            List<Point> trianglePoints = new List<Point>();
            Vector2 trianglePoint1 = carPosition + (Vector2.Multiply(carDirection, triangleHeight)) + (Vector2.Multiply(carNormalRight, trinagleBaseHalf));
            Vector2 trianglePoint2 = carPosition + (Vector2.Multiply(carDirection, triangleHeight)) + (Vector2.Multiply(carNormalLeft, trinagleBaseHalf));

            trianglePoints.Add(new Point((double)this.X, (double)this.Y));
            trianglePoints.Add(new Point((double)trianglePoint2.X, (double)trianglePoint2.Y));
            trianglePoints.Add(new Point((double)trianglePoint1.X, (double)trianglePoint1.Y));

            return trianglePoints;
        }

        public static List<NpcRoute> ReadJson(string path)
        {
            using (StreamReader r = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(path)))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<NpcRoute>>(json);

                return items;
            }
        }
    }
}
