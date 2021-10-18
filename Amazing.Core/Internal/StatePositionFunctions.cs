namespace Amazing.Core.Internal
{
    internal static class StatePositionFunctions
    {
        public static bool IsFirstRow(this IMazeState state) => 
            state.Row == 1;

        public static bool IsEndOfRow(this IMazeState state) => 
            state.Column == state.Width;

        public static bool IsNotLastRow(this IMazeState state) => 
            state.Row != state.Height;

        public static bool IsStartOfRow(this IMazeState state) => 
            state.Column == 1;

        public static bool IsNotEndOfRow(this IMazeState state) => 
            state.Column != state.Width;

        public static bool IsLastRow(this IMazeState state) => 
            !state.IsNotLastRow();
    }
}