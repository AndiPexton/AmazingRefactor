namespace Amazing
{
    public interface IAnimationChangeOutput
    {
        void DrawFrame(int[,] maze, int column, int row);
    }
}