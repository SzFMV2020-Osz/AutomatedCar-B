using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using Avalonia;
using Avalonia.Controls.Shapes;
using AutomatedCar.Models;
using System.Reflection;

namespace AutomatedCar.Logic
{
    /// <summary>
    /// Json Parser class.
    /// </summary>
    public class JsonParser
    {
        private World world;
        private IList<JToken> objectList;
        private IList<JToken> polygonList;
        private List<List<Polygon>> parsedPolygonList;


        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser"/> class.
        /// </summary>
        /// <param name="wojson">World object json location.</param>
        /// <param name="polygonjson">Polygon json location.</param>
        public JsonParser(string woJson, string polygonJson)
        {
            StreamReader worldReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(woJson));
            StreamReader polygonReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(polygonJson));
            this.world = new World();

            JObject unparsedObjectList = JObject.Parse(worldReader.ReadToEnd());
            JObject unparsedPolygonList = JObject.Parse(polygonReader.ReadToEnd());

            // world szélessség, magasság beállítása
            this.world.Height = int.Parse(unparsedObjectList["height"].ToString());
            this.world.Width = int.Parse(unparsedObjectList["width"].ToString());

            // Objectek átadása a listának
            this.objectList = unparsedObjectList["objects"].Children().ToList();

            // Polygon Objectek átadása a listának
            this.polygonList = unparsedPolygonList["objects"].Children().ToList();

        }

        /// <summary>
        /// Creates the world.
        /// </summary>
        /// <returns>Retun world object.</returns>
        public World CreateWorld()
        {
            // Feltöltjük a polygonokkal a polygon listát
            this.parsedPolygonList = this.PolygonLoader();

            // Feltöltjük a worldobjectekkel a world-t
            this.WorldObjectLoader();

            return this.world;
        }

        private void WorldObjectLoader()
        {
            foreach (JToken obj in this.objectList)
            {
                string type = obj["type"].ToString();
                int x = int.Parse(obj["x"].ToString());
                int y = int.Parse(obj["y"].ToString());
                RotationMatrix rm = new RotationMatrix(double.Parse(obj["m11"].ToString()), double.Parse(obj["m12"].ToString()), double.Parse(obj["m21"].ToString()), double.Parse(obj["m22"].ToString()));

                List<Polygon> temp = new List<Polygon>();
                foreach (List<Polygon> polygonList in this.parsedPolygonList)
                {
                    foreach (Polygon polygon in polygonList)
                    {
                        if (polygon.Name == type)
                        {
                            temp = polygonList;
                            break;
                        }
                    }
                }

                switch (type)
                {
                    // ROADS
                    case "road_2lane_90right":
                        Road road = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road);
                        break;
                    case "road_2lane_45right":
                        Road road2 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road2);
                        break;
                    case "road_2lane_45left":
                        Road road3 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road3);
                        break;
                    case "road_2lane_6right":
                        Road road4 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road4);
                        break;
                    case "road_2lane_6left":
                        Road road5 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road5);
                        break;
                    case "road_2lane_straight":
                        Road road6 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road6);
                        break;
                    case "road_2lane_90left":
                        Road road7 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road7);
                        break;
                    case "2_crossroad_1":
                        Road road8 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road8);
                        break;
                    case "2_crossroad_2":
                        Road road9 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road9);
                        break;
                    case "road_2lane_rotary":
                        Road road10 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road10);
                        break;
                    case "road_2lane_tjunctionleft":
                        Road road11 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road11);
                        break;
                    case "road_2lane_tjunctionright":
                        Road road12 = new Road(x, y, type, false, rm, temp);
                        this.world.AddObject(road12);
                        break;

                    // PARKING
                    case "parking_90":
                        Parking parking = new Parking(x, y, type, false, rm, temp);
                        this.world.AddObject(parking);
                        break;
                    case "parking_space_parallel":
                        Parking parking2 = new Parking(x, y, type, false, rm, temp);
                        this.world.AddObject(parking2);
                        break;

                    // SIGNS
                    case "roadsign_parking_right":
                        Sign sign = new Sign(x, y, type, true, rm, temp.First());
                        this.world.AddObject(sign);
                        break;
                    case "roadsign_priority_stop":
                        Sign sign1 = new Sign(x, y, type, true, rm, temp.First());
                        this.world.AddObject(sign1);
                        break;
                    case "roadsign_speed_40":
                        Sign sign2 = new Sign(x, y, type, true, rm, temp.First());
                        this.world.AddObject(sign2);
                        break;
                    case "roadsign_speed_50":
                        Sign sign3 = new Sign(x, y, type, true, rm, temp.First());
                        this.world.AddObject(sign3);
                        break;
                    case "roadsign_speed_60":
                        Sign sign4 = new Sign(x, y, type, true, rm, temp.First());
                        this.world.AddObject(sign4);
                        break;

                    // EGYÉB
                    case "garage":
                        Garage garage = new Garage(x, y, type, true, rm, temp.First());
                        this.world.AddObject(garage);
                        break;
                    case "tree":
                        Tree tree = new Tree(x, y, type, true, rm, temp.First());
                        this.world.AddObject(tree);
                        break;
                    case "bollard":
                        Circle bollard = new Circle(x, y, type, 100, true, new RotationMatrix(1,0,0,1), temp.First());   // circle radiusa mennyi?
                        this.world.AddObject(bollard);
                        break;

                    // van még egy boundary nevű cucc is, azt kikéne deríteni hogy micsoda?
                }
            }
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
