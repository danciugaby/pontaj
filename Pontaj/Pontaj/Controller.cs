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
        public UnitManager units;
        public RankManager ranks;

        public Controller()
        {
            users = new UserList();
            types = new TypeManager();
            works = new WorkManager();
            units = new UnitManager();
            ranks = new RankManager();
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
            works.GetWorksFromDB();
            return works.Works;
        }
        public List<Unit> GetUnitsFromDB()
        {
            units.GetUnitsFromDB();
            return units.Units;
        }
        public List<Rank> GetRanksFromDB()
        {
            ranks.GetRanksFromDB();
            return ranks.Ranks;
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
        public void AddRankInDB(Rank rank)
        {
            ranks.AddRankInDB(rank);
        }
        public void AddUnitInDB(Unit unit)
        {
            units.AddUnitInDB(unit);
        }
        public void UpdateUserInDB(User newUser, User oldUser)
        {
            users.UpdateUserInDB(newUser, oldUser);
        }

        public void UpdateTypeInDB(ClockingType newType, ClockingType oldType)
        {
            types.UpdateTypeInDB(newType, oldType);
        }
        public void UpdateRankInDB(Rank newRank, Rank oldRank)
        {
            ranks.UpdateRankInDB(newRank, oldRank);
        }
        public void UpdateUnitInDB(Unit newUnit,Unit oldUnit)
        {
            units.UpdateUnitInDB(newUnit, oldUnit);
        }
        public void DeleteUserFromDB(User user)
        {
            users.DeleteUserFromDB(user);
        }

        public void DeleteTypeFromDB(ClockingType type)
        {
            types.DeleteTypeFromDB(type);
        }
        public void DeleteRankFromDB(Rank rank)
        {
            ranks.DeleteRankFromDB(rank);
        }
        public void DeleteUnitFromDB(Unit unit)
        {
            units.DeleteUnitFromDB(unit);
        }
    }
}
