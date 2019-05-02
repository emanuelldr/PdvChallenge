
using GeoAPI.Geometries;
using PdvChallenge.API.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PdvChallenge.API.Infrastructure.Repositories
{
    public interface IPdvRepository
    {
        Task Add(Pdv pdv);
        Task<Pdv> Get(int id);
        Task<Pdv> Get(IPoint point);
        Task<List<Pdv>> List();
    }
}
