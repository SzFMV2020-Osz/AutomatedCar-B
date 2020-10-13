using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Linq;
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
                .GetManifestResourceStream($"AutomatedCar.Assets.worldobject_polygons.json"));
            StreamReader refPointReader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets.reference_points.json"));

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

                Bitmap image = new Bitmap(Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{filename}.png"));

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
            WorldObject currentObject;
            string type = jsonWorldObject.Filename;
            if (isRoad(type))
            {
                currentObject = new Road(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints);
            }
            else if (type == "parking_90" ||
                     type == "parking_space_parallel")
            {
                // Parking place, Parking bollard ...
                currentObject = new Parking(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints); ;
            }
            else if (type == "roadsign_parking_right" ||
                     type == "roadsign_priority_stop" ||
                     type == "roadsign_speed_40" ||
                     type == "roadsign_speed_50" ||
                     type == "roadsign_speed_60")
            {
                currentObject = new Sign(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints); ;
            }
            else if (type == "garage")
            {
                currentObject = new Garage(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints); ;
            }
            else if (type == "tree")
            {
                currentObject = new Tree(jsonWorldObject.X, jsonWorldObject.Y, type, jsonWorldObject.Width, jsonWorldObject.Height, jsonReferences.ReferenceOffsetX, jsonReferences.ReferenceOffsetY, jsonWorldObject.Rotmatrix, jsonPolygon.PolyPoints); ;
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


        ///// <summary>
        ///// Creates the world.
        ///// </summary>
        ///// <returns>Retun world object.</returns>
        //public void CreateWorld()
        //{
        //    // Feltöltjük a polygonokkal a polygon listát
        //    this.parsedPolygonLists = this.PolygonLoader();

        //    // Feltöltjük a worldobjectekkel a world-t
        //    this.WorldObjectLoader();
        //}

        //private void WorldObjectLoader()
        //{
        //    foreach (JToken obj in this.objectList)
        //    {
        //        string type = obj["type"].ToString();
        //        int x = int.Parse(obj["x"].ToString());
        //        int y = int.Parse(obj["y"].ToString());
        //        RotationMatrix rm = new RotationMatrix(double.Parse(obj["m11"].ToString()), double.Parse(obj["m12"].ToString()), double.Parse(obj["m21"].ToString()), double.Parse(obj["m22"].ToString()));
        //        WorldObject currentObject = CreateWorldObject(type, x, y, rm);
        //        try
        //        {
        //            Bitmap image = new Bitmap(Assembly.GetExecutingAssembly()
        //                .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{type}.png"));
        //            int width = (int)image.Size.Width;
        //            int height = (int)image.Size.Height;
        //            currentObject.Height = height;
        //            currentObject.Width = width;
        //        }
        //        catch (NullReferenceException e)
        //        {
        //        }

        //        Dictionary<string, WorldObject> worldObjects = new Dictionary<string, WorldObject>();

        //        if (currentObject == null)
        //        {
        //            continue;
        //        }

        //        var refPoint = refPointList.FirstOrDefault(r => r["name"].ToString().Substring(0, r["name"].ToString().LastIndexOf(".")) == type);

        //        if (refPoint != null)
        //        {
        //            currentObject.referenceOffsetX = - int.Parse(refPoint["x"].ToString());
        //            currentObject.referenceOffsetY = - int.Parse(refPoint["y"].ToString());

        //            // if (currentObject is Road)
        //            // {
        //            //     foreach (var place in currentObject.Polygons)
        //            //     {
        //            //
        //            //         var cords = new List<Coordinate>();
        //            //         cords.AddRange( place.Points.Select(p => new Coordinate(currentObject.RotMatrix.Rotate(p).X - currentObject.referenceOffsetX + currentObject.X, currentObject.RotMatrix.Rotate(p).Y-currentObject.referenceOffsetY+currentObject.Y)));
        //            //         currentObject.NetPolygon.Add(new LineString(cords.ToArray()));
        //            //     }
        //            // }
        //        }

        //        this.world.AddObject(currentObject);
        //    }
        //}

        //private List<Polygon> CollectPolygonsByType(string type)
        //{
        //    List<Polygon> polygons = new List<Polygon>();
        //    foreach (List<Polygon> polygonList in this.parsedPolygonLists)
        //    {
        //        foreach (Polygon polygon in polygonList)
        //        {
        //            if (polygon.Name == type)
        //            {
        //                polygons.AddRange(polygonList);
        //                break;
        //            }
        //        }
        //    }

        //    return polygons;
        //}

        //private List<List<Polygon>> PolygonLoader()
        //{
        //    List<List<Polygon>> polygonList = new List<List<Polygon>>();

        //    // minden object feldolgozása
        //    foreach (JToken obj in this.polygonList)
        //    {
        //        List<Polygon> subList = new List<Polygon>();
        //        string typename = obj["typename"].ToString();

        //        // egy object polyjai
        //        var polys = obj["polys"].Children();

        //        foreach (var polyObject in polys)
        //        {
        //            // a poly ponjtai
        //            IList<JToken> points2 = polyObject["points"].Children().ToList();
        //            Polygon polyg = new Polygon();
        //            polyg.Name = typename;              // mivel egy konkrét polygonnál lehet csak tárolni nevet, ezért minden polygonnál ott lesz
        //            polyg.Points = new List<Point>();

        //            foreach (JToken item in points2)
        //            {
        //                string[] xystrings = item.ToString().Split(',');
        //                double x = double.Parse(xystrings[0].Where(char.IsDigit).ToArray());
        //                double y = double.Parse(xystrings[1].Where(char.IsDigit).ToArray());

        //                polyg.Points.Add(new Point(x, y));
        //            }

        //            subList.Add(polyg);
        //        }

        //        polygonList.Add(subList);
        //    }

        //    return polygonList;
        //}
    }
}
