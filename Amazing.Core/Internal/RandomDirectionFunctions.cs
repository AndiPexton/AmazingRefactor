using Amazing.Gateway;
using Dependency;

namespace Amazing.Core.Internal
{
    internal static class RandomDirectionFunctions
    {
        private static IRandom Random => Shelf.RetrieveInstance<IRandom>();

        public static NextAction DownOrRight() =>
            RandomOfTwo() switch
            {
                1 => NextAction.MoveDown,
                _ => NextAction.MoveRight
            };

        public static NextAction UpOrRight() =>
            RandomOfTwo() switch
            {
                1 => NextAction.MoveUp,
                _ => NextAction.MoveRight
            };

        public static NextAction UpOrDown() =>
            RandomOfTwo() switch
            {
                1 => NextAction.MoveUp,
                _ => NextAction.MoveDown
            };

        public static NextAction UpDownOrRight() =>
            RandomOfThree() switch
            {
                1 => NextAction.MoveUp,
                2 => NextAction.MoveDown,
                _ => NextAction.MoveRight
            };

        public static NextAction LeftOrRight() =>
            (int) (Random.RND(0) * 2 + 1) switch // TODO : RandomOfTwo()?
            {
                1 => NextAction.MoveLeft,
                _ => NextAction.MoveRight
            };

        public static NextAction LeftOrDown() =>
            RandomOfTwo() switch
            {
                1 => NextAction.MoveLeft,
                _ => NextAction.MoveDown
            };

        public static NextAction LeftDownOrRight() =>
            RandomOfThree() switch
            {
                1 => NextAction.MoveLeft,
                2 => NextAction.MoveDown,
                _ => NextAction.MoveRight
            };

        public static NextAction LeftUpOrRight() =>
            RandomOfThree() switch
            {
                1 => NextAction.MoveLeft,
                2 => NextAction.MoveUp,
                _ => NextAction.MoveRight
            };

        public static NextAction LeftOrUp() =>
            RandomOfTwo() switch
            {
                1 => NextAction.MoveLeft,
                _ => NextAction.MoveUp
            };

        public static NextAction LeftUpOrDown() =>
            RandomOfThree() switch
            {
                1 => NextAction.MoveLeft,
                2 => NextAction.MoveUp,
                _ => NextAction.MoveDown
            };

        private static int RandomOfTwo() => 
            (int) Random.RND(2);

        private static int RandomOfThree() => 
            (int) Random.RND(3);
    }
}