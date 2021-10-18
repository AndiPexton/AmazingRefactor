using System.Collections.Generic;

namespace Amazing.Gateway
{
    public interface IMazeState
    {
        IEnumerable<int> PositionHistory { get; }
        IEnumerable<IEnumerable<int>> Maze { get; }
        int Blocks { get; }
        bool ExitComplete { get; }
        int Column { get; }
        int Row { get; }
        int Width { get; }
        int Height { get; }
        int BlocksVisited { get; }
    }
}