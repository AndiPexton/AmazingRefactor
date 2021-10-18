using System;
using Amazing.Gateway;

namespace TestMazeBuilder.Fakes
{
    public class FakeSequenceRandom : IRandom
    {
        private readonly Random _state;
        public FakeSequenceRandom(int seed) =>
            _state = new Random(seed);

        public decimal RND(int p) =>
            p == 0
                ? (decimal)_state.NextDouble()
                : (_state.Next() % p) + 1;
    }
}