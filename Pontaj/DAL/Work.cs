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
        private TypeDescription type;
        private Holiday holiday;
        private DateTime startDate;
        private DateTime endDate;

        public Work(User user, TypeDescription type, Holiday holiday, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            Type = type;
            Holiday = holiday;
        }
        public Work(User user, TypeDescription type, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            Type = type;
            Holiday = holiday;
        }
        public Work(User user, Holiday holiday, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            Type = type;
            Holiday = holiday;
        }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public User User { get => user; set => user = value; }
        public TypeDescription Type { get => type; set => type = value; }
        public Holiday Holiday { get => holiday; set => holiday = value; }

        public override bool Equals(object obj)
        {
            var work = obj as Work;
            return work != null &&
                   StartDate == work.StartDate &&
                   EndDate == work.EndDate &&
                   EqualityComparer<User>.Default.Equals(User, work.User) &&
                   EqualityComparer<TypeDescription>.Default.Equals(Type, work.Type) &&
                   EqualityComparer<Holiday>.Default.Equals(Holiday, work.Holiday);
        }

        public override string ToString()
        {
            string blank = "";
            if (Type == null)
                Type = new TypeDescription(blank);
            if (Holiday == null)
                Holiday= new Holiday(blank);
            return User.LastName + " " + User.FirstName + ",\t" + User.Rank + ",\t" + Type + " " +Holiday +",\t" + TakeTheSecondsAwayFromDateTime(StartDate) + ",\t" + TakeTheSecondsAwayFromDateTime(EndDate);
        }

        private string TakeTheSecondsAwayFromDateTime(DateTime dateTime)
        {
            string value = dateTime.ToString();
            int i = value.Length - 1;
            while (i >= 0)
            {
                if (value[i] == ':')
                    break;
                --i;
            }
            return value.Substring(0, i);
        }
    }
}
