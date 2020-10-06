namespace AutomatedCar.Models
{
    using System;
    using System.Numerics;
    using Avalonia.Media;
    using System.Collections.ObjectModel;
    using SystemComponents;
    using Avalonia;
    using Avalonia.Controls.Primitives;

    public class AutomatedCar : WorldObject, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;

        public ObservableCollection<DummySensor> Sensors { get; } = new ObservableCollection<DummySensor>();

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.IsCollidable = true;
            this.IsHighlighted = false;
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public Geometry Geometry { get; set; }

        public SolidColorBrush Brush { get; private set; }

        public int Speed { get; set; }

        /// <summary>
        /// Example usage add
        /// </summary>
        /// <param name="point"></param>
        public void SetNextPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Move(Vector2 with)
        {
            throw new NotImplementedException();
        }


        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public SolidColorBrush RadarBrush { get; set; }

        public Geometry RadarGeometry { get; set; }

        public bool RadarVisible { get; set; }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }

        public void Start()
        {
            this.virtualFunctionBus.Start();
        }
    }
}