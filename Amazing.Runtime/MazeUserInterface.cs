using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dependency;

namespace Amazing.Runtime
{
    public class MazeUserInterface : IMazeUserInterface
    {
        private static ITextInputOutput TextInputOutput => Shelf.RetrieveInstance<ITextInputOutput>();

        public  (int, int) GetDimensions()
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

        public  void DisplayWelcome()
        {
            TextInputOutput.CLS(64, 16);
            TextInputOutput.PRINT(412, "AMAZING");
            TextInputOutput.PRINT(538, "COPYRIGHT BY");
            TextInputOutput.PRINT(587, "CREATIVE COMPUTING  MORRISTOWN, NEW JERSEY");

            Thread.Sleep(5000);
        }

        public void DrawMaze(IEnumerable<IEnumerable<int>> maze)
        {
            SetWindow(maze);

            NewLine();
            maze.Aggregate(true, DrawRow);

            TextInputOutput.CursorVisible = true;
        }

        private static void SetWindow(IEnumerable<IEnumerable<int>> maze)
        {
            var width = maze.First().Count();
            var height = maze.Count();

            TextInputOutput.SetWindow(width * 4 + 4, height * 2 + 3);
            TextInputOutput.SetCursorPosition(0, 0);
            TextInputOutput.CursorVisible = false;
        }

        private bool DrawRow(bool first, IEnumerable<int> row)
        {
            IfNotFirstDrawOuterSidesNewLine(first, row);
            DrawTopsOuterNewLine(row);
            return false;
        }

        private void IfNotFirstDrawOuterSidesNewLine(bool first, IEnumerable<int> row)
        {
            if (first) return;
            OuterWall();
            DrawSides(row);
            NewLine();
        }

        private void DrawTopsOuterNewLine(IEnumerable<int> row)
        {
            DrawTops(row);
            OuterWall();
            NewLine();
        }

        private void DrawTops(IEnumerable<int> row)
        {
            foreach (var block in row) 
                TextInputOutput.LPRINT_(ResolveTopValue(block));
        }

        private void DrawSides(IEnumerable<int> row)
        {
            foreach (var block in row)
                TextInputOutput.LPRINT_(ResolveSideValue(block));
        }

        private static void NewLine()
        {
            TextInputOutput.LPRINT("");
        }

        private static void OuterWall()
        {
            TextInputOutput.LPRINT_("██");
        }

        private  string ResolveSideValue(int block) =>
            (block & 2) == 0 
                ? "  ██"
                : "    ";

        private  string ResolveTopValue(int block) =>
            (block & 1) == 0 
                ? "████"
                : "██  ";
    }
}