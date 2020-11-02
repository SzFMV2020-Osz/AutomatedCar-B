using Avalonia;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace AutomatedCar.Models
{
    public class NpcCar : WorldObject, INPC
    {
        public NpcCar(string filename, int width, int height, List<List<Point>> polyPoints, string jsonRoute)
        : base(0, 0, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polyPoints)
        {
            this.JsonRoute = jsonRoute;
            this.ReadCarRoute();
        }

        private string JsonRoute;

        private List<NpcRoute> CarRoutes;

        public int Rotation { get; set; }

        public int Speed { get; set; }

        public void ReadCarRoute()
        {
            this.CarRoutes = LoadJson(this.JsonRoute);
        }

        public void SetStartPosition()
        {
            this.X = int.Parse(this.CarRoutes.First().x);
            this.Y = int.Parse(this.CarRoutes.First().y);

        }

        public void Move(Vector2 with)
        {
            this.X = (int)with.X;
            this.Y = (int)with.Y;
        }

        public void MoveX(int x)
        {
            this.X = int.Parse(this.CarRoutes[x].x);
        }

        public void MoveY(int y)
        {
            this.Y = int.Parse(this.CarRoutes[y].y);
        }

        public void SetNextPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        private int CheckSpeedLimit(int currentLimit)
        {
            List<Point> viewTrianglePoints = CalculateNpcViewTriangle();
            List<WorldObject> visibleObjects = World.Instance.GetWorldObjectsInsideTriangle(viewTrianglePoints);

            if(visibleObjects.Where(o => o is Sign).Where(o => o.Filename.Contains("roadsign_speed")) != null)
            {
                string speedLimitSignName = visibleObjects.Where(o => o.Filename.Contains("roadsign_speed")).First().Filename;
                int speedLimit = SpeedLimitFromSignName(speedLimitSignName, currentLimit);
            }
            return currentLimit;
        }

        private int SpeedLimitFromSignName(string type, int currentLimit)
        { 
            switch(type)
                {
                    case "roadsign_speed_40":
                        Console.WriteLine("Case 1");
                        return 40;
                    case "roadsign_speed_50":
                        Console.WriteLine("Case 2");
                        return 50;
                    case "roadsign_speed_60":
                        Console.WriteLine("Case 2");
                        return 60;
                    default:
                        Console.WriteLine("Default case");
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
            carNormalLeft = carNormalLeft/ carNormalLeft.Length();

            float triangleHeight = 200;
            float trinagleBaseHalf = 231 / 2;

            List<Point> trianglePoints = new List<Point>();
            Vector2 trianglePoint1 = carPosition + (Vector2.Multiply(carDirection,triangleHeight)) + (Vector2.Multiply(carNormalRight,trinagleBaseHalf));
            Vector2 trianglePoint2 = carPosition + (Vector2.Multiply(carDirection,triangleHeight)) + (Vector2.Multiply(carNormalLeft,trinagleBaseHalf));

            trianglePoints.Add(new Point(this.X,this.Y));
            trianglePoints.Add(new Point(trianglePoint1.X,trianglePoint1.Y));
            trianglePoints.Add(new Point(trianglePoint2.X,trianglePoint2.Y));

            return trianglePoints;
        }

        public static List<NpcRoute> LoadJson(string path)
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
