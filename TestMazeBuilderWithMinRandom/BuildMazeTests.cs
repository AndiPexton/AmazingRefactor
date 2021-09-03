using Amazing;
using Amazing.Gateway;
using Dependency;
using FluentAssertions;
using TestMazeBuilderWithMinRandom;
using Xunit;

namespace TestMazeBuilderWithMaxRandom
{
    public class BuildMazeTests
    {
        [Fact]
        public void TestBuildMaze()
        {
            Shelf.ShelveInstance<IRandom>(new FakeMinRandom());
            var maze = Maze.BuildMaze(5,5);
            var expectation = new int[,]
            {
                {0,0,0,0,0,0}, 
                {3,2,3,2,1,0}, 
                {2,2,2,2,1,0}, 
                {2,2,2,2,3,0}, 
                {2,2,2,2,2,0}, 
                {2,1,0,1,0,0}
            };

            maze.Should().BeEquivalentTo(expectation);
        }
    }
}