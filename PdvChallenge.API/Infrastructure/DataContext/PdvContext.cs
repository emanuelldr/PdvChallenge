using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

using GeoAPI.Geometries;
using NetTopologySuite;
using Microsoft.Extensions.Configuration;
using PdvChallenge.API.Model;

namespace PdvChallenge.API.Infrastructure.DataContext
{
    public class PdvContext : DbContext
    {
        public PdvContext(DbContextOptions<PdvContext> options) : base(options) { }


        public virtual DbSet<Pdv> Pdvs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pdv>().ToTable("Pdv");
        }
       
    }
}
