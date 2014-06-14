using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenBarter.Models
{
    public class ForTradeStatusRepo : IRepo<ForTradeStatus>
    {
        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.ForTradeStatuses.Find(id);

            return record != null;
        }


        public int AddRecord(ForTradeStatus record)
        {
            validate(record);

            var db = new Entities();
            db.ForTradeStatuses.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(ForTradeStatus record)
        {
            if (record == null)
                throw new ArgumentNullException();

            if (record.Name == "")
                throw new ApplicationException("Name is not defined.");


        }

        public void UpdateRecord(ForTradeStatus record)
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
            var record = db.ForTradeStatuses.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.ForTradeStatuses.Remove(record);
            db.SaveChanges();
        }

        public ForTradeStatus GetRecord(int id)
        {
            var db = new Entities();
            var record = db.ForTradeStatuses.Find(id);
            return record;
        }

        public List<ForTradeStatus> GetAll()
        {
            var db = new Entities();
            return db.ForTradeStatuses.ToList();
        }
    }
}