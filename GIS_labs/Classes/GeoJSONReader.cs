using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GIS_labs.Classes
{
    public class GeoJSONReader
    {
        public List<MapObject> ConvertGeoJsonFeatureToMapObjects(Feature feature)
        {
            List<MapObject> mapObjects = new List<MapObject>();
            var geometry = feature.Geometry;

            switch (geometry.Type)
            {
                case GeoJSONObjectType.Point:
                    mapObjects.AddRange(HandlePointGeometry(feature));
                    break;
                case GeoJSONObjectType.LineString:
                    mapObjects.AddRange(HandleLineStringGeometry((LineString)geometry));
                    break;
                case GeoJSONObjectType.MultiLineString:
                    mapObjects.AddRange(HandleMultiLineStringGeometry((MultiLineString)geometry));
                    break;
                case GeoJSONObjectType.Polygon:
                    mapObjects.AddRange(HandlePolygonGeometry((Polygon)geometry));
                    break;
                case GeoJSONObjectType.MultiPolygon:
                    mapObjects.AddRange(HandleMultiPolygonGeometry((MultiPolygon)geometry));
                    break;
                default:
                    throw new NotSupportedException($"Геометрия {geometry.Type} не поддерживается.");
            }

            return mapObjects;
        }

        private List<MapObject> HandlePointGeometry(Feature feature)
        {
            List<MapObject> mapObjects = new List<MapObject>();
            var point = (GeoJSON.Net.Geometry.Point)feature.Geometry;

            // Извлечение координат
            var coordinates = point.Coordinates as Position;
            if (coordinates == null)
                throw new InvalidOperationException("Некорректные координаты точки.");

            double x = coordinates.Longitude;
            double y = coordinates.Latitude;

            MapPoint mapPoint = new MapPoint(x, y);

            // ПРоверка на текст
            if (feature.Properties.TryGetValue("text", out object textObj))
            {
                MapText mapText = new MapText(mapPoint, textObj.ToString());
                mapObjects.Add(mapText);
            }
            else
            {
                mapObjects.Add(mapPoint);
            }

            return mapObjects;
        }

        private List<MapObject> HandleLineStringGeometry(LineString lineString)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            var coordinates = lineString.Coordinates.Select(c => new MapPoint(c.Longitude, c.Latitude)).ToList();

            if (coordinates.Count >= 2)
            {
                if (coordinates.Count == 2)
                {
                    mapObjects.Add(new MapLine(coordinates[0], coordinates[1]));
                }
                else
                {
                    mapObjects.Add(new MapMultiLine(coordinates));
                }
            }

            return mapObjects;
        }

        private List<MapObject> HandleMultiLineStringGeometry(MultiLineString multiLineString)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            foreach (var lineString in multiLineString.Coordinates)
            {
                var polygonObjects = HandleLineStringGeometry(lineString);
                mapObjects.AddRange(polygonObjects);
            }

            return mapObjects;
        }

        private List<MapObject> HandlePolygonGeometry(Polygon polygon)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            var exteriorRing = polygon.Coordinates.First().Coordinates;
            var points = exteriorRing.Select(c => new MapPoint(c.Longitude, c.Latitude)).ToList();

            if (points.Count >= 3)
            {
                mapObjects.Add(new MapPolygon(points));
            }

            return mapObjects;
        }

        private List<MapObject> HandleMultiPolygonGeometry(MultiPolygon multiPolygon)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            foreach (var polygon in multiPolygon.Coordinates)
            {
                var polygonObjects = HandlePolygonGeometry(polygon);
                mapObjects.AddRange(polygonObjects);
            }

            return mapObjects;
        }
    }
}
