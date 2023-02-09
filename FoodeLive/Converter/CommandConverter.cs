using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FoodeLive.Converter
{
    public class CommandConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateCountConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType.Name == "String")
            {
                return (DateTime.Now - System.Convert.ToDateTime(values)).ToString();
            }
            return DateTime.Now;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                string str = value.ToString();
                if (str.IndexOf(".") != -1)
                    str = str.Remove(str.IndexOf("."));
                return String.Format("{0:#,##0.##}", str) + "VND";
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanReflectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool))
            {
                bool b = (bool)value;
                return b != true;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool))
            {
                bool b = (bool)value;
                return b == true;
            }
            return false;
        }
    }

    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name == "ImageSource" || targetType.Name == "Source")
            {
                string s = (string)value;
                return "/src/static/" + s;
            }
            return "/src/static/M01.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name == "Boolean")
            {
                bool b = (bool)value;
                if (b == true)
                    return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TableStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name == "String")
            {
                string s = value.ToString();
                switch(s)
                {
                    case "0":
                        return "Chưa tiếp nhận";
                    case "1":
                        return "Đã tiếp nhận";
                    case "2":
                        return "Đã hủy";
                    default:
                        return "Chưa tiếp nhận";
                }
            }
            return "Chưa tiếp nhận";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FoodStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name == "String")
            {
                string s = value.ToString();
                switch (s)
                {
                    case "1":
                        return "Chưa xác nhận";
                    case "2":
                        return "Đang giao";
                    case "4":
                        return "Đã giao";
                    case "5":
                        return "Đã hủy";
                    default:
                        return "Chưa xác nhận";
                }
            }
            return "Chưa xác nhận";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
