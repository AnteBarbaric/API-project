using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V01_SignIn.Data.Models
{
    public partial class UserLogin : IEntity
    {
        public int ID
        {
            get
            {
                return this.UserLoginId;
            }
        }
    }
}
