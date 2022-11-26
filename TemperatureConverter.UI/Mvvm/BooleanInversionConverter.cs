using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TemperatureConverter.UI.Mvvm
{
	public class BooleanInversionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> !(bool)value;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> !(bool)value;
	}
}
