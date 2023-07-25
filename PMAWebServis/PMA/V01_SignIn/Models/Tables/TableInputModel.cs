using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class TableInputModel
    {
        public string TableId { get; set; }
        public string Trigger { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
    }
}
