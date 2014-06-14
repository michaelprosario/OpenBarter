using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{
    public class WantRepo : IRepo<Want>
    {
        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.Wants.Find(id);

            return record != null;
        }


        public int AddRecord(Want record)
        {
            validate(record);

            var db = new Entities();
            db.Wants.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(Want record)
        {
            if (record == null)
                throw new ArgumentNullException();

            WantValidator v = new WantValidator();
            v.ValidateAndThrow(record);

            if (!new CategoryRepo().RecordExists(record.CategoryID))
            {
                throw new ApplicationException("Category does not exist: " + record.CategoryID);
            }

            

        }

        public void UpdateRecord(Want record)
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
            var record = db.Wants.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.Wants.Remove(record);
            db.SaveChanges();
        }

        public Want GetRecord(int id)
        {
            var db = new Entities();
            var record = db.Wants.Find(id);
            return record;
        }

        public List<Want> GetAll()
        {
            var db = new Entities();
            return db.Wants.ToList();
        }


        public List<Want> GetRecordsByUserID(int userID)
        {
            var db = new Entities();

            return db.Wants.Where(x => x.OwnerID == userID).ToList<Want>();
        }

        internal List<Want> GetRecordsForMarket(int marketID)
        {
            var db = new Entities();
            var list = db.Wants.Where(x => x.MarketID == marketID && x.Status == "open").ToList<Want>();
            list.Reverse();

            return list;
        }
    }
}