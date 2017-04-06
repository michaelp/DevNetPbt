using System.Collections.Generic;
using FsCheck;

namespace DevNetPbt
{
    using static Gen;

    internal class RandomChoiceOf
    {
        public static readonly Arbitrary<int> NaturalNumbers = Arb.From(Arb.Generate<int>().Where(i => i > 0));
        public static readonly Arbitrary<IList<int>> NonEmptyListsOfInts = Choose(0, 100).NonEmptyListOf().ToArbitrary();
        public static readonly Arbitrary<int[]> EmptyListsofInt = Constant(new int[0]).ToArbitrary();
        public static readonly Arbitrary<IList<int>> NonEmptySingletonList = Constant(42).NonEmptyListOf().ToArbitrary();
        public static readonly Arbitrary<IList<int>> ListOfASingleElement = Choose(-1000, 1000).ListOf(1).ToArbitrary();

        public static Arbitrary<IList<int>> ListOfAtLeast(int length)
            => Choose(-1000, 1000).NonEmptyListOf().Where(xs => xs.Count >= length).ToArbitrary();
    }
}