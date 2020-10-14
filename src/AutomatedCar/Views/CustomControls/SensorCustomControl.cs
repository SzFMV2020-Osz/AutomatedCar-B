using AutomatedCar.Views.CustomControls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Views.CustomControls
{
    public class SensorCustomControl : Control
    {
        private const double SCALER = 50;

        private double rotation = 0;
        private double distance = 50;
        private double fov = 30;

        public double Rotation { get { return this.rotation; } set { this.SetAndRaise(this.RotationProperty, ref this.rotation, value); } }

        public double Distance { get { return this.distance; } set { this.SetAndRaise(this.DistanceProperty, ref this.distance, value); } }

        public double FOV { get { return this.fov; } set { this.SetAndRaise(this.FOVProperty, ref this.fov, value); } }

        public SolidColorBrush Brush { get { return this.GetValue(this.BrushProperty); } set { this.SetValue(this.BrushProperty, value); } }

        public Vector Offset { get { return this.GetValue(this.OffsetProperty); } set { this.SetValue(this.OffsetProperty, value); } }

        public byte Opacity { get { return this.GetValue(this.OpacityProperty); } set { this.SetValue(this.OpacityProperty, value); } }
:Wq
        public DirectProperty<SensorCustomControl, double> RotationProperty = AvaloniaProperty.RegisterDirect<SensorCustomControl, double>(nameof(Rotation), s => s.Rotation, (s, v) => s.Rotation = v);

        public DirectProperty<SensorCustomControl, double> DistanceProperty = AvaloniaProperty.RegisterDirect<SensorCustomControl, double>(nameof(Distance), s => s.Distance, (s, v) => s.Distance = v);

        public DirectProperty<SensorCustomControl, double> FOVProperty = AvaloniaProperty.RegisterDirect<SensorCustomControl, double>(nameof(FOV), s => s.FOV, (s, v) => s.FOV = v);

        public StyledProperty<SolidColorBrush> BrushProperty = AvaloniaProperty.Register<SensorCustomControl, SolidColorBrush>(nameof(Brush));

        public StyledProperty<Vector> OffsetProperty = AvaloniaProperty.Register<SensorCustomControl, Vector>(nameof(Offset));

        public StyledProperty<byte> OpacityProperty = AvaloniaProperty.Register<SensorCustomControl, byte>(nameof(Opacity));

        public SensorCustomControl()
        {
            this.RenderTransform = new RotateTransform(-90 + this.Rotation);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform(-90 + this.Rotation));
            transformGroup.Children.Add(new TranslateTransform(this.Offset.X, this.Offset.Y));

            this.RenderTransform = transformGroup;
            context.DrawGeometry(new SolidColorBrush(new Color(this.Opacity, this.Brush.Color.R, this.Brush.Color.G, this.Brush.Color.B)), new Pen(), this.GetRectangleGeometry(this.FOV, this.Distance * SCALER));
        }

        private PolylineGeometry GetRectangleGeometry(double degrees, double height)
        {
            var points = new List<Point>();

            points.Add(new Point(0, 0));
            points.Add(new Point(height, height * Math.Tan((degrees / 2) * (Math.PI / 180))));
            points.Add(new Point(height, -1 * height * Math.Tan((degrees / 2) * (Math.PI / 180))));

            return new PolylineGeometry(points, true);
        }
    }
}
