using System;
using System.Linq;
using System.Threading;
using Dependency;

namespace Amazing
{
    public class MazeUserInterface
    {
        private static ITextInputOutput TextInputOutput => Shelf.RetrieveInstance<ITextInputOutput>();

        public static (int, int) GetDimensions()
        {
            var width = 0;
            var height = 0;

            while (width <= 1 || height <= 1)
            {
                TextInputOutput.CLS(64, 16);
                TextInputOutput.INPUT("WHAT ARE YOUR WIDTH AND LENGTH", out width, out height);
                if (width > 1 && height > 1) return (width, height);
                TextInputOutput.PRINT("MEANINGLESS DIMENSIONS. TRY AGAIN");
                Thread.Sleep(2000);
            }

            return (width, height);
        }

        public static void DisplayWelcome()
        {
            TextInputOutput.CLS(64, 16);
            TextInputOutput.PRINT(412, "AMAZING");
            TextInputOutput.PRINT(538, "COPYRIGHT BY");
            TextInputOutput.PRINT(587, "CREATIVE COMPUTING  MORRISTOWN, NEW JERSEY");

            Thread.Sleep(5000);
        }

        public static void DrawMaze(int[,] maze)
        {
            var width = maze.GetUpperBound(0);
            var height = maze.GetUpperBound(1);

            TextInputOutput.CLS(width * 3 + 2, height * 2 + 2);
            Console.WriteLine();

            foreach (var row in Enumerable.Range(0, height + 1))
            {
                if (row > 0)
                {
                    TextInputOutput.LPRINT_("I");
                    foreach (var column in Enumerable.Range(1, width))
                    {
                        TextInputOutput.LPRINT_(DrawWall(maze[column, row]));
                    }

                    TextInputOutput.LPRINT("");
                }

                foreach (var column in Enumerable.Range(1, width))
                {
                    TextInputOutput.LPRINT_(DrawBoxTop(maze[column, row]));
                }

                TextInputOutput.LPRINT(":");
            }
        }

        private static string DrawWall(int block) =>
            (block & 2) == 0 
                ? "  I" 
                : "   ";

        private static string DrawBoxTop(int block) =>
            (block & 1) == 0 
                ? ":--" 
                : ":  ";
    }
}