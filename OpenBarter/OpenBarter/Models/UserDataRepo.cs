using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{
    public class UserDataRepo : IRepo<UserData>
    {
        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.UserDatas.Find(id);

            return record != null;
        }


        public int AddRecord(UserData record)
        {
            validate(record);

            record.CreatedAt = DateTime.Now;
            record.UpdatedAt = DateTime.Now;

            var db = new Entities();
            db.UserDatas.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(UserData record)
        {
            
            record.UpdatedAt = DateTime.Now;

            if (record == null)
                throw new ArgumentNullException();

            UserDataValidator v = new UserDataValidator();
            v.ValidateAndThrow(record);
            
        }

        public void UpdateRecord(UserData record)
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
            var record = db.UserDatas.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.UserDatas.Remove(record);
            db.SaveChanges();
        }

        public UserData GetRecord(int id)
        {
            var db = new Entities();
            var record = db.UserDatas.Find(id);
            return record;
        }

        public List<UserData> GetAll()
        {
            var db = new Entities();
            return db.UserDatas.ToList();
        }


        public UserData GetUserDataByUserID(int currentUserID)
        {
            var db = new Entities();
            var userData = db.UserDatas.Where(r => r.UserProfileID == currentUserID).SingleOrDefault();
            return userData;
        }
    }
}