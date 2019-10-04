using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pontaj
{//new feature
    class Controller
    {
        public UserList users;

        public Controller()
        {
            users = new UserList();
        }

        public List<User> GetUsersFromDB()
        {
            users.GetUsersFromDB();
            return users.Users;
        }

        public bool checkNameField()
        {
            
            return false;
        }
        
        public void AddUserInDB(User user)
        {
            users.AddUserInDB(user);
        }

        public void UpdateUserInDB(User newUser, User oldUser)
        {
            users.UpdateUserInDB(newUser, oldUser);
        }
        public void DeleteUserFromDB(User user)
        {
            users.DeleteUserFromDB(user);
        }
    }
}
