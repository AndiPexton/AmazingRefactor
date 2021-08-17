using Amazing.Gateway;

namespace TestMazeBuilderWithMinRandom
{
    public class FakeMinRandom : IRandom
    {
        public decimal RND(int p) =>
            p == 0 
                ? 0.00000001M 
                : 1;
    }
}