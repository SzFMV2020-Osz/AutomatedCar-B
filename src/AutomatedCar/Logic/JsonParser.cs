using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using AutomatedCar.Models;
using System.Reflection;
using Avalonia.Media.Imaging;

namespace AutomatedCar.Logic
{
    using System;
    using NetTopologySuite.Geometries;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    /// <summary>
    /// Json Parser class.
    /// </summary>
    public class JsonParser
    {
        private World world;
        private IList<JToken> polygonList;
        private IList<JToken> refPointList;
        private List<JsonWorldObject> worldObjectParameters;
        private List<JsonPolygon> polygonParameters;
        private List<JsonReferences> referencesParameters;
        private List<List<Polygon>> parsedPolygonLists;

        public void populateWorldObjects(World world, string woJson)
        {
            this.world = world;

            StreamReader worldReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(woJson));
            StreamReader polygonReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCarTests.Assets.worldobject_polygons.json"));
            StreamReader refPointReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCarTests.Assets.reference_points.json"));

            this.worldObjectParameters = new List<JsonWorldObject>();
            this.polygonParameters = new List<JsonPolygon>();
            this.referencesParameters = new List<JsonReferences>();

            this.FillWorldObjectParameterList(worldReader);
            this.FillPoinsForPolygonsForEachObject(polygonReader);
            this.FillReferencePointsForEachObject(refPointReader);
            this.FillWorldObjects();
        }

        private void FillWorldObjectParameterList(StreamReader worldReader)
        {
            JObject unparsedObjectList = JObject.Parse(worldReader.ReadToEnd());

            this.world.Height = int.Parse(unparsedObjectList["height"].ToString());
            this.world.Width = int.Parse(unparsedObjectList["width"].ToString());

            IList<JToken> objectList = unparsedObjectList["objects"].Children().ToList();
            foreach (JToken obj in objectList)
            {
                string filename = obj["type"].ToString();

                Bitmap image = null;

                try
                {
                    image = new Bitmap(Assembly.GetExecutingAssembly()
                                .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{filename}.png"));
                }
                catch (ArgumentNullException e)
                {
                    image = new Bitmap(Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.ImageNotFound.png"));
                }

                this.worldObjectParameters.Add(new JsonWorldObject()
                {
                    X = int.Parse(obj["x"].ToString()),
                    Y = int.Parse(obj["y"].ToString()),
                    Filename = filename,
                    Width = (int)image.Size.Width,
                    Height = (int)image.Size.Height,
                    Rotmatrix = new Avalonia.Matrix(float.Parse(obj["m11"].ToString()), float.Parse(obj["m12"].ToString()), float.Parse(obj["m21"].ToString()), float.Parse(obj["m22"].ToString()), 0, 0),
                });
            }
        }

        private void FillPoinsForPolygonsForEachObject(StreamReader polygonReader)
        {
            JObject unparsedPolygonList = JObject.Parse(polygonReader.ReadToEnd());

            IList<JToken> objectList = unparsedPolygonList["objects"].Children().ToList();

            foreach (JsonWorldObject parameter in this.worldObjectParameters)
            {
               // var polys = objectList.FirstOrDefault(o => o["typename"] != null && o["typename"].ToString() == parameter.Filename)["polys"];

                JToken polys = null;
                foreach (var obj in objectList)
                {
                    if (obj["typename"].ToString() == parameter.Filename)
                    {
                        polys = obj["polys"];
                        break;
                    }
                }

                List<List<Point>> pointLists = new List<List<Point>>();

                if (polys != null)
                {
                    foreach (var poly in polys)
                    {
                        List<Point> pointList = new List<Point>();
                        var points = poly["points"];
                        foreach (var point in points)
                        {
                            string[] xystrings = point.ToString().Split(',');
                            pointList.Add(new Point(double.Parse(xystrings[0].Where(char.IsDigit).ToArray()), double.Parse(xystrings[1].Where(char.IsDigit).ToArray())));
                        }

                        pointLists.Add(pointList);
                    }
                }

                this.polygonParameters.Add(new JsonPolygon() { PolyPoints = pointLists });
            }
        }

        private void FillReferencePointsForEachObject(StreamReader refPointReader)
        {
            JArray unparsedRefPointsList = JArray.Parse(refPointReader.ReadToEnd());

            foreach (JsonWorldObject parameter in this.worldObjectParameters)
            {
                var refPoint = unparsedRefPointsList.FirstOrDefault(r => r["name"].ToString().Substring(0, r["name"].ToString().LastIndexOf(".")) == parameter.Filename);

                if (refPoint != null)
                {
                    this.referencesParameters.Add(new JsonReferences()
                    {
                        ReferenceOffsetX = -int.Parse(refPoint["x"].ToString()),
                        ReferenceOffsetY = -int.Parse(refPoint["y"].ToString()),
                    });
                }
                else
                {
                    this.referencesParameters.Add(new JsonReferences());
                }
            }
        }

        private void FillWorldObjects()
        {
            for (int i = 0; i < this.worldObjectParameters.Count; i++)
            {
                var wo = this.CreateWorldObject(this.worldObjectParameters[i], this.polygonParameters[i], this.referencesParameters[i]);
                if (wo != null)
                {
                    this.world.AddObject(wo);
                }
            }
        }

        private WorldObject CreateWorldObject(JsonWorldObject jsonWorldObject, JsonPolygon jsonPolygon, JsonReferences jsonReferences)
        {
            WorldObject currentObject = null;
            string type = jsonWorldObject.Filename;
            if (isRoadWith3Polygons(type))
            {
                currentObject = new Road(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (isSign(type))
            {
                currentObject = new Sign(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "road_2lane_tjunctionleft" ||
                     type == "road_2lane_tjunctionright")
            {
                currentObject = new TjunctionRoad(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "2_crossroad_1")
            {
                currentObject = new CrossRoad18(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "2_crossroad_2")
            {
                currentObject = new CrossRoad16(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "road_2lane_rotary")
            {
                currentObject = new RotaryRoad(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "parking_space_parallel")
            {
                currentObject = new ParallelParking(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "parking_90")
            {
                currentObject = new CrossParking(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "garage")
            {
                currentObject = new Garage(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "tree")
            {
                currentObject = new Tree(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "crosswalk")
            {
                currentObject = new CrossWalk(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix);
            }

            return currentObject;
        }

        private static bool isSign(string type)
        {
            return type == "roadsign_parking_right" ||
                   type == "roadsign_priority_stop" ||
                   type == "roadsign_speed_40" ||
                   type == "roadsign_speed_50" ||
                   type == "roadsign_speed_60";
        }

        private static bool isRoadWith3Polygons(string type)
        {
            return type == "road_2lane_90right" ||
                   type == "road_2lane_45right" ||
                   type == "road_2lane_45left" ||
                   type == "road_2lane_6right" ||
                   type == "road_2lane_6left" ||
                   type == "road_2lane_straight" ||
                   type == "road_2lane_90left";
        }
    }
}
