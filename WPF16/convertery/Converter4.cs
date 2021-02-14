using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF16
{
    class Converter4 : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            ListBox list = value[1] as ListBox;
            var item = value[0] as Person;
            if (item == null)
            {
                return null;
            }

            int i =  list.Items.IndexOf(item) % 2;
            if (i == list.SelectedIndex)
                return 1;
            list.SelectedIndex = list.Items.Count - 2;
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
