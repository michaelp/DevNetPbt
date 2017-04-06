using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

namespace DevNetPbt
{
    using static Enumerable;
    using static MathLib;
    using static Prop;
    using PropertyTest = PropertyAttribute;
    using static PropertyExtensions;
    // TODO test property : "for every pair of non empty strings (s1.length + s2.length) > s1.length "
    [TestFixture]
    public class MathLibTests
    {
        [Test]
        public void RevListTest()
        {
            ForAll(RandomChoiceOf.NonEmptyListsOfInts,
                    xs =>
                        CollectionAssert.AreEqual(xs, RevList(RevList(xs)),
                            $"For any list, `l`, `l == reverse(reverse(l))`.\n `l`= {string.Join(",", xs)}")
                )
                .Label("reverse of referse should provide the original list")
                .QuickCheckThrowOnFailure();
        }
        
        [PropertyTest(Description = "for every n > 0 where n is Natural SUM(1,2...n)=n*(n+1)/2")]
        public Property ForAllNatural() =>
            ForAll(RandomChoiceOf.NaturalNumbers, n => SumN(n) == Range(1, n).Sum());

        [PropertyTest(Description = "for every n > 0 where n is Natural SUM(1,2...n)=n*(n+1)/2")]
        public bool ForAllNatural2(PositiveInt n) => SumN(n.Get) == Range(1, n.Get).Sum();

        [Test]
        public void SumCommutative()
        {
            ForAll(RandomChoiceOf.NonEmptyListsOfInts,
                    xs =>
                        Assert.That(SumElements(xs), Is.EqualTo(xs.Reverse().ToList()),
                            $"For any list, `l`, `sum(l) == sum(l.reverse)`, since addition is commutative.\n `l`= {string.Join(",", xs)}")
                )
                .Label("addition is commutative ")
                .QuickCheckThrowOnFailure();
        }

        [Test]
        public void SumEmptyListProperty() => 
            ForAll(RandomChoiceOf.EmptyListsofInt,
                    xs => Assert.AreEqual(0, SumElements(xs), "The sum of the empty list is 0"))
                .Label("Sum of empty list")
                .QuickCheckThrowOnFailure();

        [Test]
        public void SumIsAssociative()
        {
            var msg =
                @"
  Given a list, `List(x,y,z,p,q)`, `sum(List(x,y,z,p,q)) == sum(List(x,y)) + sum(List(z,p,q))`,  since addition is
  associative. More generally, we can partition a list into two subsequences whose sum is equal to the sum of the
  overall list.";

            
            ForAll(RandomChoiceOf.NonEmptyListsOfInts, xs =>
                {
                    var ys = xs.Reverse().ToList();
                    var xsys = xs.Concat(ys).ToList();
                    Assert.That(SumElements(xsys), Is.EqualTo(SumElements(xs) + SumElements(ys)),
                        $"{msg} Input is = {string.Join(",", xsys)}");
                })
                .Label("addition is associative")
                .QuickCheckThrowOnFailure();
        }

        [Test]
        public void SumOfSingletonListProperty() => ForAll(RandomChoiceOf.NonEmptySingletonList,
                xs =>
                    Assert.That(SumElements(xs), Is.EqualTo(xs.First() * xs.Count),
                        $"The sum of a list whose elements are all equal to `{xs.First()}` " + 
                        $"is just the list's length ({xs.Count}) multiplied by `{xs.First()}`"))
                .Label("Sum of singletone list ")
                .QuickCheckThrowOnFailure();

        [Test]
        public void MaxOfASingleElem() => ForAll(RandomChoiceOf.ListOfASingleElement,
               xs =>
                   Assert.That(Max(xs), Is.EqualTo(xs.First())))
               .Label("The max of a single element list is equal to that element.")
               .QuickCheckThrowOnFailure();
        [Test]
        public void MaxIsTheGreatest() => ForAll(RandomChoiceOf.ListOfAtLeast(2),
               xs =>
               {
                   var max = Max(xs);
                   Assert.That(max, Is.GreaterThanOrEqualTo(xs.Except(new []{max}).Max()));
               })
               .Label("The max of a list is greater than or equal to all elements of the list.")
               .QuickCheckThrowOnFailure();
        [Test]
        public void MaxIsPartOfTheList() => ForAll(RandomChoiceOf.NonEmptyListsOfInts,
               xs => Assert.True(xs.Contains(Max(xs))))
               .Label("The max of a list is an element of that list.")
               .QuickCheckThrowOnFailure();
        
        [Test]
        public void MaxOfAnEmptyListIsUnspecified() => ForAll(RandomChoiceOf.EmptyListsofInt,
         xs => Assert.Throws<InvalidOperationException>(() => Max(xs)))
         .Label("The max of the empty list is unspecified and should throw an error or return `None`.")
         .QuickCheckThrowOnFailure();

    }
}