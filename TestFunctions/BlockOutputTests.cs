using Amazing;
using Xunit;

namespace TestFunctions
{
    public class BlockOutputTests
    {
        [Theory]
        [InlineData(":  ", 1)]
        [InlineData(":--", 2)]
        [InlineData(":  ", 3)]
        [InlineData(":--", 0)]
        public void TestBlockTop(string expected, int block)
        {
            var result = Basic.DrawBoxTop(block);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("  I", 1)]
        [InlineData("   ", 2)]
        [InlineData("   ", 3)]
        [InlineData("  I", 0)]
        public void TestBlockWall(string expected, int block)
        {
            var result = Basic.DrawWall(block);
            Assert.Equal(expected, result);
        }
    }
}