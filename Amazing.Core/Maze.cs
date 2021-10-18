using System.Collections.Generic;
using Amazing.Core.Internal;

namespace Amazing.Core
{
    public class Maze
    {
        public static IEnumerable<IEnumerable<int>> BuildMaze(int width, int height) =>
            SetupFunctions
                .CreateStartMazeStateWithRandomEntrance(width, height)
                .CreateRandomMaze();
    }
}
