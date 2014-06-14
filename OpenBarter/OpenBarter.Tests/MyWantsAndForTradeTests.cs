using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenBarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBarter.Tests
{
    [TestClass]
    public class MyWantsAndForTradeTests
    {
        [TestMethod]
        public void WantsRepo__GetRecordsByUserID__Success()
        {
            WantRepo wants = new WantRepo();

            Want aWant = new Want();
            aWant.MarketID = 1;
            aWant.Name = "test";
            aWant.OwnerID = 1;
            aWant.Status = "open";
            aWant.Tags = "";
            aWant.UpdatedBy = "mrosario";
            aWant.CreatedBy = "mrosario";
            aWant.CreatedAt = DateTime.Now;
            aWant.UpdatedAt = DateTime.Now;
            aWant.CategoryID = 29;
            aWant.Description = "foo";

            wants.AddRecord(aWant);

            int userID = 1;

            List<Want> list = wants.GetRecordsByUserID(userID);

            Assert.IsTrue(list.Count > 0);

        }
    }
}
