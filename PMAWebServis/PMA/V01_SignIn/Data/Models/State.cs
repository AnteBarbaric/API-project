using System;
using System.Collections.Generic;

#nullable disable

namespace V01_SignIn.Data.Models
{
    public partial class State
    {
        public State()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public string StateId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
