namespace Amazing
{
    public interface IMazeUserInterface
    {
        (int, int) GetDimensions();
        void DisplayWelcome();
        void DrawMaze(int[,] maze);
    }
}