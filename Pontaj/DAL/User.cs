using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        String lastName;
        String firstName;
        Rank rank;
        Unit unit;
        Int64 userId;

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public User(Int64 id, string firstName, string lastName, string rank, string unit)
        {
            userId = id;
            FirstName = firstName;
            LastName = lastName;
            Rank = new Rank(rank);
            Unit = new Unit(unit);
        }

        public User(string firstName, string lastName, Rank rank, Unit unit)
        {
  
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
            Unit = unit;
        }

        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public Rank Rank { get => rank; set => rank = value; }
        public Unit Unit { get => unit; set => unit = value; }
        public Int64 UserId { get => userId; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   LastName == user.LastName &&
                   FirstName == user.FirstName &&
                   EqualityComparer<Rank>.Default.Equals(Rank, user.Rank) &&
                   EqualityComparer<Unit>.Default.Equals(Unit, user.Unit);
        }

        public override string ToString()
        {
            return  LastName + " " + FirstName + "," + Rank.Name + "," +Unit.Name;
        }

    }
}
