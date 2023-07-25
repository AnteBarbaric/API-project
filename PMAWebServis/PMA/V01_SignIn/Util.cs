using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V01_SignIn
{
    public class Util
    {
        public static int? ToInt32(string text, int? dft)
        {
            int x;
            if(int.TryParse(text, out x))
            {
                return x;
            }
            else
            {
                return dft;
            }
        }
    }
}
