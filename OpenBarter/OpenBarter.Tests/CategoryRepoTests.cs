using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenBarter.Models;

namespace OpenBarter.Tests
{
    [TestClass]
    public class CategoryRepoTests
    {
        [TestCleanup]
        public void CleanUp()
        {
            CategoryRepo t = new CategoryRepo();
            var list = t.GetAll();
            foreach (var r in list)
            {
                if (r.Name.StartsWith("test"))
                {
                    t.DeleteRecord(r.Id);
                }
            }
        }

        [TestMethod]
        public void CategoryRepo__AddRecord__ItShouldWork()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "test";

            //act ....
            t.AddRecord(r);

            //assert ...
            Assert.IsTrue(t.RecordExists(r.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CategoryRepo__AddRecord__FailWhenNameNull()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "";

            //act ....
            t.AddRecord(r);
        }


        [TestMethod]
        public void CategoryRepo__DeleteRecord__ItShouldWork()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "test";
            int recordID = t.AddRecord(r);
            Assert.IsTrue(t.RecordExists(r.Id));

            //act ....
            t.DeleteRecord(recordID);

            //assert ...
            Assert.IsTrue(!t.RecordExists(recordID));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CategoryRepo__DeleteRecord__FailWhenRecordNotExist()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();

            //act ....
            t.DeleteRecord(int.MaxValue);
        }



        [TestMethod]
        public void CategoryRepo__UpdateRecord__ItShouldWork()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "test";
            int recordID = t.AddRecord(r);
            Assert.IsTrue(t.RecordExists(r.Id));

            //act ....
            var r2 = t.GetRecord(recordID);
            r2.Name = "test2";
            t.UpdateRecord(r2);

            //assert ...
            var r3 = t.GetRecord(recordID);
            Assert.IsTrue(r3.Name == r2.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CategoryRepo__UpdateRecord__FailWhenRecordNotAdded()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "test";

            //act ....
            t.UpdateRecord(r);
        }




        [TestMethod]
        public void CategoryRepo__GetAll__ItShouldWork()
        {
            //arrange ....
            CategoryRepo t = new CategoryRepo();
            Category r = new Category();
            r.Name = "test";
            int recordID = t.AddRecord(r);
            Assert.IsTrue(t.RecordExists(r.Id));

            //act ....
            var list = t.GetAll();

            //assert ...
            Assert.IsTrue(list.Count > 0);
        }






    }
}
