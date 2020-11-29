namespace AutomatedCar.Views.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia.Controls;
    using Avalonia.Media;

    public class ReversingRadar : Control
    {
        TextBlock leftText = new TextBlock()
        {
            FontSize = 18,
            Text = "L",
            Padding = new Avalonia.Thickness(5),
        };

        TextBlock rightText = new TextBlock()
        {
            FontSize = 18,
            Text = "R",
            Padding = new Avalonia.Thickness(5),
        };

        public override void Render(DrawingContext context)
        {
            AutomatedCar car = World.Instance.ControlledCar;
            if (car.VirtualFunctionBus.HMIPacket.Gear != Gears.R)
            {
                return;
            }

            double leftDistance = car.Ultrasounds[6].Distance;
            double rightDistance = car.Ultrasounds[4].Distance;

            TextBlock centerText = new TextBlock()
            {
                Text = Math.Min(leftDistance, rightDistance).ToString(),
                FontSize = 30,
                Padding = new Avalonia.Thickness(10),
            };

            ProgressBar leftBar = new ProgressBar()
            {
                MaxWidth = 150,
                Height = 25,
                Value = this.DistanceConvert(leftDistance),
                Minimum = 0,
                Maximum = 100,
            };

            RotateTransform rotate = new RotateTransform()
            {
                Angle = 180,
            };

            ProgressBar righBar = new ProgressBar()
            {
                MaxWidth = 150,
                Height = 25,
                Value = this.DistanceConvert(rightDistance),
                Minimum = 0,
                Maximum = 100,
                RenderTransform = rotate,
            };

            //WrapPanel wp = new WrapPanel
            //{
            //    Children = new Controls
            //    {
            //        this.leftText,
            //        leftBar,
            //        centerText,
            //        righBar,
            //        this.rightText,
            //    },
            //};

            base.Render(context);
        }

        private double DistanceConvert(double val)
        {
            double max = World.Instance.ControlledCar.Ultrasounds[0].maxReach;
            if (val == max + 1)
            {
                return 0;
            }

            return (max - val) / max * 100;
        }
    }
}
