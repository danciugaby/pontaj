using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class HolidayManager
    {
        public List<Holiday> Holidays { get; set; }

        public HolidayManager(List<Holiday> holidays)
        {
            Holidays = holidays;
        }
        public HolidayManager()
        {
            Holidays = new List<Holiday>();
        }
        public void GetHolidaysFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from Holiday; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Holidays.Clear();
                while (reader.Read())
                {
                    Holidays.Add(new Holiday((Int64)reader["Id"], (string)reader["Name"]));
                }
            }
        }
        public void AddHolidayInDB(Holiday holiday)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into Holiday (Name) values('" + holiday.Name + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Concediul a fost inserat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                }

            }
        }
        public void UpdateHolidayInDB(Holiday newHoliday,Holiday oldHoliday)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update Holiday set Name = '" + newHoliday.Name + "' where Name='" + oldHoliday.Name + "' ;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Concediul a fost modificat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        public void DeleteHolidayFromDB(Holiday holiday)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from Holiday where Name='" + holiday.Name + "';";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Concediul a fost sters!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }
    }
}
