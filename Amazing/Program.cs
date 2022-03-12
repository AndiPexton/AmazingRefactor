using System;
using System.Threading;
using Amazing.Core;
using Amazing.Gateway;
using Amazing.Runtime;
using Dependency;

namespace Amazing
{
    class Program
    {
        private static IMazeUserInterface MazeUserInterface => Shelf.RetrieveInstance<IMazeUserInterface>();

        static void Main(string[] args)
        {
            Shelf.Clear();
            Shelf.ShelveInstance<IRandom>(new Runtime.Random());
            Shelf.ShelveInstance<ITextInputOutput>(new TextInputOutput());
            Shelf.ShelveInstance<IMazeUserInterface>(new MazeUserInterface());
            Shelf.ShelveInstance<IAnimationChangeOutput>(new AnimationChangeOutput());

            MazeUserInterface.DisplayWelcome();
            
            var (width, height) = MazeUserInterface.GetDimensions();

            var maze = Maze.BuildMaze(width, height);

            MazeUserInterface.DrawMaze(maze);

            while (!Console.KeyAvailable) Thread.Sleep(1);
        }
    }
}
