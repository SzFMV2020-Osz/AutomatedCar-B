namespace AutomatedCar
{
    using System;
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
                JsonParser parser = new JsonParser();
                parser.populateWorldObjects(World.Instance, $"AutomatedCar.Assets.test_world.json");

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

                int W = 108;
                int H = 240;

                List<List<Point>> polyList = new List<List<Point>>();
                polyList.Add(points);
                AutomatedCar controlledCar = new Models.AutomatedCar(0, 0, "car_1_white", W, H, polyList);

                controlledCar.RadarBrush = new SolidColorBrush(Color.Parse("blue"));
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

                NpcCar npcCar = new NpcCar("car_1_red", W, H, polyList, $"AutomatedCar.Assets.npcCarRoute.json");
                npcCar.SetStartPosition();
                world.AddObject(npcCar);

                NpcPedestrian npcPedMan = new NpcPedestrian("man", W / 3, H / 3, polyList, $"AutomatedCar.Assets.npcPedRoute1.json");
                npcPedMan.SetStartPosition();
                world.AddObject(npcPedMan);

                NpcPedestrian npcPedWoman = new NpcPedestrian("woman", W / 3, H / 3, polyList, $"AutomatedCar.Assets.npcPedRoute2.json");
                npcPedWoman.SetStartPosition();
                world.AddObject(npcPedWoman);

                world.OnTick += npcCar.Move;
                world.OnTick += npcPedMan.Move;
                world.OnTick += npcPedWoman.Move;

                Game game = new Game(world);
                game.Start();

                desktop.MainWindow = new MainWindow {DataContext = new MainWindowViewModel(world)};
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}