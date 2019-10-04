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
        public UserList(List<User> users)
        {
            Users = users;
        }
        public UserList()
        {
            Users = new List<User>();
        }

        public List<User> Users { get; set; }

        public void GetUsersFromDB()
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "select * from user";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                SQLiteDataReader reader = command.ExecuteReader();                
                Users.Clear();
                while (reader.Read())
                {
                    Users.Add(new User((string)reader["Name"], (string)reader["Rank"]));
                }
            }
        }
        public void AddUserInDB(User user)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "insert into user (Name,Rank) values('" + user.Name + "','" + user.Rank + "');";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua adaugarea!");
                else
                    MessageBox.Show("Utilizatorul a fost inserat!");
            }
        }
        public void UpdateUserInDB(User newUser, User oldUser)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "update User set Name = '" + newUser.Name + "', Rank = '" +
                    newUser.Rank + "' where Name='" + oldUser.Name + "' and Rank = '" + oldUser.Rank + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua modificarea!");
                else
                    MessageBox.Show("Utilizatorul a fost modificat!");
            }
        }
        public void DeleteUserFromDB(User user)
        {
            using (SQLConnectionManager manager = new SQLConnectionManager())
            {
                manager.Open();
                string sql = "delete from user where Name='" + user.Name + "' and Rank = '" + user.Rank + "'";
                SQLiteCommand command = new SQLiteCommand(sql, manager.DbConnection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    MessageBox.Show("Nu s-a putut efectua stergerea!");
                else
                    MessageBox.Show("Utilizatorul a fost sters!");
            }
        }

        //insert

        //update

        //delete
    }
}
