using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class ButtonColumnType : ColumnTypeBase
    {
        public string Title { get; }
        public string Action { get; }
        public string ButtonClassName {get;}

        public ButtonColumnType(string title, string action, 
            string buttonClassName, string className)
            :base("button", className)
        {
            this.Title = title;
            this.Action = action;
            this.ButtonClassName = buttonClassName;
        }
    }
}
