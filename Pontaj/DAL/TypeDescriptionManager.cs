using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class TypeDescriptionManager
    {
        public List<TypeDescription> TypeDescriptions { get; set; }

        public TypeDescriptionManager(List<TypeDescription> typeDescriptions)
        {
            TypeDescriptions = typeDescriptions;
        }
        public TypeDescriptionManager()
        {
            TypeDescriptions = new List<TypeDescription>();
        }
        public void GetTypeDescriptionsFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from TypeDescription; ";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                TypeDescriptions.Clear();
                while (reader.Read())
                {
                    TypeDescriptions.Add(new TypeDescription((Int64)reader["Id"], (string)reader["Name"]));
                }
            }
        }
        public void AddTypeDescriptionInDB(TypeDescription typeDescription)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into TypeDescription (Name) values('" + typeDescription.Name + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Tipul a fost adaugat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                }

            }
        }
        public void UpdateTypeDescriptionInDB(TypeDescription newTypeDescription, TypeDescription oldTypeDescription)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update TypeDescription set Name = '" + newTypeDescription.Name + "' where Name='" + oldTypeDescription.Name + "' ;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Tipul a fost modificat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        public void DeleteTypeDescriptionFromDB(TypeDescription typeDescription)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from TypeDescription where Name='" + typeDescription.Name + "';";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Tipul a fost sters!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }
    }
}
