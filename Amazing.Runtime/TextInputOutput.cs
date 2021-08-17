using System;

namespace Amazing.Runtime
{
    public class TextInputOutput : ITextInputOutput
    {
        public  void CLS(int width, int height)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.Clear();
        }

        public  void PRINT(int pos, string text)
        {
            var y = pos / 64;
            var x = pos - (y * 64);

            Console.SetCursorPosition(x, y);

            Console.Write(text);
        }

        public  void LPRINT_(string text)
        {
            Console.Write(text);
        }

        public  void LPRINT(string text)
        {
            Console.WriteLine(text);
        }

        public  void PRINT(string text)
        {
            Console.Write(text);
        }

        public  void INPUT(string text, out int H, out int V)
        {
            Console.WriteLine(text);
            Console.Write("> ");
            var h = Console.ReadLine();
            Console.Write("> ");
            var v = Console.ReadLine();

            if (!int.TryParse(h, out H))
                H = 1;

            if (!int.TryParse(v, out V))
                V = 1;
        }
    }
}