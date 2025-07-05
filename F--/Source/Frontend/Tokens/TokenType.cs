namespace FMM.Frontend.Tokens
{
    public enum TokenType
    {   
        Number,
        Identifier,

        Let,
        Const,

        BinaryOperator,
        Equals,
        Semicolon,
        OpenParen,
        CloseParen,
        EOF,
    }

    public class Keywords
    {
        public static readonly Dictionary<string, TokenType> KEYWORDS = new Dictionary<string, TokenType>
        {
            { "let", TokenType.Let },
            { "const", TokenType.Const}
        };
    }
}
