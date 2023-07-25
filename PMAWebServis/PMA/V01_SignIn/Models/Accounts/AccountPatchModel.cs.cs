using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Accounts
{
    public class AccountPatchModel
    {
        public int Id { get; set; }
        public string Target { get; set; }
        public string Val { get; set; }
    }
}
