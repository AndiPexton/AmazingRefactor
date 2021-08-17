using Amazing.Gateway;

namespace TestMazeBuilderWithMaxRandom
{
    public class FakeMaxRandom : IRandom
    {
        public decimal RND(int p) =>
            p == 0 
                ? 0.05M 
                : p;
    }
}