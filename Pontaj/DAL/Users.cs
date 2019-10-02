using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //insert

        //update

        //delete
    }
}
