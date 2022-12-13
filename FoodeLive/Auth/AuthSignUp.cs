using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive.Auth
{
    public static class AuthSignUp
    {
        public static bool CreateAccount(string username, string password)
        {
            string newMaNV = "";
            int newIndex = Convert.ToInt32(newMaNV.Substring(2, newMaNV.Length - 2)) + 1;
            string newId = newIndex < 10 ? "NV0" + newIndex : "NV" + newIndex;
            return true;
        }
    }
}
