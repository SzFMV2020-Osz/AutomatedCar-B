using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using Avalonia;
using Avalonia.Controls.Shapes;
using AutomatedCar.Models;
using System.Reflection;
using Avalonia.Media.Imaging;

namespace AutomatedCar.Logic
{
    using System;

    /// <summary>
    /// Json Parser class.
    /// </summary>
    public class JsonParser
    {
        private World world;
        private IList<JToken> objectList;
        private IList<JToken> polygonList;
        private IList<JToken> refPointList;
        private List<List<Polygon>> parsedPolygonLists;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser"/> class.
        /// </summary>
        /// <param name="wojson">World object json location.</param>
        /// <param name="polygonjson">Polygon json location.</param>
        public JsonParser(string woJson, string polygonJson, World world)
        {
            StreamReader worldReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(woJson));
            StreamReader polygonReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(polygonJson));

            StreamReader refPointReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets.reference_points.json"));

            JObject unparsedObjectList = JObject.Parse(worldReader.ReadToEnd());
            JObject unparsedPolygonList = JObject.Parse(polygonReader.ReadToEnd());
            var unparsedRefPointsList = JArray.Parse(refPointReader.ReadToEnd());

            // world szélessség, magasság beállítása
            world.Height = int.Parse(unparsedObjectList["height"].ToString());
            world.Width = int.Parse(unparsedObjectList["width"].ToString());
            world.VisibleWidth = 960;
            world.VisibleHeight = 720;

            // Objectek átadása a listának
            this.objectList = unparsedObjectList["objects"].Children().ToList();

            // Polygon Objectek átadása a listának
            this.polygonList = unparsedPolygonList["objects"].Children().ToList();

            this.refPointList = unparsedRefPointsList.Children().ToList();

            this.world = world;
        }

        /// <summary>
        /// Creates the world.
        /// </summary>
        /// <returns>Retun world object.</returns>
        public void CreateWorld()
        {
            // Feltöltjük a polygonokkal a polygon listát
            this.parsedPolygonLists = this.PolygonLoader();

            // Feltöltjük a worldobjectekkel a world-t
            this.WorldObjectLoader();
        }

        private void WorldObjectLoader()
        {
            foreach (JToken obj in this.objectList)
            {
                string type = obj["type"].ToString();
                int x = int.Parse(obj["x"].ToString());
                int y = int.Parse(obj["y"].ToString());
                RotationMatrix rm = new RotationMatrix(double.Parse(obj["m11"].ToString()), double.Parse(obj["m12"].ToString()), double.Parse(obj["m21"].ToString()), double.Parse(obj["m22"].ToString()));
                WorldObject currentObject = CreateWorldObject(type, x, y, rm);
                try
                {
                    Bitmap image = new Bitmap(Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{type}.png"));
                    int width = (int)image.Size.Width;
                    int height = (int)image.Size.Height;
                    currentObject.Height = height;
                    currentObject.Width = width;
                }
                catch (NullReferenceException e)
                {
                }

                Dictionary<string, WorldObject> worldObjects = new Dictionary<string, WorldObject>();

                if (currentObject == null)
                {
                    continue;
                }

                var refPoint = refPointList.FirstOrDefault(r => r["name"].ToString().Substring(0, r["name"].ToString().LastIndexOf(".")) == type);

                if (refPoint != null)
                {
                    currentObject.referenceOffsetX = - int.Parse(refPoint["x"].ToString());
                    currentObject.referenceOffsetY = - int.Parse(refPoint["y"].ToString());
                }

                this.world.AddObject(currentObject);
            }
        }

        private WorldObject CreateWorldObject(string type, int x, int y, RotationMatrix rm)
        {
            var polygonsForType = this.CollectPolygonsByType(type);
            WorldObject currentObject;
            if (isRoad(type))
            {
                currentObject = new Road(x, y, type, false, rm, polygonsForType);
            }
            else if (type == "parking_90" ||
                     type == "parking_space_parallel")
            {
                currentObject = new Parking(x, y, type, false, rm, polygonsForType);
            }
            else if (type == "roadsign_parking_right" ||
                     type == "roadsign_priority_stop" ||
                     type == "roadsign_speed_40" ||
                     type == "roadsign_speed_50" ||
                     type == "roadsign_speed_60")
            {
                currentObject = new Parking(x, y, type, false, rm, polygonsForType);
            }
            else if (type == "garage")
            {
                currentObject = new Garage(x, y, type, true, rm, polygonsForType[0]);
            }
            else if (type == "tree")
            {
                currentObject = new Tree(x, y, type, true, rm, polygonsForType[0]);
            }
            else if (type == "bollard")
            {
                currentObject = new Circle(x, y, type, 1000, true, rm, polygonsForType[0]);
            }
            else
            {
                currentObject = null;
            }

            return currentObject;
        }

        private static bool isRoad(string type)
        {
            return type == "road_2lane_90right" ||
                   type == "road_2lane_45right" ||
                   type == "road_2lane_45left" ||
                   type == "road_2lane_6right" ||
                   type == "road_2lane_6left" ||
                   type == "road_2lane_straight" ||
                   type == "road_2lane_90left" ||
                   type == "2_crossroad_1" ||
                   type == "2_crossroad_2" ||
                   type == "road_2lane_rotary" ||
                   type == "road_2lane_tjunctionleft" ||
                   type == "road_2lane_tjunctionright";
        }

        private List<Polygon> CollectPolygonsByType(string type)
        {
            List<Polygon> polygons = new List<Polygon>();
            foreach (List<Polygon> polygonList in this.parsedPolygonLists)
            {
                foreach (Polygon polygon in polygonList)
                {
                    if (polygon.Name == type)
                    {
                        polygons.AddRange(polygonList);
                        break;
                    }
                }
            }

            return polygons;
        }

        private List<List<Polygon>> PolygonLoader()
        {
            List<List<Polygon>> polygonList = new List<List<Polygon>>();

            // minden object feldolgozása
            foreach (JToken obj in this.polygonList)
            {
                List<Polygon> subList = new List<Polygon>();
                string typename = obj["typename"].ToString();

                // egy object polyjai
                var polys = obj["polys"].Children();

                foreach (var polyObject in polys)
                {
                    // a poly ponjtai
                    IList<JToken> points2 = polyObject["points"].Children().ToList();
                    Polygon polyg = new Polygon();
                    polyg.Name = typename;              // mivel egy konkrét polygonnál lehet csak tárolni nevet, ezért minden polygonnál ott lesz
                    polyg.Points = new List<Point>();

                    foreach (JToken item in points2)
                    {
                        string[] xystrings = item.ToString().Split(',');
                        double x = double.Parse(xystrings[0].Where(char.IsDigit).ToArray());
                        double y = double.Parse(xystrings[0].Where(char.IsDigit).ToArray());

                        polyg.Points.Add(new Point(x, y));
                    }

                    subList.Add(polyg);
                }

                polygonList.Add(subList);
            }

            return polygonList;
        }
    }
}
