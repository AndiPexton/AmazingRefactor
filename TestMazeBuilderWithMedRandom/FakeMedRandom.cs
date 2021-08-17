using System;
using Amazing.Gateway;

namespace TestMazeBuilderWithMedRandom
{
    public class FakeMedRandom : IRandom
    {
        public decimal RND(int p) =>
            p == 0 
                ? 0.9M 
                : Math.Round(p / 2M, MidpointRounding.AwayFromZero);
    }
}