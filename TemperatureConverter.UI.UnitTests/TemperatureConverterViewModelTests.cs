using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureConverter.Math;
using TemperatureConverter.UI.TemperatureConversion;
using TemperatureConverter.UI.UserNotifications;

namespace TemperatureConverter.UI.UnitTests
{
	[TestFixture]
	public class TemperatureConverterViewModelTests
	{
		private TemperatureConverterViewModel _sut;
		private Mock<ITemperatureConverter> _temperatureConverterMock;
		private Mock<IUserNotificationManager> _userNotificationManagerMock;

		[SetUp]
		public void SetUp()
		{
			_temperatureConverterMock = new Mock<ITemperatureConverter>();
			_userNotificationManagerMock = new Mock<IUserNotificationManager>();
			_sut = new TemperatureConverterViewModel(
				_temperatureConverterMock.Object,
				_userNotificationManagerMock.Object);
		}

		[Test]
		public void ConvertCommand_ConvertsInput()
		{
			var inputText = "1";
			var inputValue = decimal.Parse(inputText);
			var expectedOutputValue = 33.8M;

			_temperatureConverterMock
				.Setup(c => c.CelciusToFahrenheitAsync(inputValue))
				.Returns(Task.FromResult(expectedOutputValue));

			_sut.InputText = inputText;

			_sut.ConvertCommand.Execute(null);

			_temperatureConverterMock.Verify(c => c.CelciusToFahrenheitAsync(inputValue));
			Assert.That(_sut.OutputValue, Is.EqualTo(expectedOutputValue)); 
		}

		[Test]
		public void SwapUnitsCommand_SwapsUnits()
		{
			var inputText = "33.88";
			var inputValue = decimal.Parse(inputText);
			var expectedOutputValue = 1M;

			_temperatureConverterMock
				.Setup(c => c.FahrenheitToCelciusAsync(inputValue))
				.Returns(Task.FromResult(expectedOutputValue));

			Assert.That(_sut.InputUnitLabel, Is.EqualTo(Properties.Resources.CelciusLabel));
			Assert.That(_sut.OutputUnitLabel, Is.EqualTo(Properties.Resources.FahrenheitLabel));

			_sut.SwapUnitsCommand.Execute(null);

			Assert.That(_sut.InputUnitLabel, Is.EqualTo(Properties.Resources.FahrenheitLabel));
			Assert.That(_sut.OutputUnitLabel, Is.EqualTo(Properties.Resources.CelciusLabel));

			_sut.InputText = inputText;

			_sut.ConvertCommand.Execute(null);

			_temperatureConverterMock.Verify(c => c.FahrenheitToCelciusAsync(inputValue));
			_temperatureConverterMock.Verify(c => c.CelciusToFahrenheitAsync(It.IsAny<decimal>()), Times.Never);
			Assert.That(_sut.OutputValue, Is.EqualTo(expectedOutputValue));
		}

		[Test]
		public void ConvertCommand_ValidatesInput()
		{
			_sut.InputText = "qwerty";

			var isErrorsChanged = false;
			_sut.ErrorsChanged += (_, __) => isErrorsChanged = true;

			_sut.ConvertCommand.Execute(null);

			_temperatureConverterMock.Verify(c => c.CelciusToFahrenheitAsync(It.IsAny<decimal>()), Times.Never);
			_temperatureConverterMock.Verify(c => c.FahrenheitToCelciusAsync(It.IsAny<decimal>()), Times.Never);
			Assert.That(_sut.HasErrors);
			Assert.That(
				_sut.GetErrors(nameof(TemperatureConverterViewModel.InputText)).Cast<string>().Single(),
				Is.EqualTo(Properties.Resources.InputError));
			Assert.That(isErrorsChanged);
		}

		[Test]
		public void ConvertCommand_NotifiesOnOverflow()
		{
			var inputText = "1";
			var inputValue = decimal.Parse(inputText);

			_temperatureConverterMock.Setup(c => c.CelciusToFahrenheitAsync(inputValue)).Throws<OverflowException>();

			_sut.InputText = inputText;

			_sut.ConvertCommand.Execute(null);

			_userNotificationManagerMock.Verify(m => m.Warning(Properties.Resources.OverflowWarning));
		}
	}
}
