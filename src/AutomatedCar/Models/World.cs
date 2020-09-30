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

        public List<WorldObject> SearchInRange(List<Point> points)
        {
            throw new NotImplementedException();
        }

        public AutomatedCar GetControlledCar()
        {
            return Instance.ControlledCar;
        }

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