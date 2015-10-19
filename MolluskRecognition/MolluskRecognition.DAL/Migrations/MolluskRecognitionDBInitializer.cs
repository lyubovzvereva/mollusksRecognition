using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Migrations
{
    public class MolluskRecognitionDBInitializer : CreateDatabaseIfNotExists<MolluskRecognitionContext>
    {
        protected override void Seed(MolluskRecognitionContext context)
        {
            base.Seed(context);
            InitValues(context);
        }

        internal static void InitValues(MolluskRecognitionContext context)
        {
            var location = new Location(new Guid("349F73C0-9921-4D41-9BA5-C2F0663BF530"), "IMG_6686.jpg");
            context.Locations.AddOrUpdate(l => l.Id, location);

            var species = new Species
            {
                Id = new Guid("C514E517-6057-4B42-A37D-C2BCB6DD677F"),
                Name = "some species name",
                Age = "some age",
                Author = "some author",
                Year = DateTime.Today.AddYears(-10),
                Locations = new List<Location>
                {
                    location
                }
            };
            context.Specieses.AddOrUpdate(s => s.Id, species);

            context.Genuses.AddOrUpdate(g => g.Id,
                new Genus
                {
                    Id = new Guid("2C379444-103E-4C97-8842-507434AD4E2B"),
                    Author = "author name",
                    Name = "some genus name",
                    Year = DateTime.Today,
                    Species = new List<Species>() {species}
                });

            context.ShellTypes.AddOrUpdate(g => g.Id,
                new ShellType
                {
                    Id = new Guid("D21FA1CD-FE13-4746-AE1C-86A2004ABDB2"),
                    Name = "A1"
                }, new ShellType
                {
                    Id = new Guid("D14F0B23-C16C-4DFD-991B-050F8EAA9003"),
                    Name = "A2"
                }, new ShellType
                {
                    Id = new Guid("8CC2CB9B-C55A-4E71-8E28-C49CCDD477C0"),
                    Name = "B1"
                }, new ShellType
                {
                    Id = new Guid("BC223CDE-5278-4445-952B-44FDF34CA027"),
                    Name = "B2"
                }, new ShellType
                {
                    Id = new Guid("7323D18C-395C-44AF-8F83-F906B47A30BE"),
                    Name = "Г1"
                }, new ShellType
                {
                    Id = new Guid("1D906FC6-41AC-4DAD-BE3E-F2E7A30BA777"),
                    Name = "Г2"
                }, new ShellType
                {
                    Id = new Guid("29C233DA-E5E9-40AD-81B1-FA609AAFEDBB"),
                    Name = "Г3"
                });

            context.SculptureTypes.AddOrUpdate(g => g.Id,
                new SculptureType
                {
                    Id = new Guid("12BFB60D-0945-4D13-A7E8-7EF25AB20479"),
                    Name = "Radial",
                    DisplayName = "Radial"
                },
                new SculptureType
                {
                    Id = new Guid("12BFB60D-0945-4D13-A7E8-7EF25AB20479"),
                    Name = "Concentric",
                    DisplayName = "Concentric"
                },
                new SculptureType
                {
                    Id = new Guid("12BFB60D-0945-4D13-A7E8-7EF25AB20479"),
                    Name = "RadialOrConcentric",
                    DisplayName = "RadialOrConcentric"
                }
                );

            context.SaveChanges();
        }
    }
}
