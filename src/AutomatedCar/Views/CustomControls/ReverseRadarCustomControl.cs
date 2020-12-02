namespace AutomatedCar.Views.CustomControls
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;

    class ReverseRadarCustomControl : Control
    {
        public ReverseRadarCustomControl()
        {
        }

        public override void Render(DrawingContext context)
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.HMIPacket.Gear != Gears.R)
            {
                return;
            }

            double length = World.Instance.ControlledCar.Ultrasounds[0].maxReach;
            double quarterLength = length / 4;
            double halfLength = length / 2;
            int startX = 37;
            int startY = 119;
            int shift = 40;
            int thickness = 2;

            Point redStart = new Point(startX, startY - shift);
            Point redRight = new Point(startX, startY + quarterLength);
            Point redEnd = new Point(-startX, startY - shift);
            Point redLeft = new Point(-startX, startY + quarterLength);
            List<Point> reds = new List<Point>()
            {
                redStart,
                redRight,
                redLeft,
                redEnd,
                new Point(redEnd.X + thickness, redEnd.Y),
                new Point(redLeft.X + thickness, redLeft.Y - thickness),
                new Point(redRight.X - thickness, redRight.Y - thickness),
                new Point(redStart.X - thickness, redStart.Y),
            };

            Point yellowStart = redRight;
            Point yellowRight = new Point(yellowStart.X, yellowStart.Y + quarterLength);
            Point yellowEnd = redLeft;
            Point yellowLeft = new Point(yellowEnd.X, yellowEnd.Y + quarterLength);
            List<Point> yellows = new List<Point>()
            {
                yellowStart,
                yellowRight,
                yellowLeft,
                yellowEnd,
                new Point(yellowEnd.X + thickness, yellowEnd.Y),
                new Point(yellowLeft.X + thickness, yellowLeft.Y - thickness),
                new Point(yellowRight.X - thickness, yellowRight.Y - thickness),
                new Point(yellowStart.X - thickness, yellowStart.Y),
            };

            Point greenStart = yellowRight;
            Point greenRight = new Point(greenStart.X, greenStart.Y + halfLength);
            Point greenEnd = yellowLeft;
            Point greenLeft = new Point(greenEnd.X, greenEnd.Y + halfLength);
            List<Point> greens = new List<Point>()
            {
                greenStart,
                greenRight,
                greenLeft,
                greenEnd,
                new Point(greenEnd.X + thickness, greenEnd.Y),
                new Point(greenLeft.X + thickness, greenLeft.Y - thickness),
                new Point(greenRight.X - thickness, greenRight.Y - thickness),
                new Point(greenStart.X - thickness, greenStart.Y),
            };

            context.DrawGeometry(new SolidColorBrush(new Color(255, 255, 0, 0)), new Pen(), new PolylineGeometry(reds, true));
            context.DrawGeometry(new SolidColorBrush(new Color(255, 255, 255, 0)), new Pen(), new PolylineGeometry(yellows, true));
            context.DrawGeometry(new SolidColorBrush(new Color(255, 0, 128, 0)), new Pen(), new PolylineGeometry(greens, true));

            base.Render(context);
        }
    }
}
