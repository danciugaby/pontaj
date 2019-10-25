using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public void AddUnitInDB(Unit unit)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into Unit (Name) values('" + unit.Name + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Unitatea a fost adaugata!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                }

            }
        }
        public void UpdateUnitInDB(Unit newUnit,Unit oldUnit)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update Unit set Name = '" + newUnit.Name + "' where Name='" + oldUnit.Name + "' ;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Unitatea a fost modificata!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        public void DeleteUnitFromDB(Unit unit)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from Unit where Name='" + unit.Name + "';";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Unitatea a fost stearsa!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }
    }
}
