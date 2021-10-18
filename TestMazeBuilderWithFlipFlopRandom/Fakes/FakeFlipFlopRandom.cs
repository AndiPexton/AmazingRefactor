using Amazing.Gateway;

namespace TestMazeBuilder.Fakes
{
    public class FakeFlipFlopRandom : IRandom
    {
        private bool _state;
        public FakeFlipFlopRandom(bool state = true)
        {
            _state = state;
        }

        public decimal RND(int p) =>
            InvertAndReturnState()
                ? MinRnd(p)
                : MaxRnd(p);

        private bool InvertAndReturnState()
        {
            _state = !_state;
            return _state;
        }

        private static decimal MaxRnd(int p) =>
            p == 0
                ? 0.9M
                : p;

        private static decimal MinRnd(int p) =>
            p == 0
                ? 0.000000001M
                : 1;
    }
}