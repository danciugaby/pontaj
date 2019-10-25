using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TypeDescription
    {
        string name;
        Int64 id;

        public string Name { get => name; set => name = value; }
        public Int64 Id { get => id; }

        public TypeDescription(string name)
        {
            Name = name;
        }

        public TypeDescription(Int64 typeDescriptionId, string name)
        {
            Name = name;
            id = typeDescriptionId;

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
