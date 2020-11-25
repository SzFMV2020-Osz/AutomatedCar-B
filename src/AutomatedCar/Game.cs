namespace AutomatedCar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Input;

    public class Game : GameBase
    {
        private readonly World world;

        public Game(World world)
        {
            this.world = world;
        }

        private Random Random { get; } = new Random();

        protected override void Tick()
        {

            // Demo-hoz
            // this.world.ControlledCar.NetPolygons = this.world.ControlledCar.GenerateNetPolygons(this.world.ControlledCar.BasePoints);
            // Collision.Handler();

            Point[] points = World.Instance.ControlledCar.Radar.computeTriangleInWorld();
            List<Point> pointList = new List<Point>();
            pointList.Add(points[0]);
            pointList.Add(points[1]);
            pointList.Add(points[2]);

            List<WorldObject> wos = World.Instance.GetWorldObjectsInsideTriangle(pointList);
            List<WorldObject> wosc = World.Instance.ControlledCar.Radar.filterCollidables(wos);

            if(wosc.Count > 0) {
                var t = wos;
            }


            World.Instance.Tick();
        }
    }
}