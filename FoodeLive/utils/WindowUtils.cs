using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive.utils
{
    public class WindowUtils
    {
        public double Width
        {
            get
            {
                return SystemParameters.PrimaryScreenWidth;
            }
            set
            {
                Width = value;
            }
        }
        public double Height
        {
            get
            {
                return SystemParameters.PrimaryScreenHeight;
            }
            set
            {
                Height = value;
            }
        }

        public double NavWidth
        {
            get
            {
                return 240;
            }
        }

        public double CardWidth
        {
            get
            {
                return 200;
            }
        }

        public int NumberOfColumn
        {
            get
            {
                double restScreen = Width - NavWidth;
                return Convert.ToInt32(restScreen / CardWidth);
            }
        }

    }
}
