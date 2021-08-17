namespace Amazing
{
    public interface ITextInputOutput
    {
        void CLS(int width, int height);
        void PRINT(int pos, string text);
        void PRINT(string text);
        void LPRINT_(string text);
        void LPRINT(string text);
        void INPUT(string text, out int H, out int V);
    }
}