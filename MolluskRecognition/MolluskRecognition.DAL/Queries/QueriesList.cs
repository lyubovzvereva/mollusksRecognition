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
        public const string ShellTypeBase = "ShellTypeBase";
        public const string SculptureTypeBase = "SculptureTypeBase";
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
                yield return new QueryWrapper<Feature>(QueryTags.FeatureBase, (context) => context.Features);
                yield return new QueryWrapper<Genus>(QueryTags.GenusBase, (context) => context.Genuses);
                yield return new QueryWrapper<Location>(QueryTags.LocationBase, (context) => context.Locations);
                yield return new QueryWrapper<MolluskCollection>(QueryTags.MolluskCollectionBase, (context) => context.Collections);
                yield return new QueryWrapper<Sample>(QueryTags.SampleBase, (context) => context.Samples);
                yield return new QueryWrapper<Section>(QueryTags.SectionBase, (context) => context.Sections);
                yield return new QueryWrapper<Species>(QueryTags.SpeciesBase, (context) => context.Specieses);
                yield return new QueryWrapper<ShellType>(QueryTags.ShellTypeBase, (context) => context.ShellTypes);
                yield return new QueryWrapper<SculptureType>(QueryTags.SculptureTypeBase, (context) => context.SculptureTypes);
            }
        }
    }
}
