using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public void AddRankInDB(Rank rank)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into Rank (Name) values('" + rank.Name + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Gradul a fost inserat!");
                }
                catch(SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                }
               
            }
        }
        public void UpdateRankInDB(Rank newRank, Rank oldRank)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update Rank set Name = '" + newRank.Name + "' where Name='" + oldRank.Name  + "' ;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Gradul a fost modificat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        public void DeleteRankFromDB(Rank rank)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from Rank where Name='" + rank.Name + "';";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Gradul a fost sters!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }
    }
}
