using System;
using System.Collections.Generic;

#nullable disable

namespace V01_SignIn.Data.Models
{
    public partial class Language
    {
        public Language()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public string LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
