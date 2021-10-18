using static Shadow.Quack.Duck;

namespace Amazing.Core.Internal
{
    internal static class MovementFunctions
    {
        public static IMazeState MoveRightOrMakeExitThenFindNextJunction(this IMazeState mazeState) =>
            mazeState.IsLastRow()
                ? mazeState
                    .SetExitComplete()
                    .SetBlockOpenTop()
                    .FindJunction()
                : mazeState
                    .SetBlockAboveVisited()
                    .SetBlockOpenTop()
                    .MoveRight();

        public static IMazeState OpenRightAndMoveDown(this IMazeState mazeState) =>
            mazeState
                .SetBlockToRightVisited()
                .SetCurrentBlockOpenRight()
                .MoveDown();

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

        public static IMazeState MoveDown(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Column = state.Column + 1
            }).DrawFrame();

        private static IMazeState MoveRight(this IMazeState state) =>
            Merge<IMazeState>(state, new
            {
                Row = state.Row + 1
            }).DrawFrame();

        public static IMazeState FindJunction(this IMazeState state)
        {
            do
            {
                if (state.IsNotEndOfRow())
                    state = state.MoveDown();
                else if (state.IsNotLastRow())
                    state = state.SetToBeginningOfNextRow();
                else
                    state = state.SetToBeginning();
            } while (state.CurrentBlockIsNotVisited());

            return state;
        }
    }
}