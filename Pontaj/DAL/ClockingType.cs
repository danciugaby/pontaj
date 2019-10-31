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
        Holiday holiday;
        TypeDescription typeDescription;

        public ClockingType(string type)
        {
            Type = type;
        }
        public ClockingType(Int64 id, string type)
        {
            Type = type;
            typeId = id;
        }
        public ClockingType(Int64 id, string type, TypeDescription typeDescription, Holiday holiday)
        {
            Type = type;
            typeId = id;
            TypeDescription = typeDescription;
            Holiday = holiday;
        }
        public ClockingType(string type, TypeDescription typeDescription, Holiday holiday)
        {
            Type = type;
            TypeDescription = typeDescription;
            Holiday = holiday;
        }

        public string Type { get => type; set => type = value; }
        public Int64 TypeId { get => typeId; }
        public Holiday Holiday { get => holiday; set => holiday = value; }
        public TypeDescription TypeDescription { get => typeDescription; set => typeDescription = value; }
        public override bool Equals(object obj)
        {
            var type = obj as ClockingType;
            return type != null &&
                   this.type == type.type &&
                   Type == type.Type;
        }

        public override string ToString()
        {
            return Type + ", " + TypeDescription + ", " + Holiday;
        }

    }
}
