using EnscryptDb.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnscryptDb.Helper
{
    public class MapList<T>
    {
        public List<T> MapToList(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<T> items = new List<T>();
                while (record.Read())
                {
                    items.Add(Map<T>(record));
                }
                return items;
            }
        }



        public T Map<T>(IDataRecord record)
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);
            }
            return objT;
        }
    }
}
