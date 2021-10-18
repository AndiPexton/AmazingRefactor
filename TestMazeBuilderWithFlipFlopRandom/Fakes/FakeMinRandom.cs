using Amazing.Gateway;

namespace TestMazeBuilder.Fakes
{
    public class FakeMinRandom : IRandom
    {
        public decimal RND(int p) =>
            p == 0 
                ? 0.00000001M 
                : 1;
    }
}