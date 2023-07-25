using System;
using System.Collections.Generic;

#nullable disable

namespace V01_SignIn.Data.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int UserRoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
