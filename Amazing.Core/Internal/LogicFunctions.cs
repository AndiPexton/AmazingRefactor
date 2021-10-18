using System.Collections.Generic;
using Amazing.Gateway;
using static Amazing.Core.Internal.RandomDirectionFunctions;

namespace Amazing.Core.Internal
{
    internal static class LogicFunctions
    {
        public static IEnumerable<IEnumerable<int>> CreateRandomMaze(this IMazeState mazeState)
        {
            while (!mazeState.AllBlocksVisited())
                mazeState = mazeState.UpdateMazeState();

            return mazeState.Maze;
        }

        private static IMazeState UpdateMazeState(this IMazeState mazeState) =>
            mazeState.GetNextAction() switch
            {
                NextAction.MoveUp =>
                    mazeState.KnockThroughAndMoveUp(),
                NextAction.MoveRight =>
                    mazeState.MoveRightOrMakeExitThenFindNextJunction(), //TODO : is this Down or right?
                NextAction.MoveDown =>
                    mazeState.OpenRightAndMoveDown(), //TODO : is this right or down?
                NextAction.MoveLeft =>
                    mazeState.KnockThroughAndMoveLeft(),
                _ =>
                    mazeState.FindJunction()
            };
        
        private static NextAction GetNextAction(this IMazeState state) =>
            CanNotGoLeft(state) 
                ? state.ResolveUpRightDownOrSeek() 
                : state.ResolveUpDownRightOrLeft();

        private static NextAction ResolveUpRightDownOrSeek(this IMazeState state) =>
            state.CanNotGoUp() 
                ? state.ResolveRightSeekUpOrDown() 
                : state.ResolveDownUpOrRight();

        private static NextAction ResolveDownUpOrRight(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? ResolveUpOrRight(state) 
                : state.ResolveRightUpOrDown();

        private static NextAction ResolveUpDownRightOrLeft(this IMazeState state) =>
            state.CanNotGoUp()
                ? state.ResolveDownRightOrLeft()
                : state.ResolveDownLeftUpOrRight();

        private static NextAction ResolveDownRightOrLeft(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? state.ResolveRightOrLeft() 
                : state.ResolveRightLeftOrDown();

        private static NextAction ResolveRightOrLeft(this IMazeState state) =>
            state.CanNotMoveRight()
                ? NextAction.MoveLeft
                : LeftOrRight();

        private static NextAction ResolveDownLeftUpOrRight(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? state.ResolveLeftUpOrRight() 
                : LeftUpOrDown();

        private static NextAction ResolveRightLeftOrDown(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? LeftOrDown() 
                : LeftDownOrRight();

        private static NextAction ResolveLeftUpOrRight(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? LeftOrUp() 
                : LeftUpOrRight();

        private static NextAction ResolveRightUpOrDown(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? UpOrDown() 
                : UpDownOrRight();

        private static NextAction ResolveRightSeekUpOrDown(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? state.ResolveRightOrSeek() 
                : state.ResolveUpDownOrRight();

        private static NextAction ResolveUpDownOrRight(this IMazeState state) =>
            state.IsNotLastRow() 
                ? state.ResolveDownOrRight() 
                : state.ResolveDownOrUp();

        private static NextAction ResolveDownOrUp(this IMazeState state) =>
            state.ExitComplete 
                ? NextAction.MoveDown 
                : NextAction.MoveUp;

        private static NextAction ResolveDownOrRight(this IMazeState state) =>
            state.BlockBelowIsVisited() 
                ? NextAction.MoveDown 
                : DownOrRight();

        private static NextAction ResolveUpOrRight(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? NextAction.MoveUp 
                : UpOrRight();

        private static NextAction ResolveRightOrSeek(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? NextAction.FindJunction 
                : NextAction.MoveRight;

        private static bool CanNotGoUp(this IMazeState state) => 
            state.IsFirstRow() 
            || state.BlockAboveIsVisited();

        private static bool CanNotGoLeft(this IMazeState state) => 
            state.IsStartOfRow() 
            || state.BlockToLeftIsVisited();

        private static bool CanNotMoveDown(this IMazeState state) =>
            state.IsEndOfRow() 
            || state.BlockToRightIsVisited();

        private static bool CanNotMoveRight(this IMazeState state) =>
            state.IsNotLastRow() && state.BlockBelowIsVisited() 
            || !state.IsNotLastRow() && state.ExitComplete;
    }
}