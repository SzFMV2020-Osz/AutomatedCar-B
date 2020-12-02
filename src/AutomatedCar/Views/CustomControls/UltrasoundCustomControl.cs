namespace AutomatedCar.Views.CustomControls
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;

    class UltrasoundCustomControl : Control
    {
        private byte indexer;
        private SolidColorBrush brush;
        private byte opacity;

        public byte Indexer { get { return this.indexer; } set { this.indexer = value; } }

        public SolidColorBrush Brush { get => this.brush; set { this.brush = value; } }

        public byte Opacity { get => this.opacity; set { this.opacity = value; } }

        public UltrasoundCustomControl()
        {
        }

        public override void Render(DrawingContext context)
        {
            if (!World.Instance.ControlledCar.UltraSoundVisible)
            {
                return;
            }

            base.Render(context);
            Ultrasound sensor = World.Instance.ControlledCar.Ultrasounds[this.indexer];
            PolylineGeometry geometry;
            if (sensor.Points == null)
            {
                geometry = new PolylineGeometry(sensor.CalculatePoints(), true);
            }
            else
            {
                geometry = new PolylineGeometry(sensor.Points, true);
            }

            this.Brush = sensor.Brush;

            this.Opacity = 60;

            context.DrawGeometry(new SolidColorBrush(new Color(this.Opacity, this.Brush.Color.R, this.Brush.Color.G, this.Brush.Color.B)), new Pen(), geometry);
        }
    }
}
