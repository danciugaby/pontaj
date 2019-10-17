using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class WorkManager
    {
        public List<Work> Works { get; set; }

        public WorkManager(List<Work> works)
        {
            Works = works;
        }
        public WorkManager()
        {
            Works = new List<Work>();
        }

        public void GetWorksFromDB() {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select user.Name,user.Rank,StartDate,EndDate,type.Type from Work " +
                              "left join Type type on Work.TypeId = type.TypeId " +
                               "left join User user on Work.UserId = user.UserId";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Works.Clear();
                while (reader.Read())
                {
                    Works.Add(new Work(new User((string)reader["Name"], (string)reader["Rank"]),
                        new ClockingType((string)reader["Type"]),(DateTime)reader["StartDate"], (DateTime)reader["EndDate"]));

                }
            }
        }
        //insert
        public void AddWorkInDB(Work work)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into work (UserId,TypeId,StartDate,EndDate) values('" + work.User.UserId + "','" + work.Type.TypeId + "','" + work.StartDate + "','" + work.EndDate + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                else
                    MessageBox.Show("Utilizatorul a fost inserat!");
            }
        }
        //update
        

    }
}
