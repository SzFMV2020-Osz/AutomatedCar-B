namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media;
    using System.IO;
    using System.Linq;
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media;
    using NetTopologySuite.Geometries;
    using Newtonsoft.Json.Serialization;
    using ReactiveUI;
    using Geometry = Avalonia.Media.Geometry;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public class World : ReactiveObject
    {
        /* private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());
         public static World Instance { get { return lazySingleton.Value; } }*/

        private World()
        {
        }

        private AutomatedCar _controlledCar;

        private bool debugOn = false;

        public bool DebugOn { get => this.debugOn; }

        public static World Instance { get; } = new World();

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();
        public ObservableCollection<WorldObject> WorldObjectsForTesting { get; } = new ObservableCollection<WorldObject>();
        public AutomatedCar ControlledCar
        {
            get => this._controlledCar;
            set => this.RaiseAndSetIfChanged(ref this._controlledCar, value);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public List<WorldObject> NPCs { get; set; }

        public void PopulateInstance(string configFilename)
        {
            //JSONToWorldObject.LoadAllObjectsFromJSON(configFilename).ForEach(o => AddObject(o));
            JSONToWorldObject.LoadAllObjectsFromJSON(configFilename).ForEach(i => WorldObjectsForTesting.Add(i));
        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        /// <summary>
        /// Getting WorldObjects in given rectangle area, mostly for visualization.
        /// </summary>
        /// <param name="a">Point A of defined area.</param>
        /// <param name="b">Point B of defined area.</param>
        /// <param name="c">Point C of defined area.</param>
        /// <param name="d">Point D of defined area.</param>
        /// <returns>List of world objects containing all WorldObjects in given area.</returns>
        public List<WorldObject> getWorldObjectsInRectangle(Point a, Point b, Point c, Point d)
        {
            // TODO: quick ugly solution refactor needed
            var lr1 = new LinearRing(new[]
            {
                new Coordinate(a.X, a.Y), new Coordinate(b.X, b.Y),
                new Coordinate(c.X, c.Y), new Coordinate(d.X, d.Y),
                new Coordinate(a.X, a.Y),
            });

            NetTopologySuite.Geometries.Geometry rec = new NetTopologySuite.Geometries.Polygon(lr1);
            var ret = new List<WorldObject>();
            foreach (var wo in WorldObjectsForTesting)
            {
                if (wo.NetPolygon != null)
                {
                    foreach (var np in wo.NetPolygon)
                    {
                        if (rec.Intersects(np))
                        {
                            ret.Add(wo);
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        public static World GetInstance()
        {
            return Instance;
        }
    }
}