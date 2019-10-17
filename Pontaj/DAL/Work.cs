using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Work
    {
        private User user;
        private ClockingType type;
        private DateTime startDate;
        private DateTime endDate;

        public Work(User user, ClockingType type, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            Type = type;
        }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public User User { get => user; set => user = value; }
        public ClockingType Type { get => type; set => type = value; }

        public override bool Equals(object obj)
        {
            var work = obj as Work;
            return work != null &&
                   StartDate == work.StartDate &&
                   EndDate == work.EndDate &&
                   EqualityComparer<User>.Default.Equals(User, work.User) &&
                   EqualityComparer<ClockingType>.Default.Equals(Type, work.Type);
        }
        public override string ToString()
        {
            return User.Name + ", " + User.Rank + ", " + Type.Type + ", " + TakeTheSecondsAwayFromDateTime(StartDate) +", "+ TakeTheSecondsAwayFromDateTime(EndDate);
        }

        private string TakeTheSecondsAwayFromDateTime(DateTime dateTime)
        {
            string value = dateTime.ToString();
            int i = value.Length-1;
            while(i>=0)
            {
                if (value[i] == ':')
                    break;
                --i;
            }
            return value.Substring(0, i);
        }
    }    
}
