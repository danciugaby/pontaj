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
                string sql = "select User.Id as \"UserId\", User.FirstName,User.LastName, Rank.Id as \"RankId\", Rank.Name as \"Rank\", Unit.Id as \"UnitId\",unit.Name as \"Unit\", "+
                    " Type.Name as \"Name\", TypeDescription.Name as \"Description\"," +
                    " Holiday.Name as \"Holiday\",StartDate, EndDate " +
                    "from Work, Type, TypeDescription, Holiday, User, Unit, Rank " +
                    "where Work.TypeId = Type.Id AND Type.TypeDescriptionId = TypeDescription.Id " +
                    "AND Type.HolidayId = Holiday.Id AND Work.UserId = User.Id " +
                    "AND User.RankId = Rank.Id AND User.UnitId = Unit.Id; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Works.Clear();
                while (reader.Read())
                {
                    Works.Add(new Work(new User((Int64)reader["UserId"],(string)reader["FirstName"], (string)reader["LastName"],
                        new Rank((Int64)reader["RankId"],(string)reader["Rank"]), new Unit((Int64)reader["UnitId"],(string)reader["Unit"])),
                        new ClockingType((string)reader["Name"],new TypeDescription((string)reader["Description"]),new Holiday((string)reader["Holiday"])),
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
                string sql = "insert into work (UserId,TypeId,StartDate,EndDate) values('" + work.User.UserId + "','" + work.Type.TypeId + "','" + work.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + work.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "');";
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
