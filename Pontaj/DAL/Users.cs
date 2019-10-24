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
                string sql = "select user.Id as \"UserId\",user.FirstName,user.LastName,rank.Name as \"Rank\",unit.Name as \"Unit\""
                             + " from user, rank, unit"
                             + " where user.RankId == rank.Id and user.UnitId == unit.Id;";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                Users.Clear();
                while (reader.Read())
                {
                    Users.Add(new User((Int64)reader["UserId"], (string)reader["FirstName"], (string)reader["LastName"], (string)reader["Rank"], (string)reader["Unit"]));
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
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                else
                    MessageBox.Show("Utilizatorul a fost inserat!");
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
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                else
                    MessageBox.Show("Utilizatorul a fost modificat!");
            }
        }
        //delete
        public void DeleteUserFromDB(User user)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from user where FirstName='" + user.FirstName + "' and LastName = '" + user.LastName + "' and RankId = '" + user.Rank.Id + "' and UnitId = '" + user.Unit.Id + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                else
                    MessageBox.Show("Utilizatorul a fost sters!");
            }
        }






    }
}
