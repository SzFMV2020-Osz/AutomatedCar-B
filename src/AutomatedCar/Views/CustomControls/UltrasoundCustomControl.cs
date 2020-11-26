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
                geometry = new PolylineGeometry(sensor.CalculatePoints(), true);
            }
            else
            {
                geometry = new PolylineGeometry(sensor.Points, true);
            }

            this.Brush = sensor.Brush;

            this.Opacity = 60;
            Random r = new Random();
            context.DrawGeometry(new SolidColorBrush(new Color(this.Opacity, (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255))), new Pen(), geometry);
        }
    }
}
