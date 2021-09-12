using System;

namespace Amazing.Runtime
{
    public class TextInputOutput : ITextInputOutput
    {
        public  void CLS(int width, int height)
        {
            SetCursorPosition(0, 0);
            SetWindow(width, height);
            Console.Clear();
        }

        public void SetWindow(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public  void PRINT(int pos, string text)
        {
            var y = pos / 64;
            var x = pos - (y * 64);

            SetCursorPosition(x, y);

            Write(text);
        }

        public  void LPRINT_(string text)
        {
            Write(text);
        }

        public  void LPRINT(string text)
        {
            Console.WriteLine(text);
        }

        public  void PRINT(string text)
        {
            Write(text);
        }

        public  void INPUT(string text, out int H, out int V)
        {
            Console.WriteLine(text);
            Write("> ");
            var h = Console.ReadLine();
            Write("> ");
            var v = Console.ReadLine();

            if (!int.TryParse(h, out H))
                H = 1;

            if (!int.TryParse(v, out V))
                V = 1;
        }

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);
        public bool CursorVisible { get => Console.CursorVisible; set => Console.CursorVisible = value; } 
        public ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
        public ConsoleColor BackgroundColor { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }
        public void Write(string s) => Console.Write(s);
        public void ResetColor() => Console.ResetColor();
    }
}