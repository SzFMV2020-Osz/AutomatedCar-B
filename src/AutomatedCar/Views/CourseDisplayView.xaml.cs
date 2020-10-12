namespace AutomatedCar.Views
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    using AutomatedCar.Models;

    public class CourseDisplayView : UserControl
    {
        public CourseDisplayView()
        {
            this.InitializeComponent();

            var scrollViewer = this.Get<ScrollViewer>("MyScrollViewer");

            World.Instance.OnTick += (x,y) =>
            {
                scrollViewer.Offset = ScreenPositioner.CalcCameraPosition();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}