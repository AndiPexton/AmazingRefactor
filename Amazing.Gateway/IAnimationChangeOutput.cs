using System.Collections.Generic;

namespace Amazing
{
    public interface IAnimationChangeOutput
    {
        void DrawFrame(IEnumerable<IEnumerable<int>> maze, int column, int row);
    }
}