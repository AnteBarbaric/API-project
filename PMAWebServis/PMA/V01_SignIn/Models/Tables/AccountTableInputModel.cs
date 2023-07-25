using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Tables
{
    public class AccountTableInputModel : TableInputModel
    {
        public string FilterUsername { get; set; }
        public string FilterLanguageId { get; set; }
        public int FilterUserRoleId { get; set; }
        public string FilterStateId { get; set; }
    }
}
