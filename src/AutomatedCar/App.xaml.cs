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
    using Logic;
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
                JsonParser parser = new JsonParser($"AutomatedCar.Assets.test_world.json", $"AutomatedCar.Assets.worldobject_polygons.json", World.Instance);
                parser.CreateWorld();

                var world = World.Instance;

                StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.worldobject_polygons.json"));
                string json_text = reader.ReadToEnd();
                dynamic stuff = JObject.Parse(json_text);
                List<Point> points = new List<Point>();
                foreach (var i in stuff["objects"][0]["polys"][0]["points"])
                {
                    points.Add(new Point(i[0].ToObject<int>(), i[1].ToObject<int>()));
                }

                PolylineGeometry geom = new PolylineGeometry(points, false);

                AutomatedCar controlledCar2 = new Models.AutomatedCar(50, 50, "car_1_red");
                controlledCar2.Geometry = geom;
                controlledCar2.Width = 108;
                controlledCar2.Height = 240;
                World.Instance.AddObject(controlledCar2);

                int W = 108;
                int H = 240;

                AutomatedCar controlledCar = new Models.AutomatedCar(50+(W/2), 50+(H/2), "car_1_white");
                controlledCar.Angle = 90;
                controlledCar.Width = W;
                controlledCar.Height = H;
                controlledCar.referenceOffsetX = -(controlledCar.Width/2);
                controlledCar.referenceOffsetY = -(controlledCar.Height/2);

                controlledCar.RadarBrush = new SolidColorBrush(Color.Parse("blue"));
                controlledCar.UltraSoundBrush = new SolidColorBrush(Color.Parse("green"));
                controlledCar.CameraBrush = new SolidColorBrush(Color.Parse("red"));

                List<Point> sensorPoints = new List<Point>();
                sensorPoints.Add(new Point(51, 239));
                sensorPoints.Add(new Point(200, 100));
                sensorPoints.Add(new Point(100, 300));

                List<Point> cameraSensorPoints = new List<Point>();
                cameraSensorPoints.Add(new Point(100, 200));
                cameraSensorPoints.Add(new Point(300, 200));
                cameraSensorPoints.Add(new Point(150, 300));

                controlledCar.RadarGeometry = new PolylineGeometry(sensorPoints, false);

                controlledCar.Geometry = geom;
                controlledCar.RadarVisible = true;
                controlledCar.UltraSoundVisible = true;

                controlledCar.CameraGeometry = new PolylineGeometry(cameraSensorPoints, false);
                controlledCar.CameraVisible = true;

                world.AddObject(controlledCar);
                world.ControlledCar = controlledCar;
                controlledCar.Start();

                Game game = new Game(world);
                game.Start();

                desktop.MainWindow = new MainWindow {DataContext = new MainWindowViewModel(world)};
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}