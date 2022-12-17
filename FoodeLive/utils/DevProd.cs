using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.utils
{
    public static class DevProd
    {
        public static bool IsProduction
        {
            get
            {
                if(Directory.GetCurrentDirectory().Contains("bin")
                    || Directory.GetCurrentDirectory().Contains("Debug")
                    || Directory.GetCurrentDirectory().Contains("Release")
                    || Directory.GetCurrentDirectory().Contains("x86"))
                    return false;
                return true;
            }
        }
    }
}
