using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenBarter.Models
{
    public class WantStatusRepo : IRepo<WantStatus>
    {
        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.WantStatuses.Find(id);

            return record != null;
        }


        public int AddRecord(WantStatus record)
        {
            validate(record);

            var db = new Entities();
            db.WantStatuses.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(WantStatus record)
        {
            if (record == null)
                throw new ArgumentNullException();



        }

        public void UpdateRecord(WantStatus record)
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
            var record = db.WantStatuses.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.WantStatuses.Remove(record);
            db.SaveChanges();
        }

        public WantStatus GetRecord(int id)
        {
            var db = new Entities();
            var record = db.WantStatuses.Find(id);
            return record;
        }

        public List<WantStatus> GetAll()
        {
            var db = new Entities();
            return db.WantStatuses.ToList();
        }

    }
}