namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Avalonia;
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

        public int Width { get; set; }

        public int Height { get; set; }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        public List<World> SearchInRange(List<Point> points)
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
    }
}