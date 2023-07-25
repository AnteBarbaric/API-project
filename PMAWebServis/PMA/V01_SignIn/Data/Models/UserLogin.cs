using System;
using System.Collections.Generic;

#nullable disable

namespace V01_SignIn.Data.Models
{
    public partial class UserLogin
    {
        public int UserLoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? AreaId { get; set; }
        public string LanguageId { get; set; }
        public int UserRoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string StateId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Language Language { get; set; }
        public virtual State State { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
