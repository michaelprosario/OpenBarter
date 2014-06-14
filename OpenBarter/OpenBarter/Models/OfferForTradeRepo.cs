using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{
    public class OfferForTradeRepo : IRepo<OfferForTrade>
    {

        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.OfferForTrades.Find(id);

            return record != null;
        }


        public int AddRecord(OfferForTrade record)
        {
            validate(record);

            var db = new Entities();
            db.OfferForTrades.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(OfferForTrade record)
        {
            if (record == null)
                throw new ArgumentNullException();

            new OfferForTradeValidator().ValidateAndThrow(record);

        }

        public void UpdateRecord(OfferForTrade record)
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
            var record = db.OfferForTrades.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.OfferForTrades.Remove(record);
            db.SaveChanges();
        }

        public OfferForTrade GetRecord(int id)
        {
            var db = new Entities();
            var record = db.OfferForTrades.Find(id);
            return record;
        }

        public List<OfferForTrade> GetAll()
        {
            var db = new Entities();
            return db.OfferForTrades.ToList();
        }

        public List<OfferForTrade> GetOffers(int recordID)
        {
            var db = new Entities();
            var list = db.OfferForTrades.Where(r => r.ForTradeID == recordID).ToList();

            return list;
        }


    }



}