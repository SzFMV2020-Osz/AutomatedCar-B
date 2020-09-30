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

        /// <summary>
        /// Returns an instance of a World object.
        /// </summary>
        /// <returns>Returns an instance of the World.</returns>
        public static World GetInstance()
        {
            return new World();
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
    }
}