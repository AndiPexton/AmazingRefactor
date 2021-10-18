using System.Collections.Generic;

namespace Amazing
{
    public interface IMazeUserInterface
    {
        (int, int) GetDimensions();
        void DisplayWelcome();
        void DrawMaze(IEnumerable<IEnumerable<int>> maze);
    }
}