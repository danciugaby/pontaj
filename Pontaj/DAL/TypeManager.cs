using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class TypeManager
    {
        public List<ClockingType> Types { get; set; }

        public TypeManager(List<ClockingType> types)
        {
            Types = types;
        }
        public TypeManager()
        {
            Types = new List<ClockingType>();
        }

        public void GetTypesFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from type";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Types.Clear();
                while (reader.Read())
                {
                    Types.Add(new ClockingType((string)reader["Type"]));
                }
            }
        }

        public void AddTypeInDB(ClockingType type)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into type(type) values('" + type.Type + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                else
                    MessageBox.Show("Tipul de pontaj a fost inserat!");
            }
        }
        public void UpdateTypeInDB(ClockingType newType, ClockingType oldType)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update type set Type = '" + newType.Type + "'" +
                    " where Type='" + oldType.Type +"'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                else
                    MessageBox.Show("Tipul de pontaj a fost modificat!");
            }
        }
        public void DeleteTypeFromDB(ClockingType type)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from type where Type='" + type.Type + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                else
                    MessageBox.Show("Tipul de pontaj a fost sters!");
            }
        }
    }
}
