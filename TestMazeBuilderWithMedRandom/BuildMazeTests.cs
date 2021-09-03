using Amazing;
using Amazing.Gateway;
using Dependency;
using FluentAssertions;
using Xunit;

namespace TestMazeBuilderWithMedRandom
{
    public class BuildMazeTests
    {
        [Fact]
        public void TestBuildMaze()
        {
            Shelf.ShelveInstance<IRandom>(new FakeMedRandom());
            var maze = Maze.BuildMaze(5,5);
            var expectation = new int[,]
            {
                {0,0,0,0,0,0}, 
                {2,3,1,1,1,2}, 
                {2,1,1,2,2,2}, 
                {3,3,2,1,0,2}, 
                {2,2,1,1,1,0}, 
                {2,1,1,1,1,1}
            };

            maze.Should().BeEquivalentTo(expectation);
        }
    }
}