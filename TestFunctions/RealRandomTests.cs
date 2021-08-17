using System;
using Amazing;
using Amazing.Gateway;
using Dependency;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Xunit;
using Xunit.Abstractions;

namespace TestFunctions
{
    public class RealRandomTests
    {
        private readonly ITestOutputHelper _console;
        
        public RealRandomTests(ITestOutputHelper output)
        {
            _console = output;
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
            var result = new Amazing.Runtime.Random().RND(bound);
            _console.WriteLine($"{result}");
            Assert.InRange(result,1,bound);
        }

        [Fact]
        public void TestRndDecimal()
        {
            var result = new Amazing.Runtime.Random().RND(0);
            _console.WriteLine($"{result}");
            Assert.InRange(result, 0, 1);
        }
    }
}
