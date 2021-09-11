using System;
using System.Threading;
using Dependency;

namespace Amazing
{
    public class AnimationChangeOutput : IAnimationChangeOutput
    {
        private static ITextInputOutput TextInputOutput => Shelf.RetrieveInstance<ITextInputOutput>();

        public void DrawFrame(int[,] maze, bool clear, int column, int row)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                MazeUserInterface.DrawMaze(maze, clear);
                clear = false;
                Console.SetCursorPosition(column * 4 - 1, row * 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("♥");
                Console.ResetColor();
                Console.CursorVisible = true;
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(100);
            }
        }
    }
}