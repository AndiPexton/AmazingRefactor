using Amazing.Gateway;
using Dependency;

namespace Amazing.Core.Internal
{
    internal static class AnimationFunctions
    {
        private static IAnimationChangeOutput AnimationChangeOutput => Shelf.RetrieveInstance<IAnimationChangeOutput>();

        public static IMazeState DrawFrame(this IMazeState state)
        {
            AnimationChangeOutput?.DrawFrame(state.Maze, state.Column, state.Row);
            return state;
        }
    }
}