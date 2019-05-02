using System;
using System.Collections.Generic;
using System.Text;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace PdvChallenge.API.Model
{
    public class Pdv
    {
        public int id { get; set; }
        public string tradingName { get; set; }
        public string ownerName { get; set; }
        public string document { get; set; }
        public MultiPolygon coverageArea { get; set; }
        public Point address { get; set; }

        public bool IsValid =>
            (   
                !string.IsNullOrWhiteSpace(tradingName)
                && !string.IsNullOrWhiteSpace(ownerName)
                && !string.IsNullOrWhiteSpace(document)
                && coverageArea.IsValid
                && address.IsValid
                && (id > 0)
            );

    }
}

