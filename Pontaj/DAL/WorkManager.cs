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

        public void GetWorksFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select U.Id as \"UserId\", U.FirstName, U.LastName, R.Id as \"RankId\",R.Name as \"Rank\", " +
                    " Un.Id as \"UnitId\", Un.Name as \"Unit\", TD.Id as \"TypeId\", TD.Name as \"Type\",h.Id as \"HolidayId\", h.Name as \"Holiday\", " +
                    " w.StartDate, w.EndDate " +
                    " from Work w " +
                    " LEFT JOIN Holiday H on w.HolidayId = H.Id " +
                    " LEFT JOIN TypeDescription TD on w.TypeDescriptionId = TD.Id " +
                    " LEFT JOIN User U on w.UserId = U.Id " +
                    " LEFT JOIN Rank R on U.RankId = R.Id " +
                    " LEFT JOIN Unit Un on U.UnitId = Un.Id; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Works.Clear();
                while (reader.Read())
                {
                    bool insertHoliday = true;
                    bool insertType = true;
                    try {
                        if ((string)reader["Holiday"] != null)
                            insertHoliday = true;
                    }
                    catch(System.InvalidCastException ex)
                    {
                        insertHoliday = false;
                    }
                    try
                    {
                        if ((string)reader["Type"] != null)
                            insertType = true;
                    }
                    catch (System.InvalidCastException ex)
                    {
                        insertType = false;
                    }
                    if (insertType&&insertHoliday)
                        Works.Add(new Work(new User((Int64)reader["UserId"], (string)reader["FirstName"], (string)reader["LastName"],
                            new Rank((Int64)reader["RankId"], (string)reader["Rank"]), new Unit((Int64)reader["UnitId"], (string)reader["Unit"])),
                            new TypeDescription((Int64)reader["TypeId"], (string)reader["Type"]), new Holiday((Int64)reader["HolidayId"], (string)reader["Holiday"]),
                            (DateTime)reader["StartDate"], (DateTime)reader["EndDate"]));
                    else if (insertType)
                        Works.Add(new Work(new User((Int64)reader["UserId"], (string)reader["FirstName"], (string)reader["LastName"],
                            new Rank((Int64)reader["RankId"], (string)reader["Rank"]), new Unit((Int64)reader["UnitId"], (string)reader["Unit"])),
                            new TypeDescription((Int64)reader["TypeId"], (string)reader["Type"]),
                            (DateTime)reader["StartDate"], (DateTime)reader["EndDate"]));
                    else if (insertHoliday)
                        Works.Add(new Work(new User((Int64)reader["UserId"], (string)reader["FirstName"], (string)reader["LastName"],
                            new Rank((Int64)reader["RankId"], (string)reader["Rank"]), new Unit((Int64)reader["UnitId"], (string)reader["Unit"])),
                            new Holiday((Int64)reader["HolidayId"], (string)reader["Holiday"]),
                            (DateTime)reader["StartDate"], (DateTime)reader["EndDate"]));
                    

                }
            }
        }
        //insert
        public void AddWorkInDB(Work work)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into work (StartDate, EndDate, UserId, TypeDescriptionId, HolidayId) values('" +
                    work.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + work.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                    work.User.UserId + "','" + work.Type.Id + "','" + work.Holiday.Id + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                else
                    MessageBox.Show("Pontajul a fost adaugat!");
            }
        }

        //update


    }
}
