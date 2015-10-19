using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using MolluskRecognition.DAL.DataModels;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MolluskRecognition.DAL.Migrations;

namespace MolluskRecognition.DAL.DataModels
{
    public class MolluskRecognitionContext : DbContext
    { 
        internal MolluskRecognitionContext() : this("MolluskRecognitionDB") { }

        internal MolluskRecognitionContext(string connectionStringName) : base(connectionStringName)
        {
            Database.SetInitializer(new MolluskRecognitionDBInitializer());
        }

        public virtual DbSet<MolluskCollection> Collections { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Genus> Genuses { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Species> Specieses { get; set; }
        public virtual DbSet<SculptureType> SculptureTypes { get; set; }
        public virtual DbSet<ShellType> ShellTypes { get; set; }
    }
}
