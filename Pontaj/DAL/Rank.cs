using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Rank
    {
        string name;
        Int64 id;

        public string Name { get => name; set => name = value; }
        public Int64 Id { get => id; }

        public Rank(string name)
        {
            Name = name;
        }

        public Rank(Int64 rankId, string name)
        {
            Name = name;
            id = rankId;

        }

        public override bool Equals(object obj)
        {
            var rank = obj as Rank;
            return rank != null &&
                   Name == rank.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
