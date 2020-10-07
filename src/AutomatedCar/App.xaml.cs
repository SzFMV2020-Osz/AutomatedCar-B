namespace AutomatedCar
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using AutomatedCar.Models;
    using AutomatedCar.ViewModels;
    using AutomatedCar.Views;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;
    using Avalonia.Media;
    using Newtonsoft.Json.Linq;

    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.worldobject_polygons.json"));
                string json_text = reader.ReadToEnd();
                dynamic stuff = JObject.Parse(json_text);
                var points = new List<Point>();
                foreach (var i in stuff["objects"][0]["polys"][0]["points"])
                {
                    points.Add(new Point(i[0].ToObject<int>(), i[1].ToObject<int>()));
                }

                var geom = new PolylineGeometry(points, false);

                var world = World.Instance;
                world.Width = 2000;
                world.Height = 1000;

                var circle = new Circle(400, 200, "circle.png", 20);
                circle.Width = 40;
                circle.Height = 40;
                circle.ZIndex = 2;
                world.AddObject(circle);

                var controlledCar = new Models.AutomatedCar(50, 50, "car_1_white.png");
                controlledCar.Width = 108;
                controlledCar.Height = 240;
                controlledCar.Geometry = geom;
                var g = new Avalonia.Media.EllipseGeometry(new Rect(30, 30, 80, 80));
                // controlledCar.AddGeom(g);
                var l1 = new Avalonia.Media.PolylineGeometry(new List<Point> { new Point(30, 30), new Point(70, 30) }, false);
                var l2 = new Avalonia.Media.PolylineGeometry(new List<Point> { new Point(40, 40), new Point(70, 40) }, false);
                var l3 = new Avalonia.Media.PolylineGeometry(new List<Point> { new Point(50, 60), new Point(70, 60) }, false);
                controlledCar.AddGeom(l1);
                controlledCar.AddGeom(l2);
                controlledCar.AddGeom(l3);

                world.AddObject(controlledCar);
                world.ControlledCar = controlledCar;
                controlledCar.Start();

                var game = new Game(world);
                game.Start();

                desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(world) };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}