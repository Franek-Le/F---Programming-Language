using FMM.Frontend.Tokens;
using FMM.Helper.Error;
using FMM.Imports;


namespace FMM.Frontend.Lexer
{
    public class Lexer
    {
        public List<Token> Tokenize(string sourceCode)
        {
            var tokens = new List<Token>();
            var src = sourceCode.ToCharArray().Select(c => c.ToString()).ToList();

            while (src.Count > 0) {
                if (src[0] == "(")
                {
                    tokens.Add(token(src[0], TokenType.OpenParen));
                    src.RemoveAt(0);
                }
                else if (src[0] == ")")
                {
                    tokens.Add(token(src[0], TokenType.CloseParen));
                    src.RemoveAt(0);
                }
                else if (src[0] == "+" || src[0] == "-" || src[0] == "*" || src[0] == "/" || src[0] == "%")
                {
                    tokens.Add(token(src[0], TokenType.BinaryOperator));
                    src.RemoveAt(0);
                }
                else if (src[0] == "=")
                {
                    tokens.Add(token(src[0], TokenType.Equals));
                    src.RemoveAt(0);
                }
                else if (src[0] == ";") {
                    tokens.Add(token(src[0], TokenType.Semicolon));
                    src.RemoveAt(0);
                }
                else
                {
                    if (isInt(src[0]))
                    {
                        String num = "";
                        while (src.Count > 0 && isInt(src[0]))
                        {
                            num += src[0];
                            src.RemoveAt(0);
                        }

                        tokens.Add(token(num, TokenType.Number));
                    }
                    else if (isAlpha(src[0]))
                    {
                        String ident = "";
                        while (src.Count > 0 && isAlpha(src[0]))
                        {
                            ident += src[0];
                            src.RemoveAt(0);
                        }

                        ;
                        if (Keywords.KEYWORDS.TryGetValue(ident, out TokenType reserved))
                        {
                            tokens.Add(token(ident, reserved));
                        }
                        else
                        {
                            tokens.Add(token(ident, TokenType.Identifier));
                        }
                    }
                    else if (isSkippable(src[0]))
                    {
                        src.RemoveAt(0);
                    }
                    else
                    {
                        Error e = new Error(ErrorType.LexerError, "Unrecognized character!");
                    }
                }
            }

            tokens.Add(token("EndOfFile", TokenType.EOF));

            return tokens;
        }

        Token token(string value = "", TokenType type = TokenType.Identifier)
        {
            return new Token {Value=value, Type=type };
        }

        public static bool isAlpha(string str) {
            return str.ToUpper() != str.ToLower();        
        }
        public static bool isInt(string str)
        {
            char c = str[0];
            return c >= '0' && c <= '9';
        }

        public static bool isSkippable(string str) {
            return str == " " || str == "\t" || str == "\n";    
        }
    }
}
