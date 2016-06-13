using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deluxe.Model
{
    public class DeluxeSQLiteConnection : SQLiteConnection
    {
        public DeluxeSQLiteConnection(ISQLitePlatform sqlitePlatform, string databasePath, bool isCreateDataBase = false, bool storeDateTimeAsTicks = true, IBlobSerializer serializer = null, IDictionary<string, TableMapping> tableMappings = null, IDictionary<Type, string> extraTypeMappings = null, IContractResolver resolver = null) : base(sqlitePlatform, databasePath, storeDateTimeAsTicks, serializer, tableMappings, extraTypeMappings, resolver)
        {
            if (isCreateDataBase)
            {
                foreach (Type entity in Helper.GetAllEntities())
                {
                    CreateTable(entity);
                }
            }
        }

    }


}
