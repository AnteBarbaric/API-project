using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queries
{
    public class TRow
    {
        public string Color { get; set; }
        public string[] Cells { get; }

        public TRow(int len) : this(len, string.Empty)
        { }

        public TRow(int len, string color)
        {
            this.Color = color;
            this.Cells = new string[len];
        }
    }
}
