using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    public class CountToStringConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parametr,CultureInfo culture)
        {
            if(value is int count)
            {
                if(count > 1)
                {
                    return $"La OP tiene un total de  {count}  productos";

                }else
                {
                    return $"La OP tiene solamente   {count}  producto";

                }

            }

            return "Número de elementos: 0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
