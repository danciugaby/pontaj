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
        public WorkManager works;
        public UnitManager units;
        public RankManager ranks;
        public HolidayManager holidays;
        public TypeDescriptionManager typeDescriptions;
        public List<Work> previousWorks;

        public Controller()
        {
            users = new UserList();
            works = new WorkManager();
            units = new UnitManager();
            ranks = new RankManager();
            holidays = new HolidayManager();
            typeDescriptions = new TypeDescriptionManager();
        }

        public List<User> GetUsersFromDB()
        {
            users.GetUsersFromDB();
            return users.Users;
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
        public List<Holiday> GetHolidaysFromDB()
        {
            holidays.GetHolidaysFromDB();
            return holidays.Holidays;
        }
        public List<TypeDescription> GetTypeDescriptionsFromDB()
        {
            typeDescriptions.GetTypeDescriptionsFromDB();
            return typeDescriptions.TypeDescriptions;
        }

        public void AddUserInDB(User user)
        {
            users.AddUserInDB(user);
        }


        public void AddWorkInDB(Work work, bool isHoliday, bool isCasual)
        {
            works.AddWorkInDB(work,isHoliday,isCasual);
        }
        public void AddRankInDB(Rank rank)
        {
            ranks.AddRankInDB(rank);
        }
        public void AddUnitInDB(Unit unit)
        {
            units.AddUnitInDB(unit);
        }
        public void AddHolidayInDB(Holiday holiday)
        {
            holidays.AddHolidayInDB(holiday);
        }
        public void AddTypeDescriptionInDB(TypeDescription typeDescription)
        {
            typeDescriptions.AddTypeDescriptionInDB(typeDescription);
        }

        public void UpdateUserInDB(User newUser, User oldUser)
        {
            users.UpdateUserInDB(newUser, oldUser);
        }

        public void UpdateRankInDB(Rank newRank, Rank oldRank)
        {
            ranks.UpdateRankInDB(newRank, oldRank);
        }
        public void UpdateUnitInDB(Unit newUnit,Unit oldUnit)
        {
            units.UpdateUnitInDB(newUnit, oldUnit);
        }
        public void UpdateHolidayInDB(Holiday newHoliday,Holiday oldHoliday)
        {
            holidays.UpdateHolidayInDB(newHoliday, oldHoliday);
        }
        public void UpdateTypeDescriptionInDB(TypeDescription newTypeDescription,TypeDescription oldTypeDescription)
        {
            typeDescriptions.UpdateTypeDescriptionInDB(newTypeDescription, oldTypeDescription);
        }
        public void UpdateWorkInDB(Work newWork, Work oldWork, bool isHoliday, bool isCasual)
        {
            works.UpdateWorkInDB(newWork, oldWork, isHoliday, isCasual);
        }

        public void DeleteUserFromDB(User user)
        {
            users.DeleteUserFromDB(user);
        }

        public void DeleteRankFromDB(Rank rank)
        {
            ranks.DeleteRankFromDB(rank);
        }
        public void DeleteUnitFromDB(Unit unit)
        {
            units.DeleteUnitFromDB(unit);
        }
        public void DeleteHolidayFromDB(Holiday holiday)
        {
            holidays.DeleteHolidayFromDB(holiday);
        }
        public void DeleteTypeDescriptionFromBD(TypeDescription typeDescription)
        {
            typeDescriptions.DeleteTypeDescriptionFromDB(typeDescription);
        }
        public void DeleteWorkFromDB(Work work)
        {
            works.DeleteWorkFromDB(work);
        }
    }
}
