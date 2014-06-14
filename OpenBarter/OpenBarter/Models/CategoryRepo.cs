using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenBarter.Models
{
    public class CategoryRepo : IRepo<Category>
    {

        public bool RecordExists(int id)
        {
            var db = new Entities();
            var record = db.Categories.Find(id);

            return record != null;
        }


        public int AddRecord(Category record)
        {
            validate(record);

            var db = new Entities();
            db.Categories.Add(record);
            db.SaveChanges();

            return record.Id;
        }

        public void validate(Category record)
        {
            if (record == null)
                throw new ArgumentNullException();

            if (record.Name == "")
                throw new ApplicationException("Name is not defined.");

           
        }

        public void UpdateRecord(Category record)
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
            var record = db.Categories.Find(id);
            if (record == null)
                throw new ApplicationException("Record does not exist: " + id);

            db.Categories.Remove(record);
            db.SaveChanges();
        }

        public Category GetRecord(int id)
        {
            var db = new Entities();
            var record = db.Categories.Find(id);
            return record;
        }

        public List<Category> GetAll()
        {
            var db = new Entities();
            return db.Categories.ToList();
        }

        public Dictionary<int, string> GetCategoriesDictionary()
        {
            var list = GetAll();
            Dictionary<int, string> d = new Dictionary<int, string>();
            foreach (var category in list)
            {
                d[category.Id] = category.Name;
            }

            return d;
        }
    }



}