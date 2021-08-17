using Amazing;
using Amazing.Gateway;
using Dependency;
using FluentAssertions;
using Xunit;

namespace TestMazeBuilderWithMaxRandom
{
    public class BuildMazeTests
    {
        [Fact]
        public void TestBuildMaze()
        {
            Shelf.ShelveInstance<IRandom>(new FakeMaxRandom());
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
}