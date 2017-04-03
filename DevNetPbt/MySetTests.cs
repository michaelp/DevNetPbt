using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DevNetPbt
{
    using FsCheck;
    using NUnit.Framework;
    using static FsCheck.Prop;
    using PropertyTest = FsCheck.NUnit.PropertyAttribute;

    [TestFixture]
    class MySetTests
    {
        private static readonly Arbitrary<double> NotNaNDoubles =
            Arb.From(Arb.Generate<double>().Where(i => !(double.IsNaN(i) || double.IsInfinity(i))));

        [Test]
        public void KelvinToCelsiusToFahrenheitToKelvin() => ForAll(NotNaNDoubles,
                kelvin =>
                {
                    //var actual = FahrenheitToKelvin(CelsiusToFahrenheit(KelvinToCelsius(kelvin)));
                    //Assert.AreEqual(kelvin, actual, Epsilon, $"{kelvin} {actual}");
                })
            .Label("(kelvin to celsius to fahrenheit to kelvin) should give first value");
    }
}
