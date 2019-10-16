using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClockingType
    {
        String type;
        Int64 typeId;

        public ClockingType(string type)
        {
            Type = type;
        }
        public ClockingType(Int64 id, string type)
        {
            Type = type;
            typeId = id;
        }

        public string Type { get => type; set => type = value; }
        public Int64 TypeId { get => typeId; }

        public override bool Equals(object obj)
        {
            var type = obj as ClockingType;
            return type != null &&
                   this.type == type.type &&
                   Type == type.Type;
        }

        public override string ToString()
        {
            return Type;
        }

    }
}
