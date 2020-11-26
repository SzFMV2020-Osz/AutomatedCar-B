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
    class RadarCustomControl : Control
    {
        private byte indexer;
        private SolidColorBrush brush;
        private byte opacity;

        public byte Indexer { get { return this.indexer; } set { this.SetAndRaise(this.IndexerProperty, ref this.indexer, value); } }

        public SolidColorBrush Brush { get => this.brush; set { this.brush = value; } }

        public byte Opacity { get => this.opacity; set { this.opacity = value; } }

        public DirectProperty<RadarCustomControl, byte> IndexerProperty = AvaloniaProperty.RegisterDirect<RadarCustomControl, byte>(nameof(Indexer), s => s.Indexer, (s, v) => s.Indexer = v);

        public StyledProperty<SolidColorBrush> BrushProperty = AvaloniaProperty.Register<RadarCustomControl, SolidColorBrush>(nameof(Brush));

        public StyledProperty<byte> OpacityProperty = AvaloniaProperty.Register<RadarCustomControl, byte>(nameof(Opacity));

        public RadarCustomControl()
        {
            // this.RenderTransform = new RotateTransform(0);
        }

        public override void Render(DrawingContext context)
        {
            if (!World.Instance.ControlledCar.RadarVisible)
            {
                return;
            }

            base.Render(context);
            Radar sensor = World.Instance.ControlledCar.Radar;
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
