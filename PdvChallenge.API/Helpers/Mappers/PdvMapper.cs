using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetTopologySuite.IO.Converters;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Contrib.Wkb;
using PdvChallenge.API.Model;
using System.Linq;

namespace PdvChallenge.API.Helpers.Mappers
{
    internal class PdvMapper
    {
        public static Dto.PdvDto MapToDto(Pdv pdv)
        {
            if (pdv == null)
                return null;

            return new Dto.PdvDto
            {
                id = pdv.id,
                tradingName = pdv.tradingName,
                ownerName = pdv.ownerName,
                document = pdv.document,
                coverageArea = GeoMapper.ToGeoJsonMultiPolygon(pdv.coverageArea),              
                address = GeoMapper.ToGeoJsoPoint(pdv.address)
            };
        }

        public static List<Dto.PdvDto> MapToDto(List<Pdv> pdvs)
        {
            return pdvs.Select(p => MapToDto(p)).ToList();
        }

        public static Pdv MapToModel(Dto.PdvDto pdv)
        {
            if (pdv == null)
                return null;

            return new Pdv
            {
                id = pdv.id,
                tradingName = pdv.tradingName,
                ownerName = pdv.ownerName,
                document = pdv.document,
                coverageArea = GeoMapper.ToNTSMultiPolygon(pdv.coverageArea),
                address = GeoMapper.ToNTSPoint(pdv.address)
            };

        }

    }
}

