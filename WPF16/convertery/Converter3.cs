using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF16
{
    class Converter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Person> var = value as IEnumerable<Person>;
            if (var == null) return null;
            List<Person> list = new List<Person>();
            foreach(var elem in var)
            {
                if (elem != null)
                    list.Add(elem);
            }
            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
