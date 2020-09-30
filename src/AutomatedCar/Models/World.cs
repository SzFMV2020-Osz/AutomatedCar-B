namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media;
    using ReactiveUI;

    public class World : ReactiveObject
    {
        // private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());
        // public static World Instance { get { return lazySingleton.Value; } }

        private AutomatedCar _controlledCar;

        public static World Instance { get; } = new World();

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();

        public AutomatedCar ControlledCar
        {
            get => this._controlledCar;
            set => this.RaiseAndSetIfChanged(ref this._controlledCar, value);
        }

        private List<WorldObject> trees;
        private List<WorldObject> signs;
        private List<WorldObject> roads;
        private List<WorldObject> road;
        private List<WorldObject> npcs; // for 2nd sprint

        private struct SerializeObject
        {
            public int ZIndex;
            public int Width;
            public int Height;
            public string FileName;
            public Point PositionPoint;
            public Point RotationPoint;
            public Polygon Polygon;
            public bool IsCollidable;
            public double RotationAngle;
            public bool IsHighlighted;
            public int X;
            public int Y;
            public string Filename { get; set; }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        private World()
        {

        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        /// <summary>
        /// For sensors: Returns a list of worldobject that can be found in an area defined by the given list of points.
        /// </summary>
        /// <param name="searchArea">Area to be searched, defined by a list of points.</param>
        /// <returns>Returns a list of worldobjects in a given area.</returns>
        public List<WorldObject> SearchInRange(List<Point> searchArea)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// For getting the controlledCars position, geometry etc.
        /// </summary>
        /// <returns>Returns the instance of the controlled car as an AutomatedCar.</returns>
        public AutomatedCar GetControlledCar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// For getting the NPC-s position, geometry etc.
        /// </summary>
        /// <returns>Returns a list of WorldObjects containing all the NPCs</returns>
        public List<WorldObject> GetNPCs()
        {
            throw new NotImplementedException();
        }

        private List<WorldObject> JSONLoadWorldObjectsFromFile(string filename)
        {
            List<WorldObject> list = new List<WorldObject>();


            // newtonsoft json object
            // -> worldobject

            return list;
        }

        public static World GetInstance()
        {

            if (Instance != null)
            {
                return Instance;
            }
            else
            {
                // loadstuff
                // config elérés
                string[] Filenames = File.ReadAllLines("config.txt");
                // configból filenevek és deserialize
                // instance manipulation
                return Instance;
            }
        }
    }
}