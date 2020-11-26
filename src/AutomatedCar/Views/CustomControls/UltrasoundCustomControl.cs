using AutomatedCar.Models;
using AutomatedCar.SystemComponents;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using RTools_NTS.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomatedCar.Views.CustomControls
{
    class UltrasoundCustomControl : Control
    {
        private byte indexer;
        private SolidColorBrush brush;
        private byte opacity;

        public byte Indexer { get { return this.indexer; } set { this.SetAndRaise(this.IndexerProperty, ref this.indexer, value); } }

        public SolidColorBrush Brush { get => this.brush; set { this.brush = value; } }

        public byte Opacity { get => this.opacity; set { this.opacity = value; } }

        public DirectProperty<UltrasoundCustomControl, byte> IndexerProperty = AvaloniaProperty.RegisterDirect<UltrasoundCustomControl, byte>(nameof(Indexer), s => s.Indexer, (s, v) => s.Indexer = v);

        public StyledProperty<SolidColorBrush> BrushProperty = AvaloniaProperty.Register<UltrasoundCustomControl, SolidColorBrush>(nameof(Brush));

        public StyledProperty<byte> OpacityProperty = AvaloniaProperty.Register<UltrasoundCustomControl, byte>(nameof(Opacity));

        public UltrasoundCustomControl()
        {
            // this.RenderTransform = new RotateTransform(0);
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
                var car = World.Instance.ControlledCar;
                var radAngle = car.Angle * Math.PI / 180;
                car.RotMatrix = new Matrix(Math.Cos(radAngle), -Math.Sin(radAngle), Math.Sin(radAngle), Math.Cos(radAngle), 0, 0);
                var p = sensor.CalculatePoints();
                var start = p[0];
                List<Point> p2 = new List<Point>() { new Point(start.X + 10, start.Y + 10), new Point(start.X - 10, start.Y - 10), new Point(start.X + 10, start.Y - 10), new Point(start.X - 10, start.Y + 10) };
                geometry = new PolylineGeometry(p2, true);
            }
            else
            {
                var start = sensor.Points[0];
                List<Point> p2 = new List<Point>() { new Point(start.X + 10, start.Y + 10), new Point(start.X - 10, start.Y - 10), new Point(start.X + 10, start.Y - 10), new Point(start.X - 10, start.Y + 10) };
                geometry = new PolylineGeometry(p2, true);
            }

            this.Brush = sensor.Brush;

            this.Opacity = 60;
            context.DrawGeometry(new SolidColorBrush(new Color(this.Opacity, this.Brush.Color.R, this.Brush.Color.G, this.Brush.Color.B)), new Pen(), geometry);
        }
    }
}
