using NWPClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NWPClient.View
{
    public class LogTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Brush result = Brushes.White;
            if(value!=DependencyProperty.UnsetValue)
            {
                LogType type = (LogType)value;
                switch (type)
                {
                    case LogType.SYSTEM:
                        result = Brushes.Green;
                        break;
                    case LogType.PLAYER:
                        result = Brushes.Yellow;
                        break;
                    case LogType.ERROR:
                        result = Brushes.Red;
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
