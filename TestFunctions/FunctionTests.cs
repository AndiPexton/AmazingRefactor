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
            var result = new Amazing.Runtime.Random().RND(bound);
            console.WriteLine($"{result}");
            Assert.InRange(result,1,bound);
        }

        [Fact]
        public void TestRndDecimal()
        {
            var result = new Amazing.Runtime.Random().RND(0);
            console.WriteLine($"{result}");
            Assert.InRange(result, 0, 1);
        }


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


        [Fact]
        public void TestBuildMaze()
        {
            Shelf.ShelveInstance<IRandom>(new FakeRandom());
            var maze = Basic.BuildMaze(5,5);
            var expectation = new int[,]
            {
                {0,0,0,0,0,0}, 
                {2,3,1,1,3,0}, 
                {2,3,1,2,3,0}, 
                {2,2,0,2,3,0}, 
                {2,2,0,0,3,0}, 
                {3,0,0,0,1,1}
            };

            maze.Should().BeEquivalentTo(expectation);
        }
    }

    public class FakeRandom : IRandom
    {
        public decimal RND(int p) =>
            p == 0 
                ? 0.05M 
                : p;
    }
}
