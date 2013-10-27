using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Data;
using MolluskRecognition.DataModels;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var adapter = new InMemoryAdapter();
            adapter.SetKeyColumn("Test", "Id");
            Database.UseMockAdapter(adapter);

            // Insert some test data
            var db = Database.Open();
            db.Test.Insert(Id: 1, Name: "Alice");

            // Act
            var record = db.Test.FindById(1);

            // Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(1, record.Id);
            Assert.AreEqual("Alice", record.Name);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);

            var db = Database.Open();
            db.Family.Insert(Id: 1, Name: "fam1");
            db.Family.Insert(Id: 2, Name: "fam2");

            var facade = new DbFacade();
            var families = facade.GetFamilies();

            Assert.AreEqual(families.Count, 2);
            foreach (var item in families)
            {
                if (item.Id == 1)
                {
                    Assert.AreEqual("fam1", item.Name);
                }
                else
                {
                    Assert.AreEqual("fam2", item.Name);
                }
            }
        }
    }
}
