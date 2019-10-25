using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitManager
    {
        public List<Unit> Units { get; set; }

        public UnitManager(List<Unit> units)
        {
            Units = units;
        }
        public UnitManager()
        {
            Units = new List<Unit>();
        }
        public void GetUnitsFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from Unit; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Units.Clear();
                while (reader.Read())
                {
                    Units.Add(new Unit((Int64)reader["Id"], (string)reader["Name"]));
                }
            }
        }
    }
}
