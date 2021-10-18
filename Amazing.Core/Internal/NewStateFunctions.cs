using System.Collections.Generic;
using System.Linq;
using static Shadow.Quack.Duck;

namespace Amazing.Core.Internal
{
    internal static class NewStateFunctions
    {
        private const int WallRight = 2;
        private const int WallTop = 1;

        public static IMazeState SetBlockToLeftOpenRight(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Maze = state.GetNewMazeWithBlockOpenRightAt( state.Row, state.Column - 1)
            });

        public static IMazeState SetCurrentBlockOpenRight(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Maze = state.GetNewMazeWithBlockOpenRightAt(state.Row, state.Column)
            });

        public static IMazeState SetBlockOpenTop(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Maze = state.GetNewMazeWithBlockOpenTopAt(state.Row, state.Column)
            });

        public static IMazeState SetBlockAboveOpenTop(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Maze = state.GetNewMazeWithBlockOpenTopAt(state.Row - 1, state.Column)
            });

        private static IEnumerable<IEnumerable<int>> GetNewMazeWithBlockOpenTopAt(this IMazeState state, int stateRow, int stateColumn) => 
            NewMazeWithBlockAugmentedAt(state, stateRow, stateColumn, WallTop);

        private static IEnumerable<IEnumerable<int>> GetNewMazeWithBlockOpenRightAt(this IMazeState state, int stateRow, int stateColumn) => 
            NewMazeWithBlockAugmentedAt(state, stateRow, stateColumn, WallRight);

        private static IEnumerable<IEnumerable<int>> NewMazeWithBlockAugmentedAt(IMazeState state, int row, int column, int wall) =>
            RowsBefore(state, row)
                .Append(state.CurrentRow(row).CreateNewRow(column, wall))
                .Concat(state.RowsAfter(row))
                .ToArray();

        private static IEnumerable<IEnumerable<int>> RowsBefore(this IMazeState state, int row) =>
            state.Maze
                .Take(row)
                .ToArray();

        private static int[] CurrentRow(this IMazeState state, int row) =>
            state.Maze
                .Skip(row).First()
                .ToArray();

        private static IEnumerable<IEnumerable<int>> RowsAfter(this IMazeState state, int row) => 
            state.Maze
                .Skip(row + 1)
                .ToArray();

        private static IEnumerable<int> CreateNewRow(this int[] currentRow, int stateColumn, int wall) =>
            currentRow
                .Take(stateColumn - 1)
                .ToArray()
                .Append(currentRow.Skip(stateColumn - 1).First() | wall)
                .Concat(currentRow.Skip(stateColumn))
                .ToArray();
    }
}