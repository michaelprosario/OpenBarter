using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{
    public class OfferForWantRepo : IRepo<OfferForWant>
    {

        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.OfferForWants.Find(id);

            return record != null;
        }


        public int AddRecord(OfferForWant record)
        {
            validate(record);

            var db = new Entities();
            db.OfferForWants.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(OfferForWant record)
        {
            if (record == null)
                throw new ArgumentNullException();

            new OfferForWantValidator().ValidateAndThrow(record);

        }

        public void UpdateRecord(OfferForWant record)
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
            var record = db.OfferForWants.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.OfferForWants.Remove(record);
            db.SaveChanges();
        }

        public OfferForWant GetRecord(int id)
        {
            var db = new Entities();
            var record = db.OfferForWants.Find(id);
            return record;
        }

        public List<OfferForWant> GetAll()
        {
            var db = new Entities();
            return db.OfferForWants.ToList();
        }

        public List<OfferForWant> GetOffers(int recordID)
        {
            var db = new Entities();
            var list = db.OfferForWants.Where(r => r.WantID == recordID).ToList();

            return list;
        }


    }



}