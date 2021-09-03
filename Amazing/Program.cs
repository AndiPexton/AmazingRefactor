using System;
using System.Threading;
using Amazing.Gateway;
using Amazing.Runtime;
using Dependency;

namespace Amazing
{
    class Program
    {
        static void Main(string[] args)
        {
            Shelf.ShelveInstance<IRandom>(new Runtime.Random());
            Shelf.ShelveInstance<ITextInputOutput>(new TextInputOutput());

            MazeUserInterface.DisplayWelcome();
            
            var (width, height) = MazeUserInterface.GetDimensions();

            var maze = Maze.BuildMaze(width, height);

            MazeUserInterface.DrawMaze(maze);

            while (!Console.KeyAvailable) Thread.Sleep(1);
        }
    }
}
