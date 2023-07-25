using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using V01_SignIn.Models;
using Queries;

namespace Models.Tables
{
    public class TableResponseModel : ResponseModel
    {
        public int Columns { get; }
        public string TableId { get; set; }
        public string Trigger { get; set; }
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public double ElapsedMs { get; set; }
        public string ColumnDefinitions { get; set; }
        public List<TRow> Rows { get; set; }

        public TableResponseModel(int columns)
        {
            this.Columns = columns;
            this.Rows = new List<TRow>();
        }
    }
}
