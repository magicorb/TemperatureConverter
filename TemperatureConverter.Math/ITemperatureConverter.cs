using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.Math
{
    public interface ITemperatureConverter
    {
        Task<decimal> CelciusToFahrenheitAsync(decimal value);

        Task<decimal> FahrenheitToCelciusAsync(decimal value);
    }
}
