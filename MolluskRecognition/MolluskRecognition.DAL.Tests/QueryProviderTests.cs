using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using MolluskRecognition.DAL.DataModels;
using MolluskRecognition.DAL.Queries;
using Moq;
using NUnit.Framework;

namespace MolluskRecognition.DAL.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class QueryProviderTests
    {
        public class QueryProviderTestCase
        {
            public IQueryable<Family> Families;
            public IQueryable<Genus> Genuses;
            public IQueryable<Location> Locations;
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }

        private IEnumerable<QueryProviderTestCase> oneTableTestCases
        {
            get {
                yield return new QueryProviderTestCase
                {
                    Families = new Family[0].AsQueryable(),
                    Name = "Empty table"
                };
                yield return new QueryProviderTestCase
                {
                    Families = new[] { new Family { Id = Guid.NewGuid(), Name = "test1"} }.AsQueryable(),
                    Name = "One entity"
                };
                yield return new QueryProviderTestCase
                {
                    Families = new[] { new Family { Id = Guid.NewGuid(), Name = "test1" }, new Family { Id = Guid.NewGuid(), Name = "test2" } }.AsQueryable(),
                    Name = "List of entities"
                };
            }
        }

        [TestCaseSource(nameof(oneTableTestCases))]
        public void GetBaseQuery_ShouldReturnCorrectTableFromOne(QueryProviderTestCase testCase)
        {
            var contextStub = new Mock<IMolluskRecognitionContext>();
            var setStub = new Mock<DbSet<Family>>();
            setStub.As<IQueryable<Family>>().Setup(m => m.Provider).Returns(testCase.Families.Provider);
            setStub.As<IQueryable<Family>>().Setup(m => m.Expression).Returns(testCase.Families.Expression);
            setStub.As<IQueryable<Family>>().Setup(m => m.ElementType).Returns(testCase.Families.ElementType);
            setStub.As<IQueryable<Family>>().Setup(m => m.GetEnumerator()).Returns(testCase.Families.GetEnumerator());
            contextStub.Setup(s => s.Families).Returns(() => setStub.Object);

            var queriesListStub = new Mock<IQueriesList>();
            queriesListStub.Setup(l => l.Queries)
                .Returns(() => new[] {new QueryWrapper<Family>(QueryTags.FamilyBase, (context) => context.Families)});

            var queryProvider = new DBQueryProvider(contextStub.Object, queriesListStub.Object);

            var query = queryProvider.GetBaseQuery<Family>();

            query.Should().BeEquivalentTo(testCase.Families, "because we provided such list as DbSet in context");
        }

        private IEnumerable<QueryProviderTestCase> threeTablesTestCases
        {
            get
            {
                yield return new QueryProviderTestCase
                {
                    Families = new Family[0].AsQueryable(),
                    Genuses = new Genus[0].AsQueryable(),
                    Locations = new Location[0].AsQueryable(),
                    Name = "Empty tables"
                };
                yield return new QueryProviderTestCase
                {
                    Families = new[] { new Family { Id = Guid.NewGuid(), Name = "test1" } }.AsQueryable(),
                    Genuses = new Genus[0].AsQueryable(),
                    Locations = new Location[0].AsQueryable(),
                    Name = "One entity in one table"
                };
                yield return new QueryProviderTestCase
                {
                    Families = new[] { new Family { Id = Guid.NewGuid(), Name = "test1" } }.AsQueryable(),
                    Genuses = new[] { new Genus { Id = Guid.NewGuid(), Name = "test1" } }.AsQueryable(),
                    Locations = new[] { new Location { Id = Guid.NewGuid(), FileName = "test1" } }.AsQueryable(),
                    Name = "One entity in each table"
                };
            }
        }

        [TestCaseSource(nameof(threeTablesTestCases))]
        public void GetBaseQuery_ShouldReturnCorrectTableFromThree(QueryProviderTestCase testCase)
        {
            var contextStub = new Mock<IMolluskRecognitionContext>();
            var familySetStub = new Mock<DbSet<Family>>();
            familySetStub.As<IQueryable<Family>>().Setup(m => m.Provider).Returns(testCase.Families.Provider);
            familySetStub.As<IQueryable<Family>>().Setup(m => m.Expression).Returns(testCase.Families.Expression);
            familySetStub.As<IQueryable<Family>>().Setup(m => m.ElementType).Returns(testCase.Families.ElementType);
            familySetStub.As<IQueryable<Family>>().Setup(m => m.GetEnumerator()).Returns(testCase.Families.GetEnumerator());
            contextStub.Setup(s => s.Families).Returns(() => familySetStub.Object);

            var genusSetStub = new Mock<DbSet<Genus>>();
            genusSetStub.As<IQueryable<Genus>>().Setup(m => m.Provider).Returns(testCase.Genuses.Provider);
            genusSetStub.As<IQueryable<Genus>>().Setup(m => m.Expression).Returns(testCase.Genuses.Expression);
            genusSetStub.As<IQueryable<Genus>>().Setup(m => m.ElementType).Returns(testCase.Genuses.ElementType);
            genusSetStub.As<IQueryable<Genus>>().Setup(m => m.GetEnumerator()).Returns(testCase.Genuses.GetEnumerator());
            contextStub.Setup(s => s.Genuses).Returns(() => genusSetStub.Object);

            var locationSetStub = new Mock<DbSet<Location>>();
            locationSetStub.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(testCase.Locations.Provider);
            locationSetStub.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(testCase.Locations.Expression);
            locationSetStub.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(testCase.Locations.ElementType);
            locationSetStub.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(testCase.Locations.GetEnumerator());
            contextStub.Setup(s => s.Locations).Returns(() => locationSetStub.Object);

            var queriesListStub = new Mock<IQueriesList>();
            queriesListStub.Setup(l => l.Queries)
                .Returns(() => new IQueryWrapper[]
                {
                    new QueryWrapper<Family>(QueryTags.FamilyBase, (context) => context.Families),
                    new QueryWrapper<Genus>(QueryTags.GenusBase, (context) => context.Genuses),
                    new QueryWrapper<Location>(QueryTags.LocationBase, (context) => context.Locations),
                });

            var queryProvider = new DBQueryProvider(contextStub.Object, queriesListStub.Object);

            var familyQuery = queryProvider.GetBaseQuery<Family>();
            var genusQuery = queryProvider.GetBaseQuery<Genus>();
            var locationQuery = queryProvider.GetBaseQuery<Location>();

            familyQuery.Should().BeEquivalentTo(testCase.Families, "because we provided such list as DbSet in context");
            genusQuery.Should().BeEquivalentTo(testCase.Genuses, "because we provided such list as DbSet in context");
            locationQuery.Should().BeEquivalentTo(testCase.Locations, "because we provided such list as DbSet in context");
        }
        
        [Test]
        public void Negative_GetBaseQuery_ShouldThrowErrorIfNotFound()
        {
            var contextStub = new Mock<IMolluskRecognitionContext>();
            var queriesListStub = new Mock<IQueriesList>();
            queriesListStub.Setup(l => l.Queries)
                .Returns(() => new List<IQueryWrapper>());

            var queryProvider = new DBQueryProvider(contextStub.Object, queriesListStub.Object);

            try
            {
                queryProvider.GetBaseQuery<Family>();
                Assert.Fail("Didn't throw any exception");
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ArgumentException>("because we should throw ArgumentException");
            }
        }
    }
}
