using Avalonia;
using Avalonia.Controls.Shapes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NetTopologySuite.Geometries;

namespace AutomatedCar.Models
{
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public static class JSONToWorldObject
    {
        /// <summary>
        /// Loads WorldOjbect from JSON files given in input.
        /// </summary>
        /// <param name="configFilename">This json file specifies the other files used.</param>
        /// <returns>A list of WorldObjects populated by data from the files.</returns>
        public static List<WorldObject> LoadAllObjectsFromJSON(string configFilename)
        {
            JObject configFilenames = JObject.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + configFilename)).ReadToEnd());

            var allObjects = ReadWorldObjects(configFilenames["world_objects"].ToString());
            ReadPolygons(configFilenames["polygons"].ToString(), allObjects);
            ReadRotationPoints(configFilenames["rotation_points"].ToString(), allObjects);

            return allObjects;
        }

        private static List<WorldObject> ReadWorldObjects(string filename)
        {
            List<WorldObject> allObjects;

            JObject worldObjectsInFile = JObject.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + filename)).ReadToEnd());

            IList<JToken> results = worldObjectsInFile["objects"].Children().ToList();

            allObjects = results.Select(r => new WorldObject
            {
                X = (int)r["x"],
                Y = (int)r["y"],
                FileName = (string)r["type"],
                Angle = Math.Acos((double)r["m11"]),
            }).ToList();

            return allObjects;
        }

        private static void ReadPolygons(string filename, List<WorldObject> allObjects)
        {
            JObject polygonsInFile = JObject.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + filename)).ReadToEnd());

            IList<JToken> polygonsInJson = polygonsInFile["objects"].Children().ToList();

            foreach (var worldObject in allObjects)
            {
                foreach (var polygons in polygonsInJson)
                {
                    if (worldObject.FileName == (string)polygons["typename"])
                    {

                        worldObject.NetPolygon = polygons["polys"].Children()["points"].Select(pointlist =>
                            new NetTopologySuite.Geometries.LineString(
                                pointlist.Select(p => new Coordinate((double)p[0] + worldObject.X, (double)p[1] + worldObject.Y)).ToArray()
                            )).ToArray();

                        worldObject.Polygon = polygons["polys"].Children()["points"].Select(pointlist => new Polygon
                        {
                            Points = pointlist.Select(point => new Point((double)point[0] + worldObject.X, (double)point[1] + worldObject.Y)).ToList(),
                        }).ToList().ToArray();
                    }
                }
            }
        }

        private static void ReadRotationPoints(string filename, List<WorldObject> allObjects)
        {
            var rotationPointsInFile = JArray.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + filename)).ReadToEnd());

            foreach (var worldObject in allObjects)
            {
                foreach (var rotationPoint in rotationPointsInFile)
                {
                    if (worldObject.FileName == rotationPoint["name"].ToString().Substring(0, rotationPoint["name"].ToString().LastIndexOf(".")))
                    {
                        worldObject.RotationCenterPointX = (int)rotationPoint["x"];
                        worldObject.RotationCenterPointY = (int)rotationPoint["y"];
                    }
                }
            }
        }
    }
}
