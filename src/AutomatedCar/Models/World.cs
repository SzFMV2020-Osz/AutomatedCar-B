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
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media;
    using ReactiveUI;

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

        public AutomatedCar ControlledCar
        {
            get => this._controlledCar;
            set => this.RaiseAndSetIfChanged(ref this._controlledCar, value);
        }

        private List<WorldObject> trees;
        private List<WorldObject> signs;
        private List<WorldObject> roads;
        private List<WorldObject> npcs; // for 2nd sprint

        public int Width { get; set; }

        public int Height { get; set; }

        public List<WorldObject> NPCs { get; set; }

        private World()
        {

        }

        public void PopulateInstance(string configFilename)
        {
            JSONToWorldObject.LoadAllObjectsFromJSON(configFilename).ForEach(o => AddObject(o));
        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        public static World GetInstance()
        {
            return Instance;
        }
    }
}