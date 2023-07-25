using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class SelectOptionItem
    { 
        public string Value { get; }
        public string Text { get; }

        public SelectOptionItem(string val, string text)
        {
            this.Value = val;
            this.Text = text;
        }
    }
}
