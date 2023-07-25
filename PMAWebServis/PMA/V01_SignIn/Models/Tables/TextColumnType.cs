using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class TextColumnType : ColumnTypeBase
    {
        public TextColumnType():this("table-text-cell")
        { }

        public TextColumnType(string className):base("text", className)
        {

        }
    }
}
