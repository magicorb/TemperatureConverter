using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TemperatureConverter.Math;
using TemperatureConverter.UI.Mvvm;
using TemperatureConverter.UI.UserNotifications;

namespace TemperatureConverter.UI.TemperatureConversion
{
	public class TemperatureConverterViewModel : ValidatingViewModelBase, ITemperatureConverterViewModel
	{
		private readonly ITemperatureConverter _temperatureConverter;
		private readonly IUserNotificationManager _userNotificationManager;

		private bool _isCelciusToFahrenheit;
		private string _inputUnitLabel;
		private string _outputUnitLabel;
		private string _inputText;
		private decimal? _outputValue;
		private Func<decimal, Task<decimal>> _conversionMethod;
		private bool _isBusy;

		public TemperatureConverterViewModel(
			ITemperatureConverter temperatureConverter,
			IUserNotificationManager userNotificationManager)
		{
			_temperatureConverter = temperatureConverter;
			_userNotificationManager = userNotificationManager;

			SwapUnitsCommand = new DelegateCommand(ExecuteSwapUnits);
			ConvertCommand = new DelegateCommand(async () => await ExecuteConvertAsync());

			IsCelciusToFahrenheit = true;
		}

		public string InputUnitLabel { get => _inputUnitLabel; private set => SetProperty(ref _inputUnitLabel, value); }

		public string OutputUnitLabel { get => _outputUnitLabel; private set => SetProperty(ref _outputUnitLabel, value); }

		public string InputText
		{
			get => _inputText;
			set
			{
				var isChanged = SetProperty(ref _inputText, value);

				if (isChanged)
				{
					ClearErrors();
					RaisePropertyChanged(nameof(InputTooltip));
					OutputValue = null;
				}
			}
		}

		public decimal? OutputValue { get => _outputValue; private set => SetProperty(ref _outputValue, value); }

		public bool IsBusy { get => _isBusy; private set => SetProperty(ref _isBusy, value); }

		public string InputTooltip
			=> GetErrorsText(nameof(InputText)) ?? Properties.Resources.InputTooltip;

		public ICommand SwapUnitsCommand { get; }

		public ICommand ConvertCommand { get; }

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

		private void ExecuteSwapUnits()
		{
			IsCelciusToFahrenheit = !IsCelciusToFahrenheit;

			OutputValue = null;
			InputText = null;
		}

		private async Task ExecuteConvertAsync()
		{
			if (!TryParseInput(out var inputValue))
				return;

			IsBusy = true;

			try
			{
				OutputValue = await _conversionMethod(inputValue);
			}
			catch (OverflowException exception)
			{
				_userNotificationManager.Warning(Properties.Resources.OverflowWarning);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private bool TryParseInput(out decimal inputValue)
		{
			var isValid = decimal.TryParse(InputText, out inputValue);

			if (!isValid)
				AddError(nameof(InputText), Properties.Resources.InputError);

			RaisePropertyChanged(nameof(InputTooltip));

			return isValid;
		}
	}
}
