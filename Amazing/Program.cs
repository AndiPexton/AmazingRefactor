using System;
using System.Threading;
using Amazing.Gateway;
using Dependency;

namespace Amazing
{
    class Program
    {
        static void Main(string[] args)
        {
            Shelf.ShelveInstance<IRandom>(new Runtime.Random());
            Shelf.ShelveInstance<ITextInputOutput>(new Runtime.TextInputOutput());

            MazeUserInterface.DisplayWelcome();

            var (width, height) = MazeUserInterface.GetDimensions();

            var maze = Basic.BuildMaze(width, height);

            MazeUserInterface.DrawMaze(maze);

            while (!Console.KeyAvailable) Thread.Sleep(1);
        }
    }
}
