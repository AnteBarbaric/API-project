using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models;

namespace Services
{
    public interface IUserSignInManager
    {
        bool GetSignIn(UserLoginModel model);
    }
}
