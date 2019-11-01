using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class UserList
    {
        public List<User> Users { get; set; }

        public UserList(List<User> users)
        {
            Users = users;
        }
        public UserList()
        {
            Users = new List<User>();
        }

        public void GetUsersFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select user.Id as \"UserId\",user.FirstName,user.LastName,rank.Id as \"RankId\",rank.Name as \"Rank\", unit.Id as \"UnitId\",unit.Name as \"Unit\""
                             + " from user, rank, unit"
                             + " where user.RankId == rank.Id and user.UnitId == unit.Id;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Users.Clear();
                while (reader.Read())
                {
                    Users.Add(new User((Int64)reader["UserId"], (string)reader["FirstName"], (string)reader["LastName"],
                        new Rank((Int64)reader["RankId"], (string)reader["Rank"]), new Unit((Int64)reader["UnitId"], (string)reader["Unit"])));


                }
            }
        }
        //insert
        public void AddUserInDB(User user)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into user (FirstName,LastName,RankId,UnitId) values('" + user.FirstName + "','" + user.LastName + "','" + user.Rank.Id + "','" + user.Unit.Id + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Utilizatorul a fost introdus!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua inserarea!");
                }
            }
        }
        //update
        public void UpdateUserInDB(User newUser, User oldUser)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update User set FirstName = '" + newUser.FirstName + "', LastName = '" +
                    newUser.LastName + "', RankId = '" + newUser.Rank.Id + "', UnitId = '" +
                    newUser.Unit.Id + "' where FirstName='" + oldUser.FirstName + "' and LastName = '"
                    + oldUser.LastName + "' and RankId = '" + oldUser.Rank.Id +
                    "' and UnitId = '" + oldUser.Unit.Id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Utilizatorul a fost modificat!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                }
            }
        }
        //delete
        public void DeleteUserFromDB(User user)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from user where user.Id='" + user.UserId + "' ;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                        MessageBox.Show("Utilizatorul a fost sters!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                }
            }
        }






    }
}
