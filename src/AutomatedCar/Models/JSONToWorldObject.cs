using Avalonia;
using Avalonia.Controls.Shapes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutomatedCar.Models
{
    public static class JSONToWorldObject
    {
        public static List<WorldObject> LoadAllObjectsFromJSON(string configFilename)
        {
            // másik függvény
            JObject configFilenames = JObject.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + configFilename)).ReadToEnd());

            var allObjects = ReadWorldObjects(configFilenames["world_objects"].ToString());
            var onlyPolygons = ReadPolygons(configFilenames["polygons"].ToString());
            var onlyRotationPoints = ReadRotationPoints(configFilenames["rotation_points"].ToString());

            // ez másik függvénybe megy -> merge a függvényekkel
            foreach (var worldObject in allObjects)
            {
                foreach (var polygon in onlyPolygons)
                {
                    if (worldObject.FileName == polygon.FileName) worldObject.Polygon = polygon.Polygon;
                }

                foreach (var rotationPoint in onlyRotationPoints)
                {
                    if (worldObject.FileName == rotationPoint.FileName)
                    {
                        worldObject.RotationCenterPointX = rotationPoint.RotationCenterPointX;
                        worldObject.RotationCenterPointY = rotationPoint.RotationCenterPointY;
                    }
                }
            }

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

        private static List<WorldObject> ReadPolygons(string filename)
        {
            List<WorldObject> polygons;

            JObject polygonsInFile = JObject.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + filename)).ReadToEnd());

            IList<JToken> polygonsInJson = polygonsInFile["objects"].Children().ToList();

            polygons = polygonsInJson.Select(polygon => new WorldObject
            {
                FileName = (string)polygon["typename"],
                Polygon = polygon["polys"].Children()["points"].Select(pointlist => new Polygon
                {
                    Points = pointlist.Select(point => new Point((double)point[0], (double)point[1])).ToList(),
                }).ToList().ToArray(),
            }).ToList();

            return polygons;
        }

        private static List<WorldObject> ReadRotationPoints(string filename)
        {
            List<WorldObject> rotationPoints;

            var rotationPointsInFile = JArray.Parse(new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets." + filename)).ReadToEnd());

            rotationPoints = rotationPointsInFile.Children().ToList().Select(rotationpoint => new WorldObject
            {
                FileName = rotationpoint["name"].ToString().Substring(0, rotationpoint["name"].ToString().LastIndexOf(".")),
                RotationCenterPointX = (int)rotationpoint["x"],
                RotationCenterPointY = (int)rotationpoint["y"],
            }).ToList();

            return rotationPoints;
        }
    }
}
