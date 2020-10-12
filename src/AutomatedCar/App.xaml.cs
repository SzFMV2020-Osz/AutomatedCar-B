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
    using NetTopologySuite.Geometries;
    using Newtonsoft.Json.Linq;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

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
                List<Avalonia.Point> points = new List<Avalonia.Point>();
                foreach (var i in stuff["objects"][0]["polys"][0]["points"])
                {
                    points.Add(new Avalonia.Point(i[0].ToObject<int>(), i[1].ToObject<int>()));
                }

                PolylineGeometry geom = new PolylineGeometry(points, false);

                var pointlist = new List<Coordinate>();
                pointlist.Add(new Coordinate(0,0));
                pointlist.Add(new Coordinate(70,0));

                var pointlist2 = new List<Coordinate>();
                pointlist2.Add(new Coordinate(70,70));
                pointlist2.Add(new Coordinate(100,100));

                int W = 108;
                int H = 240;

                AutomatedCar controlledCar = new Models.AutomatedCar(50+(W/2), 50+(H/2), "car_1_white");
                controlledCar.Angle = 90;
                controlledCar.Width = W;
                controlledCar.Height = H;
                controlledCar.referenceOffsetX = -(controlledCar.Width/2);
                controlledCar.referenceOffsetY = -(controlledCar.Height/2);
                controlledCar.NetPolygon = new List<NetTopologySuite.Geometries.LineString>();
                controlledCar.NetPolygon.Add(new NetTopologySuite.Geometries.LineString(pointlist.ToArray()));

                World.Instance.AddObject(new Road(500, 500, "crosswalk", false, new RotationMatrix(1, 0, 0, 1), new List<Polygon>()
                {
                    new Polygon() { Points = new List<Point>(){new Point(10,10), new Point(50, 10)}},
                    new Polygon() { Points = new List<Point>(){new Point(20,20), new Point(50, 20)}},
                    new Polygon() { Points = new List<Point>(){new Point(30,30), new Point(50, 30)}},
                }));


                controlledCar.RadarBrush = new SolidColorBrush(Color.Parse("blue"));
                controlledCar.UltraSoundBrush = new SolidColorBrush(Color.Parse("green"));
                controlledCar.CameraBrush = new SolidColorBrush(Color.Parse("red"));

                List<Avalonia.Point> sensorPoints = new List<Avalonia.Point>();
                sensorPoints.Add(new Avalonia.Point(51, 239));
                sensorPoints.Add(new Avalonia.Point(200, 100));
                sensorPoints.Add(new Avalonia.Point(100, 300));

                List<Avalonia.Point> cameraSensorPoints = new List<Avalonia.Point>();
                cameraSensorPoints.Add(new Avalonia.Point(100, 200));
                cameraSensorPoints.Add(new Avalonia.Point(300, 200));
                cameraSensorPoints.Add(new Avalonia.Point(150, 300));

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