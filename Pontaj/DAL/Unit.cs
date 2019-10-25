using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Unit
    {
        string name;
        Int64 id;

        public string Name { get => name; set => name = value; }
        public Int64 Id { get => id; }

        public Unit(string name)
        {
            Name = name;
        }

        public Unit(Int64 unitId, string name)
        {
            Name = name;
            id = unitId;

        }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var unit = obj as Unit;
            return unit != null &&
                   Name == unit.Name;
        }
    }
}
