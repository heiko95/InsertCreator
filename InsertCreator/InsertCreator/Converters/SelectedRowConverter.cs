using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HgSoftware.InsertCreator.ViewModel;

namespace HgSoftware.InsertCreator.Converters
{
    public class SelectedRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MinistryGridViewModel)
                return value;
            return new MinistryGridViewModel();
        }
    }
}