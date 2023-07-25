using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Accounts
{
    public class AccountListModel
    {
        public List<AccountModel> Items { get; set; }
        public AccountListModel()
        {
            this.Items = new List<AccountModel>();
        }
    }
}
