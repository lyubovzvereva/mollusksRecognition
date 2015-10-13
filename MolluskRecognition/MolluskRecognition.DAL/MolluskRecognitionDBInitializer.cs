using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL
{
    public class MolluskRecognitionDBInitializer : CreateDatabaseIfNotExists<MolluskRecognitionContext>
    {
        protected override void Seed(MolluskRecognitionContext context)
        {
            base.Seed(context);
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

            context.SaveChanges();
        }
    }
}
