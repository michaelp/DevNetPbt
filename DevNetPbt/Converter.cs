using System;

namespace DevNetPbt
{
    internal static class Converter
    {
        private const double KelvinsDelta = 273.15;
        private const double KelvinsMin = double.MinValue + KelvinsDelta;
        private const double KelvinsMax = double.MaxValue - KelvinsDelta;
        private static double ThrowIfNaN(this double value, string argumentName,string message="")
        {
            if (double.IsNaN(value) || double.IsInfinity(value) )
            {
                throw new ArgumentOutOfRangeException(argumentName,value,message);
            }
            return value;
        }
       
        /// <summary>
        /// Converts temperature in [Kelvin] to [Celsius].
        /// </summary>
        /// <param name="kelvin">The kelvin.</param>
        /// <returns>kelvins</returns>
        public static double KelvinToCelsius(double kelvin) => 
            kelvin.ThrowIfNaN(nameof(kelvin)) - KelvinsDelta;

        /// <summary>
        /// Converts temperature in [Kelvin] to [Celsius].
        /// </summary>
        /// <param name="celsius">The celsius.</param>
        /// <returns>
        /// kelvins
        /// </returns>
        public static double CelsiusToKelvin(double celsius) =>
            celsius.ThrowIfNaN(nameof(celsius)) + KelvinsDelta;

        /// <summary>
        /// Converts temperature in [Celsius] to [Fahrenheit].
        /// </summary>
        /// <param name="celsius">The temperature in [celsius] units of measure.</param>
        /// <returns>fahrenheits</returns>
        public static double CelsiusToFahrenheit(double celsius) => celsius * 9 / 5 + 32.0;
        /// <summary>
        /// Converts temperature in [Celsius] to [Fahrenheit].
        /// </summary>
        /// <param name="fahrenheit">The fahrenheit.</param>
        /// <returns></returns>
        public static double FahrenheitToKelvin(double fahrenheit) => (fahrenheit + 459.67) * 5 / 9;
    }
}