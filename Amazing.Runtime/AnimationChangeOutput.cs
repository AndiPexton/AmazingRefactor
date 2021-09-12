using System;
using System.Threading;
using Dependency;

namespace Amazing.Runtime
{
    public class AnimationChangeOutput : IAnimationChangeOutput
    {
        private static ITextInputOutput TextInputOutput => Shelf.RetrieveInstance<ITextInputOutput>();
        private static IMazeUserInterface MazeUserInterface => Shelf.RetrieveInstance<IMazeUserInterface>();

        public void DrawFrame(int[,] maze, int column, int row)
        {
            if (!Console.IsOutputRedirected)
            {
                TextInputOutput.ForegroundColor = ConsoleColor.Cyan;
                MazeUserInterface.DrawMaze(maze);
                DrawActor(column, row);
                Thread.Sleep(10);
            }
        }

        private static void DrawActor(int column, int row)
        {
            TextInputOutput.SetCursorPosition(column * 4 - 1, row * 2);
            TextInputOutput.ForegroundColor = ConsoleColor.Red;
            TextInputOutput.BackgroundColor = ConsoleColor.White;
            TextInputOutput.Write("♥");
            TextInputOutput.ResetColor();
        }
    }
}