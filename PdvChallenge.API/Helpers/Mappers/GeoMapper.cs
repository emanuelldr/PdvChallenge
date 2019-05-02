using GJSon = GeoJSON.Net.Geometry;
using NTS = NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdvChallenge.API.Model;
using GeoJSON.Net.Contrib.Wkb;
using GeoAPI.Geometries;

namespace PdvChallenge.API.Helpers.Mappers
{
    internal class GeoMapper
    {
        public static GJSon.MultiPolygon ToGeoJsonMultiPolygon(IGeometry geometry)
        {
            return geometry.AsBinary().ToGeoJSONObject<GJSon.MultiPolygon>();
        }

        public static GJSon.Point ToGeoJsoPoint(NTS.Point point)
        {
            return point.ToBinary().ToGeoJSONObject<GJSon.Point>();
        }

        public static NTS.Point ToNTSPoint(GJSon.Point point)
        {
            return new NTS.Point(new WKBReader().Read(point.ToWkb()).Coordinate);          
        }

        public static NTS.Point ToNTSPoint(double lng, double lat)
        {
            var point = new NTS.Point(lng, lat);

            return point;

        }

        public static NTS.MultiPolygon ToNTSMultiPolygon(GJSon.MultiPolygon multiPolygon)
        {
             var geometry = new WKBReader().Read(multiPolygon.ToWkb());

            return (NTS.MultiPolygon)geometry;
        }

    }
}
