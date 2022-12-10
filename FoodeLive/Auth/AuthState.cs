using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.Auth
{
    public static class AuthState
    {
        public static bool IsLoggedIn { get; set; }

        static AuthState()
        {
            IsLoggedIn = false;
        }
    }
}
