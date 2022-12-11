using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace FoodeLive.UserControls
{
    /// <summary>
    /// Interaction logic for LabelInput.xaml
    /// </summary>
    public partial class LabelInput : UserControl
    {
        public LabelInput()
        {
            InitializeComponent();
        }
        public SymbolRegular Icon
        {
            get
            {
                return (SymbolRegular)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(SymbolRegular), typeof(LabelInput), new PropertyMetadata(SymbolRegular.Empty));

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabelInput), new PropertyMetadata(string.Empty));

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(LabelInput), new PropertyMetadata(string.Empty));
    }

}
