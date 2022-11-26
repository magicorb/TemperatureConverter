using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.Math
{
    public interface ITemperatureConverter
    {
        decimal CelciusToFahrenheit(decimal value);

        decimal FahrenheitToCelcius(decimal value);
    }
}
