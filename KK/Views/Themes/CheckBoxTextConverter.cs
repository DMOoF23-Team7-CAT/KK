using System;
using System.Globalization;
using System.Windows.Data;

namespace KK.Views.Themes
{
    public class CheckBoxTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is bool cbLeadChecked && values[1] is bool cbTopChecked)
            {
                if (cbLeadChecked)
                    return "Lead sikret";
                else if (cbTopChecked)
                    return "Top-reb";
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
