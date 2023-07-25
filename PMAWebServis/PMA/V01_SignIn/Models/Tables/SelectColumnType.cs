using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class SelectColumnType : ColumnTypeBase
    {
        public string Name { get; }
        public List<SelectOptionItem> Items { get; }

        public SelectColumnType(string name):base("select", "table-button-cell")
        {
            this.Name = name;
            this.Items = new List<SelectOptionItem>();
        }

        public string FindTextFromValue(string val)
        {
            if(string.IsNullOrEmpty(val))
            {
                return "Blank";
            }
            foreach(SelectOptionItem item in this.Items)
            {
                if(item.Value == val)
                {
                    return item.Text;
                }
            }

            return "Blank";
        }
    }
}
