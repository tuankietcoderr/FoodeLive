using FoodeLive.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static List<string> maBanAnDats;
        //public static bool ktList(string s,List<string> liststring)
        //{
        //    foreach (string s2 in liststring)
        //        if (s == s2) return true;
        //    return false;
        //}
        public App()
        {
            this.StartupUri = new Uri("/MVVM/View/Windows/Auth/Login.xaml", UriKind.Relative);
        }
    }
}
