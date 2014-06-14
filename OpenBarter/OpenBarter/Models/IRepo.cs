using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBarter.Models
{
    interface IRepo<T>
    {
        int AddRecord(T record);
        void UpdateRecord(T record);
        void DeleteRecord(int id);
        bool RecordExists(int id);
        T GetRecord(int id);
        void validate(T record);
        List<T> GetAll();
    }
}
