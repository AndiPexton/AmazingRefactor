using System.Collections;
using System.Collections.Generic;

namespace Amazing
{
    public interface IMazeState
    {
        IEnumerable<int> PositionHistory { get; }
        IEnumerable<IEnumerable<int>> Maze { get; }
        int Blocks { get; }
        bool ExitComplete { get; }
        int Entrance { get; }
        int Column { get; }
        int Row { get; }
        int Width { get; }
        int Height { get; }
        int BlocksVisited { get; }
    }
}