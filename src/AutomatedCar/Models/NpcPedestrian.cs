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
    using Newtonsoft.Json;

    public class NpcPedestrian : WorldObject, INPC
    {
        public NpcPedestrian(string filename, int width, int height, List<List<Point>> polyPoints, string jsonRoute)
             : base(0, 0, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polyPoints)
        {
            this.JsonRoute = jsonRoute;
            this.ReadPedRoute();
        }

        private string JsonRoute;

        private List<NpcRoute> PedRoutes;

        public int Rotation { get; set; }

        public int Speed { get; set; }

        public void ReadPedRoute()
        {
            this.PedRoutes = LoadJson(this.JsonRoute);
        }

        public void SetStartPosition()
        {

            this.X = int.Parse(this.PedRoutes.First().x);
            this.Y = int.Parse(this.PedRoutes.First().y);

        }

        public void ReadPedestrianRoute()
        {
            //this.PedestrianRoute.LoadJson(this.JsonRoute);
        }

        public void Move(Vector2 with)
        {
            this.X = (int)with.X;
            this.Y = (int)with.Y;
        }

        public void MoveX(int x)
        {
            throw new NotImplementedException();
        }

        public void MoveY(int y)
        {
            throw new NotImplementedException();
        }

        public void SetNextPosition(int x, int y)
        {
            throw new NotImplementedException();
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
