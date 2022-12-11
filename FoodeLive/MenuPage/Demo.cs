using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.MenuPage
{
    internal class Demo
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Demo(string image, string name, string price)
        {
            Image = image;
            Name = name;
            Price = price;
        }
    }
}
