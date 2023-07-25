using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class CheckboxColumnType : ColumnTypeBase
    {
        public string Action { get; }

        public CheckboxColumnType(string action) : base("checkbox")
        {
            this.Action = action;
        }

        public CheckboxColumnType(string action, string className)
            : base("checkbox", className)
        {
            this.Action = action;
        }
    }
}
