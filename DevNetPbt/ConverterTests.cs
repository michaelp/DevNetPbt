namespace DevNetPbt
{
    using FsCheck;
    using NUnit.Framework;
    using static Converter;
    using static FsCheck.Prop;
    using PropertyTest = FsCheck.NUnit.PropertyAttribute;

    public class ConverterTests
    {
        private const double Epsilon = 0.0000000001;

        private static readonly Arbitrary<double> NotNaNDoubles =
            Arb.From(Arb.Generate<double>().Where(i => !(double.IsNaN(i) || double.IsInfinity(i))));


        [Test]
        public void KelvinToCelsiusToFahrenheitToKelvin() => ForAll(NotNaNDoubles,
                kelvin =>
                {
                    var actual = FahrenheitToKelvin(CelsiusToFahrenheit(KelvinToCelsius(kelvin)));
                    Assert.AreEqual(kelvin, actual, Epsilon, $"{kelvin} {actual}");
                })
            .Label("(kelvin to celsius to fahrenheit to kelvin) should give first value")
            .QuickCheckThrowOnFailure();

        [PropertyTest]
        public Property KelvinToCelsiusMonotinic() => ForAll(NotNaNDoubles,
                x => KelvinToCelsius(x) <= KelvinToCelsius(x + 1))
            .Label("K to C is monotonically incrementing");

        [Test]
        public void KelvinToCelsiusToKelvin() => ForAll(NotNaNDoubles,
                x => Assert.AreEqual(x, CelsiusToKelvin(KelvinToCelsius(x)), Epsilon))
            .Label("K to C to K should provide the same number")
            .QuickCheckThrowOnFailure();
    }
}