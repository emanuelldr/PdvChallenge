using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GJson = GeoJSON.Net.Geometry;

namespace PdvChallenge.API.Dto
{
    public class PdvDto
    {
        public int id { get; set; }
        public string tradingName { get; set; }
        public string ownerName { get; set; }
        public string document { get; set; }
        public GJson.MultiPolygon coverageArea { get; set; }
        public GJson.Point address { get; set; }

    }
}
