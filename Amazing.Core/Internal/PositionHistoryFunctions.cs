using System.Linq;
using Shadow.Quack;

namespace Amazing.Core.Internal
{
    internal static class PositionHistoryFunctions
    {
        public static bool CurrentBlockIsNotVisited(this IMazeState state) => 
            !state.GetPositionHistoryVisited(state.Column, state.Row);

        public static bool BlockToLeftIsVisited(this IMazeState state) => 
            state.GetPositionHistoryVisited(state.Column - 1, state.Row);

        public static bool BlockAboveIsVisited(this IMazeState state) => 
            state.GetPositionHistoryVisited(state.Column, state.Row - 1);

        public static bool BlockToRightIsVisited(this IMazeState state) => 
            state.GetPositionHistoryVisited(state.Column + 1, state.Row);

        public static bool BlockBelowIsVisited(this IMazeState state) => 
            state.GetPositionHistoryVisited(state.Column, state.Row + 1);

        private static bool GetPositionHistoryVisited(this IMazeState state, int x, int y) => 
            state.GetPositionForHistory(x, y) != 0;

        public static bool AllBlocksVisited(this IMazeState state) => 
            state.BlocksVisited == state.Blocks;

        public static IMazeState SetCurrentVisited(this IMazeState state) =>
            Duck.Merge<IMazeState>(state, new
            {
                PositionHistory = state.GetNewHistory(state.Column, state.Row)
            });

        public static IMazeState SetVisited(this IMazeState state, int x, int y) =>
            Duck.Merge<IMazeState>(state, new
            {
                PositionHistory = state.GetNewHistory(x, y)
            });

        private static int[] GetNewHistory(this IMazeState state, int x, int y)
        {
            var newHistory = state.PositionHistory.ToArray();
            newHistory[state.CalculatePositionForHistory(x, y)] = state.BlocksVisited;
            return newHistory;
        }

        public static IMazeState IncBlocksVisited(this IMazeState mazeState) =>
            Duck.Merge<IMazeState>(mazeState, new
            {
                BlocksVisited = mazeState.BlocksVisited + 1
            });

        public static int GetPositionForHistory(this IMazeState state, int x, int y) => 
            state.PositionHistory.ToArray()[state.CalculatePositionForHistory(x, y)];

        private static int CalculatePositionForHistory(this IMazeState state, int x, int y) => 
            x - 1 + (y - 1) * state.Height;

        public static IMazeState SetBlockToLeftVisited(this IMazeState state) => 
            state
                .SetVisited(state.Column - 1, state.Row)
                .IncBlocksVisited();

        public static IMazeState SetBlockBelowVisited(this IMazeState state) => 
            state
                .SetVisited(state.Column, state.Row - 1)
                .IncBlocksVisited();

        public static IMazeState SetBlockToRightVisited(this IMazeState state) =>
            state
                .SetVisited(state.Column + 1, state.Row)
                .IncBlocksVisited();

        public static IMazeState SetBlockAboveVisited(this IMazeState state) =>
            state
                .SetVisited(state.Column, state.Row + 1)
                .IncBlocksVisited();
    }
}