using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pontaj
{//new feature
    class Controller
    {
        UserList users;

        public Controller()
        {
            users = new UserList();
        }

        public List<User> GetUsersFromDB()
        {
            users.GetUsersFromDB();
            return users.Users;
        }

    }
}
