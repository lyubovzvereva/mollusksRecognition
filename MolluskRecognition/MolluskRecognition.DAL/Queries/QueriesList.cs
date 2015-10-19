using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Queries
{
    public class QueryTags
    {
        public const string FamilyBase = "FamilyBase";
        public const string FeatureBase = "FeatureBase";
        public const string GenusBase = "GenusBase";
        public const string LocationBase = "LocationBase";
        public const string MolluskCollectionBase = "MolluskCollectionBase";
        public const string SampleBase = "SampleBase";
        public const string SectionBase = "SectionBase";
        public const string SpeciesBase = "SpeciesBase";
    }

    public interface IQueriesList
    {
        IEnumerable<IQueryWrapper> Queries { get; }
    }

    [Export(typeof(IQueriesList))]
    public class QueriesList : IQueriesList
    {
        public IEnumerable<IQueryWrapper> Queries
        {
            get
            {
                yield return new QueryWrapper<Family>(QueryTags.FamilyBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.FeatureBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.GenusBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.LocationBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.MolluskCollectionBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.SampleBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.SectionBase, (context) => context.Families);
                yield return new QueryWrapper<Family>(QueryTags.SpeciesBase, (context) => context.Families);
            }
        }
    }
}
