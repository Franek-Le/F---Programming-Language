using FMM.Frontend.Lexer;
using FMM.Frontend.Ast;
using FMM.Frontend.Tokens;
using FMM.Helper.Error;
using FMM.Imports;

namespace FMM.Frontend.Parser
{
    public class Parser
    {
        private Lexer.Lexer lexer = new Lexer.Lexer();
        private List<Token> tokens = new List<Token>();

        private bool not_eof()
        {
            return tokens[0].Type != TokenType.EOF;
        }

        private Token at()
        {
            return tokens[0] as Token;
        }

        private Token eat()
        {
            var prev = tokens[0];
            tokens.RemoveAt(0);
            return prev;
        }

        private Token expect(TokenType type, String err)
        {
            Token prev = tokens[0];
            tokens.RemoveAt(0);
            if (prev.Type != type)
            {
                Error error = new Error(ErrorType.SyntaxError, $"{err} {prev.Type} - Expecting: {type}");
            }

            return prev;
        }

        public Program produceAST(string SourceCode)
        {
            tokens = lexer.Tokenize(SourceCode);

            Program program = new Program();
            program.Body = new List<Stmt>();

            while (not_eof())
            {
                program.Body.Add(Parse_Stmt());
            }
                
            return program;
        }

        private Stmt Parse_Stmt()
        {
            switch (at().Type)
            {
                case TokenType.Let:
                case TokenType.Const:
                    return Parse_Var_Declaration();
                default:
                    var expr = Parse_Expr();
                    expect(TokenType.Semicolon, "Missing semicolon at the end of line.");
                    return expr;
            }
        }

        private Stmt Parse_Var_Declaration() {
            bool isConstant = eat().Type == TokenType.Const;
            string identifier = expect(TokenType.Identifier, "Expected identifier name following let/const.").Value;

            if (at().Type == TokenType.Semicolon) {
                eat();
                if (isConstant) {
                    Error isConstantError = new Error(ErrorType.ParserError, $"Must assign value to constant variable '{identifier}'");
                    return new VarDeclaration(isConstant, identifier);
                }
                    
                return new VarDeclaration(false, identifier);
            }

            expect(TokenType.Equals, "Expected equals token following identifier in var declaration.");
            VarDeclaration declaration = new VarDeclaration(isConstant, identifier, Parse_Expr());

            expect(TokenType.Semicolon, "Variable declaration statement must end with a semicolon.");

            return declaration;
        }

        private Expr Parse_Expr()
        {
            return Parse_Additive_Expr();
        }

        private Expr Parse_Additive_Expr()
        {
            var left = Parse_Multiplicative_Expr();

            while (at().Value == "+" || at().Value == "-")
            {
                var op = eat().Value;
                var right = Parse_Multiplicative_Expr();
                left = new BinaryExpr(left, right, op);
            }

            return left;
        }

        private Expr Parse_Multiplicative_Expr()
        {
            var left = Parse_Primary_Expr();

            while (at().Value == "*" || at().Value == "/" || at().Value == "%")
            {
                var op = eat().Value;
                var right = Parse_Primary_Expr();
                left = new BinaryExpr(left, right, op);
            }

            return left;
        }

        private Expr Parse_Primary_Expr()
        {
            var tk = at().Type;

            switch (tk)
            {
                case TokenType.Identifier:
                    return new Identifier(eat().Value);

                case TokenType.Number:
                    return new NumericLiteral(float.Parse(eat().Value));

                case TokenType.OpenParen:
                    eat();
                    Expr value = Parse_Expr();
                    expect(TokenType.CloseParen, $"Unexpected token found inside parenthesised expression. Expected closing parenthesis {at().Type}");
                    return value;

                default:
                    Error error = new Error(ErrorType.ParserError, $"Unexpected token found while parsing! Unexpected token found: {at().Type}");
                    return new NumericLiteral(2137  );
            }
        }
    }
}

