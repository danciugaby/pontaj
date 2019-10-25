using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RankManager
    {
        public List<Rank> Ranks { get; set; }

        public RankManager(List<Rank> ranks)
        {
            Ranks = ranks;
        }
        public RankManager()
        {
            Ranks = new List<Rank>();
        }
        public void GetRanksFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from Rank; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Ranks.Clear();
                while (reader.Read())
                {
                    Ranks.Add(new Rank((Int64)reader["Id"], (string)reader["Name"]));
                }
            }
        }
    }
}
