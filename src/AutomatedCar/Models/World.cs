﻿namespace AutomatedCar.Models
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
        /* private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());
         public static World Instance { get { return lazySingleton.Value; } }*/

        private World()
        {
        }

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
        private List<WorldObject> npcs; // for 2nd sprint

        public int Width { get; set; }

        public int Height { get; set; }

        public List<WorldObject> NPCs { get; set; }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        /// <summary>
        /// Getting WorldObjects in given rectangle area, mostly for visualization.
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

        /// <summary>
        /// Getting WorldObject in given triangle area, mostly for sensors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<WorldObject> getWorldObjectsInTriangle(int a, int b, int c)
        {
            throw new NotImplementedException();
        }
    }
}