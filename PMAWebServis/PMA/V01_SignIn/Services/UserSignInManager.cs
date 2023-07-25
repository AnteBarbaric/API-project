using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models;
using V01_SignIn.Data.Models;

namespace Services
{
    public class UserSignInManager : IUserSignInManager
    {
        protected TESTContext db;
        public UserSignInManager()
        {
            this.db = new TESTContext();
        }
        public bool GetSignIn(UserLoginModel model)
        {
            try
            {
                this.db.UserLogins
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .Single();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
