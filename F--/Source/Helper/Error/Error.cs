namespace FMM.Helper.Error
{
    enum ErrorType {
        LexerError = 1,
        ParserError = 2,
        SyntaxError = 3,
        RuntimeError = 4,
    }

    class Error
    {
        public Error(ErrorType type, String errorMsg) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{type.ToString()}: {errorMsg}");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit((int)type);
        }
    }
}
