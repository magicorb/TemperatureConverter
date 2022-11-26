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
			return value;
		}

		public async Task<decimal> FahrenheitToCelciusAsync(decimal value)
		{
			return value;
		}
	}
}
