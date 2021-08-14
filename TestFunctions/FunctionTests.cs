using System;
using Amazing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;
using Xunit.Abstractions;

namespace TestFunctions
{
    public class FunctionTests
    {
        private readonly ITestOutputHelper console;

        public FunctionTests(ITestOutputHelper output)
        {
            console = output;
        }

        [Theory]
        [InlineData(25)]
        [InlineData(4)]
        [InlineData(123)]
        [InlineData(6)]
        [InlineData(11)]
        [InlineData(100)]
        [InlineData(10)]
        public void TestRndIntegerRange(int bound)
        {
            var result = Basic.RND(bound);
            console.WriteLine($"{result}");
            Assert.InRange(result,1,bound);
        }

        [Fact]
        public void TestRndDecimal()
        {
            var result = Basic.RND(0);
            console.WriteLine($"{result}");
            Assert.InRange(result, 0, 1);
        }


    }
}
