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
                NextAction.MoveDown =>
                    mazeState.MoveDownOrMakeExitThenFindNextJunction(), 
                NextAction.MoveRight =>
                    mazeState.OpenRightAndMoveRight(), 
                NextAction.MoveLeft =>
                    mazeState.KnockThroughAndMoveLeft(),
                _ =>
                    mazeState.FindJunction()
            };
        
        private static NextAction GetNextAction(this IMazeState state) =>
            CanNotGoLeft(state) 
                ? state.ResolveUpDownSeekOrRight() 
                : state.ResolveUpDownRightOrLeft();

        private static NextAction ResolveUpDownSeekOrRight(this IMazeState state) =>
            state.CanNotGoUp() 
                ? state.ResolveDownSeekUpOrRight() 
                : state.ResolveDownUpOrRight();

        private static NextAction ResolveDownUpOrRight(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? ResolveUpOrDown(state) 
                : state.ResolveRightUpOrDown();

        private static NextAction ResolveUpDownRightOrLeft(this IMazeState state) =>
            state.CanNotGoUp()
                ? state.ResolveDownRightOrLeft()
                : state.ResolveDownLeftUpOrRight();

        private static NextAction ResolveDownRightOrLeft(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? state.ResolveDownOrLeft() 
                : state.ResolveRightLeftOrDown();

        private static NextAction ResolveDownOrLeft(this IMazeState state) =>
            state.CanNotMoveDown()
                ? NextAction.MoveLeft
                : LeftOrDown();

        private static NextAction ResolveDownLeftUpOrRight(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? state.ResolveLeftUpOrDown() 
                : LeftUpOrRight();

        private static NextAction ResolveRightLeftOrDown(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? LeftOrRight() 
                : LeftRightOrDown();

        private static NextAction ResolveLeftUpOrDown(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? LeftOrUp() 
                : LeftUpOrDown();

        private static NextAction ResolveRightUpOrDown(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? UpOrRight() 
                : UpRightOrDown();

        private static NextAction ResolveDownSeekUpOrRight(this IMazeState state) =>
            state.CanNotMoveRight() 
                ? state.ResolveDownOrSeek() 
                : state.ResolveUpDownOrRight();

        private static NextAction ResolveUpDownOrRight(this IMazeState state) =>
            state.IsNotLastRow() 
                ? state.ResolveRightOrDown() 
                : state.ResolveRightOrUp();

        private static NextAction ResolveRightOrUp(this IMazeState state) =>
            state.ExitComplete 
                ? NextAction.MoveRight 
                : NextAction.MoveUp;

        private static NextAction ResolveRightOrDown(this IMazeState state) =>
            state.BlockBelowIsVisited() 
                ? NextAction.MoveRight 
                : RightOrDown();

        private static NextAction ResolveUpOrDown(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? NextAction.MoveUp 
                : UpOrDown();

        private static NextAction ResolveDownOrSeek(this IMazeState state) =>
            state.CanNotMoveDown() 
                ? NextAction.FindJunction 
                : NextAction.MoveDown;

        private static bool CanNotGoUp(this IMazeState state) => 
            state.IsFirstRow() 
            || state.BlockAboveIsVisited();

        private static bool CanNotGoLeft(this IMazeState state) => 
            state.IsStartOfRow() 
            || state.BlockToLeftIsVisited();

        private static bool CanNotMoveRight(this IMazeState state) =>
            state.IsEndOfRow() 
            || state.BlockToRightIsVisited();

        private static bool CanNotMoveDown(this IMazeState state) =>
            state.IsNotLastRow() && state.BlockBelowIsVisited() 
            || !state.IsNotLastRow() && state.ExitComplete;
    }
}