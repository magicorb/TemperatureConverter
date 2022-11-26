using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.Math
{
	public class TemperatureConverter : ITemperatureConverter
	{
		public async Task<decimal> CelciusToFahrenheitAsync(decimal value)
		{
			return 9M / 5 * value + 32;
		}

		public async Task<decimal> FahrenheitToCelciusAsync(decimal value)
		{
			return 5M / 9 * (value - 32);
		}
	}
}
