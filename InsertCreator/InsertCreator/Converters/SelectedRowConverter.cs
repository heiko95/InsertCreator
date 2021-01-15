using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

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