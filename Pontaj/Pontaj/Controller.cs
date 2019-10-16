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
        public TypeManager types;
        public WorkManager works;

        public Controller()
        {
            users = new UserList();
            types = new TypeManager();
            works = new WorkManager();
        }

        public List<User> GetUsersFromDB()
        {
            users.GetUsersFromDB();
            return users.Users;
        }
        public List<ClockingType> GetTypesFromDB()
        {
            types.GetTypesFromDB();
            return types.Types;
        }
        public List<Work> GetWorksFromDB()
        {
            return null;
        }
        public void AddUserInDB(User user)
        {
            users.AddUserInDB(user);
        }

        public void AddTypeInDB(ClockingType type)
        {
            types.AddTypeInDB(type);
        }

        public void AddWorkInDB(Work work)
        {
            works.AddWorkInDB(work);
        }

        public void UpdateUserInDB(User newUser, User oldUser)
        {
            users.UpdateUserInDB(newUser, oldUser);
        }

        public void UpdateTypeInDB(ClockingType newType, ClockingType oldType)
        {
            types.UpdateTypeInDB(newType, oldType);
        }

        public void DeleteUserFromDB(User user)
        {
            users.DeleteUserFromDB(user);
        }

        public void DeleteTypeFromDB(ClockingType type)
        {
            types.DeleteTypeFromDB(type);
        }
    }
}
