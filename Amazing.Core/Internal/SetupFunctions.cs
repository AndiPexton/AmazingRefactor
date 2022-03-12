using System.Collections.Generic;
using System.Linq;
using Amazing.Gateway;
using Dependency;
using Shadow.Quack;

namespace Amazing.Core.Internal
{
    internal static class SetupFunctions
    {
        private static IRandom Random => Shelf.RetrieveInstance<IRandom>();

        private static int GetRandomEntranceForWidth(int width) => 
            (int)Random.RND(width);

        public static IMazeState CreateStartMazeStateWithRandomEntrance(int width, int height) =>
            ImplementEmptyState(width, height)
                .DrawFrame()
                .SetFirstRowWithEntrance()
                .DrawFrame()
                .SetCurrentVisited()
                .IncBlocksVisited();

        private static IMazeState ImplementEmptyState(int width, int height) =>
            Duck.Implement<IMazeState>(
                new
                {
                    PositionHistory = new int[width * height],
                    Blocks = CalculateTotalBlocks(width, height),
                    ExitComplete = false,
                    Column = GetRandomEntranceForWidth(width),
                    Row = 1,
                    Width = width,
                    Height = height,
                    BlocksVisited = 1,
                    Maze = CreateEmptyMaze(width, height + 1)
                });

        private static IMazeState SetFirstRowWithEntrance(this IMazeState mazeState) =>
            mazeState.MergeWith(new
            {
                Maze = CreateMazeWithFirstRow(mazeState)
            });

        private static IEnumerable<IEnumerable<int>> CreateMazeWithFirstRow(this IMazeState mazeState) =>
            Enumerable
                .Empty<IEnumerable<int>>()
                .Append(mazeState.GenerateEntranceRow())
                .Concat(mazeState.Maze.Skip(1));

        private static IEnumerable<int> GenerateEntranceRow(this IMazeState mazeState) => 
            Enumerable
                .Range(1, mazeState.Width)
                .Select(block => block == mazeState.Column ? 3 : 2)
                .ToArray();

        private static IEnumerable<IEnumerable<int>> CreateEmptyMaze(int width, int height) => 
            Enumerable
                .Range(1, height)
                .Select(x => CreateEmptyRow(width))
                .ToArray();

        private static IEnumerable<int> CreateEmptyRow(int width) => 
            Enumerable
                .Repeat(0, width)
                .ToArray();

        private static int CalculateTotalBlocks(int stateWidth, int stateHeight) => 
            stateWidth * stateHeight + 1;
    }
}