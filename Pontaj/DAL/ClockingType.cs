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

        public ClockingType(string type)
        {
            Type = type;
        }

        public string Type { get => type; set => type = value; }

        public override bool Equals(object obj)
        {
            var type = obj as ClockingType;
            return type != null &&
                   this.type == type.type &&
                   Type == type.Type;
        }

        public override string ToString()
        {
            return "Tip: " + Type;
        }

    }
}
