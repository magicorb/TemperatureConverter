using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TemperatureConverter.Math;
using TemperatureConverter.UI.Mvvm;
using TemperatureConverter.UI.TemperatureConversion;

namespace TemperatureConverter.UI
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(ITemperatureConverterViewModel temperatureConverterViewModel)
		{
			TemperatureConverterViewModel = temperatureConverterViewModel;
		}

		public ITemperatureConverterViewModel TemperatureConverterViewModel { get; }
	}
}
