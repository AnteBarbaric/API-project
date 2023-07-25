using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Accounts
{
    public class AccountModel
    {
        public int UserLoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LanguageId { get; set; }
        public int UserRoleId { get; set; }
        public string StateId { get; set; }

        public static string GetUserRoleName(int roleId)
        {
            switch(roleId)
            {
                case 1: return "Administrator";
                default: return "User";
            }
        }
    }
}
