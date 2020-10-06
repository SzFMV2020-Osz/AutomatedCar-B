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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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

        public List<WorldObject> NPCs { get; set; }

        private World()
        {

        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        public static World GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Getting NPCs in given area.
        /// </summary>
        /// <param name="a">Point A of defined area</param>
        /// <param name="b">Point B of defined area</param>
        /// <param name="c">Point C of defined area</param>
        /// <returns>List of world objects containing all NPCs in given area.</returns>
        public List<WorldObject> getNPCsInTriangle(int a, int b, int c)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting Roads in given area.
        /// </summary>
        /// <param name="a">Point A of defined area</param>
        /// <param name="b">Point B of defined area</param>
        /// <param name="c">Point C of defined area</param>
        /// <returns>List of world objects containing all Roads in given area.</returns>
        public List<WorldObject> getRoadsInTriangle(int a, int b, int c)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting Trees in given area.
        /// </summary>
        /// <param name="a">Point A of defined area</param>
        /// <param name="b">Point B of defined area</param>
        /// <param name="c">Point C of defined area</param>
        /// <returns>List of world objects containing all Trees in given area.</returns>
        public List<WorldObject> getTreesInTriangle(int a, int b, int c)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting Signs in given area.
        /// </summary>
        /// <param name="a">Point A of defined area</param>
        /// <param name="b">Point B of defined area</param>
        /// <param name="c">Point C of defined area</param>
        /// <returns>List of world objects containing all Signs in given area.</returns>
        public List<WorldObject> getSignsInTriangle(int a, int b, int c)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting WorldObjects in given rectangle area.
        /// </summary>
        /// <param name="a">Point A of defined area</param>
        /// <param name="b">Point B of defined area</param>
        /// <param name="c">Point C of defined area</param>
        /// <param name="d">Point D of defined area</param>
        /// <returns>List of world objects containing all WorldObjects in given area.</returns>
        public List<WorldObject> getWorldObjectsInRectangle(int a, int b, int c, int d)
        {
            throw new NotImplementedException();
        }
    }
}