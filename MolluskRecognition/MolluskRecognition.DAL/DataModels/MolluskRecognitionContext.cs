using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using MolluskRecognition.DAL.DataModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolluskRecognition.DAL.DataModels
{
    public class MolluskRecognitionContext : DbContext
    {
        internal MolluskRecognitionContext() : base("MolluskRecognitionDB") { }

        internal MolluskRecognitionContext(string connectionStringName) : base(connectionStringName) { }

        public DbSet<MolluskCollection> Collections { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Genus> Genuses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Species> Specieses { get; set; }
    }
}
