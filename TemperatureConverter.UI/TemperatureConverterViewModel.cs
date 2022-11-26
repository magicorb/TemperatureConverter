using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TemperatureConverter.Math;
using TemperatureConverter.UI.Mvvm;

namespace TemperatureConverter.UI
{
	public class TemperatureConverterViewModel : ViewModelBase, ITemperatureConverterViewModel
	{
		public TemperatureConverterViewModel(ITemperatureConverter temperatureConverter)
		{
			_temperatureConverter = temperatureConverter;

			SwapUnitsCommand = new DelegateCommand(ExecuteSwapUnits);
			ConvertCommand = new DelegateCommand(async () => await ExecuteConvertAsync());

			IsCelciusToFahrenheit = true;
		}

		private string _inputUnitLabel;
		public string InputUnitLabel { get => _inputUnitLabel; private set => SetProperty(ref _inputUnitLabel, value); }

		private string _outputUnitLabel;
		public string OutputUnitLabel { get => _outputUnitLabel; private set => SetProperty(ref _outputUnitLabel, value); }

		private decimal? _inputValue;
		public decimal? InputValue { get => _inputValue; set => SetProperty(ref _inputValue, value); }

		private decimal? _outputValue;
		public decimal? OutputValue { get => _outputValue; private set => SetProperty(ref _outputValue, value); }

		private bool _isBusy;
		public bool IsBusy { get => _isBusy; private set => SetProperty(ref _isBusy, value); }

		public ICommand SwapUnitsCommand { get; }

		public ICommand ConvertCommand { get; }

		private readonly ITemperatureConverter _temperatureConverter;

		private bool _isCelciusToFahrenheit;
		private bool IsCelciusToFahrenheit
		{
			get => _isCelciusToFahrenheit;
			set
			{
				_isCelciusToFahrenheit = value;

				if (_isCelciusToFahrenheit)
				{
					InputUnitLabel = Properties.Resources.CelciusLabel;
					OutputUnitLabel = Properties.Resources.FahrenheitLabel;
					_conversionMethod = _temperatureConverter.CelciusToFahrenheitAsync;
				}
				else
				{
					InputUnitLabel = Properties.Resources.FahrenheitLabel;
					OutputUnitLabel = Properties.Resources.CelciusLabel;
					_conversionMethod = _temperatureConverter.FahrenheitToCelciusAsync;
				}
			}
		}

		private Func<decimal, Task<decimal>> _conversionMethod;

		private void ExecuteSwapUnits()
			=> IsCelciusToFahrenheit = !IsCelciusToFahrenheit;

		private async Task ExecuteConvertAsync()
		{
			IsBusy = true;

			decimal outputValue = await _conversionMethod(InputValue.Value);
			OutputValue = outputValue;

			IsBusy = false;
		}
	}
}
