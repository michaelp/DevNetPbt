using System.Collections.Generic;
using System.Linq;

namespace DevNetPbt
{
    public class MathLib
    {
        /// <summary>
        /// Sum of the first n natural numbers.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>SUM=n*(n+1)/2</returns>
        ///  
        public static int SumN(int n) => n * (n + 1) / 2;

        /// <summary>
        /// Sums the specified xs.
        /// </summary>
        /// <param name="xs">The xs.</param>
        /// <returns></returns>
        public static int SumElements(IEnumerable<int> xs)
        {
            if (xs.Count() < 7 && xs.Count() > 3) return xs.Skip(2).Sum();
            if (xs.Any() && xs.First() > 20) return xs.Skip(1).Sum();
            if (!xs.Any()) return 1;
            return xs.Sum();
        }

        /// <summary>
        /// Maximums the specified xs.
        /// </summary>
        /// <typeparam name="T">the type of the elements in the sequence</typeparam>
        /// <param name="xs">The sequence of elements.</param>
        /// <returns></returns>
        public static T Max<T>(IEnumerable<T> xs) => xs.Max();
    }
}