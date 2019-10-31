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
                string sql = "select type.Id as \"Id\", type.Name, TypeDescription.Id as \"TypeDescriptionId\", TypeDescription.Name as \"TypeDescription\",Holiday.Id  as \"HolidayId\", Holiday.Name as \"Holiday\" " +
                             " from type, TypeDescription, Holiday " +
                             " where type.TypeDescriptionId = TypeDescription.Id and type.HolidayId = Holiday.Id; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Types.Clear();
                while (reader.Read())
                {
                    Types.Add(new ClockingType((Int64)reader["Id"], (string)reader["Name"],
                        new TypeDescription((Int64)reader["TypeDescriptionId"],(string)reader["TypeDescription"]),
                        new Holiday((Int64)reader["HolidayId"],(string)reader["Holiday"])));
                }
            }
        }

        public void AddTypeInDB(ClockingType type)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into type(Name,TypeDescriptionId,HolidayId) values('" + type.Type + "','" + type.TypeDescription.Id + "','" + type.Holiday.Id + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
               
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Pontajul a fost adaugat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                }
            }
        }
        public void UpdateTypeInDB(ClockingType newType, ClockingType oldType)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update type set Name = '" + newType.Type + "', TypeDescriptionId= '" + newType.TypeDescription.Id+ "', HolidayId= '"+ newType.Holiday.Id +"' "+
                    " where Name='" + oldType.Type + "' AND TypeDescriptionId= '" + oldType.TypeDescription.Id + "' AND HolidayId= '" + oldType.Holiday.Id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Pontajul a fost modificat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        public void DeleteTypeFromDB(ClockingType type)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from type where Id='" + type.TypeId + "';";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Pontajul a fost sters!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }
    }
}
