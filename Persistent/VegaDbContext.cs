using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistent
{
    public class VegaDbContext: DbContext
    {
        public DbSet<Make> MakeSet { get; set; }
        public DbSet<Model> ModelSet { get; set; }
        public DbSet<Feature> FeatureSet { get; set; }
        public DbSet<Vehicle> VehicleSet { get; set; }
        public DbSet<VehicleFeature> VehicleFeatureSet{ get; set; }

        // By using this, we will decouple from context
        // Instead of create a new instance of context in every controller
        // We pass the context into the controller constructor
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.FeatureId, vf.VehicleId});
        }
    }
}
