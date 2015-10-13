using System.Collections.Generic;
using System.ComponentModel.Composition;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MolluskRecognitionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MolluskRecognitionContext context)
        {
        }
    }
}
