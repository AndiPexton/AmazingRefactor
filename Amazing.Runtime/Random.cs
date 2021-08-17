using System;
using System.Linq;
using Amazing.Gateway;

namespace Amazing.Runtime
{
    public class Random : IRandom
    {
        public decimal RND(int p)
        {
            if (p == 0) return 1.0M / ((Math.Abs(Guid.NewGuid().GetHashCode()) / 100000000) +1);
            var possible = Enumerable.Range(1, p);
            return possible.OrderBy( x => Guid.NewGuid()).First();
        }
    }
}