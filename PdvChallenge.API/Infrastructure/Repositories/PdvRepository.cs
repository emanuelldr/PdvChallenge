
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using PdvChallenge.API.Infrastructure.DataContext;
using PdvChallenge.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdvChallenge.API.Infrastructure.Repositories
{

    public class PdvRepository : IPdvRepository
    {

        private readonly PdvContext _context;

        public PdvRepository(PdvContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Pdv pdv)
        {
            if (pdv == null)
                throw new ArgumentNullException();

            if (!pdv.IsValid)
                throw new ArgumentException();

            await _context.Pdvs.AddAsync(pdv);
            _context.SaveChanges();
        }

        public async Task<Pdv> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            return await _context.Pdvs.FindAsync(id);
        }

        public async Task<Pdv> Get(IPoint point)
        {
            if (point == null)
                throw new ArgumentNullException();

            if (!point.IsValid)
                throw new ArgumentException();

            var pdvs = await _context.Pdvs.ToListAsync();

            var nearestPdv = pdvs
                .Where(pdv => pdv.coverageArea.Contains(point) || pdv.coverageArea.Touches(point))
                .OrderBy(pdv => pdv.address.Distance(point))
                .FirstOrDefault();

            return  nearestPdv;
        }

        public async Task<List<Pdv>> List()
        {
            return await _context.Pdvs.ToListAsync();
        }
    }
}
