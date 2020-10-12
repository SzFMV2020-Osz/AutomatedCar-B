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
        private double height = 50;
        private double fov = 30;

        public double Rotation { get { return this.rotation; } set { this.SetAndRaise(this.RotationProperty, ref this.rotation, value); } }

        public double Height { get { return this.height; } set { this.SetAndRaise(this.HeightProperty, ref this.height, value); } }

        public double FOV { get { return this.fov; } set { this.SetAndRaise(this.FOVProperty, ref this.fov, value); } }

        public SolidColorBrush Brush { get { return this.GetValue(this.BrushProperty); } set { this.SetValue(this.BrushProperty, value); } }

        public Vector Offset { get { return this.GetValue(this.OffsetProperty); } set { this.SetValue(this.OffsetProperty, value); } }

        public byte Opacity { get { return this.GetValue(this.OpacityProperty); } set { this.SetValue(this.OpacityProperty, value); } }


        public DirectProperty<SensorCustomControl, double> RotationProperty = AvaloniaProperty.RegisterDirect<SensorCustomControl, double>(nameof(Rotation), s => s.Rotation, (s, v) => s.Rotation = v);

        public DirectProperty<SensorCustomControl, double> HeightProperty = AvaloniaProperty.RegisterDirect<SensorCustomControl, double>(nameof(Height), s => s.Height, (s, v) => s.Height = v);

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

            this.RenderTransform = new RotateTransform(-90 + this.Rotation);
            context.DrawGeometry(new SolidColorBrush(new Color(this.Opacity, this.Brush.Color.R, this.Brush.Color.G, this.Brush.Color.B)), new Pen(), this.GetRectangleGeometry(this.FOV, this.Height * SCALER, this.Offset));
        }

        private PolylineGeometry GetRectangleGeometry(double degrees, double height, Vector offset)
        {
            var points = new List<Point>();

            points.Add(new Point(this.Offset.X, this.Offset.Y));
            points.Add(new Point(this.Offset.X + height, (this.Offset.Y + height) * Math.Tan((degrees / 2) * (Math.PI / 180))));
            points.Add(new Point(this.Offset.X + height, -(this.Offset.Y + height) * Math.Tan((degrees / 2) * (Math.PI / 180))));

            return new PolylineGeometry(points, true);
        }
    }
}
