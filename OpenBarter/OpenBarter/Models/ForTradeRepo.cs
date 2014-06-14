using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{


    public class ForTradeRepo : IRepo<ForTrade>
    {

        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.ForTrades.Find(id);

            return record != null;
        }


        public int AddRecord(ForTrade record)
        {
            validate(record);

            var db = new Entities();
            db.ForTrades.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(ForTrade record)
        {
            if (record == null)
                throw new ArgumentNullException();

            ForTradeValidator v = new ForTradeValidator();
            v.ValidateAndThrow(record);

            if (!new CategoryRepo().RecordExists(record.CategoryID))
            {
                throw new ApplicationException("Category does not exist: " + record.CategoryID);
            }

            
           
        }

        public void UpdateRecord(ForTrade record)
        {
            validate(record);
            if (!RecordExists(record.Id))
                throw new ApplicationException("Record does not exist.");

            var db = new Entities();
            db.Entry(record).State = System.Data.EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteRecord(int id)
        {
            var db = new Entities();
            var record = db.ForTrades.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.ForTrades.Remove(record);
            db.SaveChanges();
        }

        public ForTrade GetRecord(int id)
        {
            var db = new Entities();
            var record = db.ForTrades.Find(id);
            return record;
        }

        public List<ForTrade> GetAll()
        {
            var db = new Entities();
            return db.ForTrades.ToList();
        }

        public List<ForTrade> GetRecordsByUserID(int userID)
        {
            var db = new Entities();

            return db.ForTrades.Where(x => x.OwnerID == userID).ToList<ForTrade>();
        }


        public List<ForTrade> GetRecordsForMarket(int marketID)
        {
            var db = new Entities();
            var list = db.ForTrades.Where(x => x.MarketID == marketID && x.Status == "open").ToList<ForTrade>();
            list.Reverse();

            return list;
        }
    }

}