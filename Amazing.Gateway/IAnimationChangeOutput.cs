namespace Amazing
{
    public interface IAnimationChangeOutput
    {
        void DrawFrame(int[,] maze, bool clear, int column, int row);
    }
}