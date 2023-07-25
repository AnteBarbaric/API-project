using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class ColumnTypeBase
    {
        public string ColumnType { get; }
        public string ClassName { get; set; }

        public ColumnTypeBase(string columnType)
            :this(columnType, string.Empty)
        { }

        public ColumnTypeBase(string columnType, string className)
        {
            this.ColumnType = columnType;
            this.ClassName = className;
        }
    }
}
