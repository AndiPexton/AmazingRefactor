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

            Basic.DisplayWelcome();

            var (width, height) = Basic.GetDimensions();

            var maze = Basic.BuildMaze(width, height);

            Basic.DrawMaze(maze);

            while (!Console.KeyAvailable) Thread.Sleep(1);
        }
    }
}
