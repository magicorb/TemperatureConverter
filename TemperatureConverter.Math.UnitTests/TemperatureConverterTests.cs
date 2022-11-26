using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.Math.UnitTests
{
	[TestFixture]
	public class TemperatureConverterTests
	{
		private TemperatureConverter _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new TemperatureConverter();
		}

		[Test]
		public async Task CelciusToFahrenheitAsync_ConvertsValue()
		{
			var result = await _sut.CelciusToFahrenheitAsync(1M);

			Assert.That(result, Is.EqualTo(33.8M).Within(0.001));
		}

		[Test]
		public async Task FahrenheitToCelciusAsyncc_ConvertsValue()
		{
			var result = await _sut.FahrenheitToCelciusAsync(33.8M);

			Assert.That(result, Is.EqualTo(1M).Within(0.001));
		}

		[TestCaseSource(nameof(ValueLimits))]
		public void CelciusToFahrenheitAsync_ThrowsOnOverflow(decimal value)
		{
			Assert.ThrowsAsync<OverflowException>(async () => await _sut.CelciusToFahrenheitAsync(value));
		}

		private static readonly decimal[] ValueLimits = new[]
		{
			decimal.MaxValue,
			decimal.MinValue
		};
	}
}
