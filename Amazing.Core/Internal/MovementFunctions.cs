using Amazing.Gateway;
using static Shadow.Quack.Duck;

namespace Amazing.Core.Internal
{
    internal static class MovementFunctions
    {
        public static IMazeState MoveDownOrMakeExitThenFindNextJunction(this IMazeState mazeState) =>
            mazeState.IsLastRow()
                ? mazeState
                    .SetExitComplete()
                    .SetBlockOpenTop()
                    .FindJunction()
                : mazeState
                    .SetBlockAboveVisited()
                    .SetBlockOpenTop()
                    .MoveDown();

        public static IMazeState OpenRightAndMoveRight(this IMazeState mazeState) =>
            mazeState
                .SetBlockToRightVisited()
                .SetCurrentBlockOpenRight()
                .MoveRight();

        public static IMazeState KnockThroughAndMoveLeft(this IMazeState state) => 
            state
                .SetBlockToLeftVisited()
                .SetBlockToLeftOpenRight()
                .MoveLeft();

        public static IMazeState KnockThroughAndMoveUp(this IMazeState state) => 
            state
                .SetBlockBelowVisited()
                .SetBlockAboveOpenTop()
                .MoveUp();

        public static IMazeState SetToBeginningOfNextRow(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Column = 1,
                Row = state.Row + 1
            }).DrawFrame();

        public static IMazeState SetToBeginning(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Column = 1,
                Row = 1
            }).DrawFrame();

        public static IMazeState SetExitComplete(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                ExitComplete = true
            });

        private static IMazeState MoveLeft(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Column = state.Column - 1
            }).DrawFrame();

        private static IMazeState MoveUp(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Row = state.Row - 1
            }).DrawFrame();

        public static IMazeState MoveRight(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Column = state.Column + 1
            }).DrawFrame();

        private static IMazeState MoveDown(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Row = state.Row + 1
            }).DrawFrame();

        public static IMazeState FindJunction(this IMazeState state)
        {
            do
            {
                state = state.SeekNext();
            } while (state.CurrentBlockIsNotVisited());

            return state;
        }

        private static IMazeState SeekNext(this IMazeState state) =>
            state.IsEndOfRow() 
                ? MoveToStartOfNextLogicalRow(state)
                : state.MoveRight();

        private static IMazeState MoveToStartOfNextLogicalRow(IMazeState state) =>
            state.IsLastRow() 
                ? state.SetToBeginning() 
                : state.SetToBeginningOfNextRow();
    }
}