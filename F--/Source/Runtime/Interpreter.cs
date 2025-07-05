using FMM.Frontend.Ast;
using FMM.Helper.Error;
using FMM.Imports;

namespace FMM.Runtime
{
    class Interpreter
    {
        public RuntimeVal Evaluate(Stmt AstNode, Enviorment env) {

            switch (AstNode.kind) {
                case NodeType.NumericLiteral:
                    return new NumberVal((AstNode as NumericLiteral).value);

                case NodeType.Identifier:
                    return Evaluate_Identifier(AstNode as Identifier, env);

                case NodeType.VarDeclaration:
                    return Evaluate_Var_Declaration(AstNode as VarDeclaration, env);

                case NodeType.BinaryExpr:
                    return Evaluate_Binary_Expr(AstNode as BinaryExpr, env);

                case NodeType.Program:
                    return Evaluate_Program(AstNode as Program, env);
                default:
                    Error error = new Error(ErrorType.RuntimeError, $"This AST Node has not yet been set up for interpretation: {AstNode}");
                    return new NullVal();
            }
        }

        private RuntimeVal Evaluate_Var_Declaration(VarDeclaration declaration, Enviorment env)
        {
            RuntimeVal value = declaration.value != null
                ? Evaluate(declaration.value, env)
                : new NullVal();

            return env.DeclareVar(declaration.identifier, value, declaration.constant);
        }

        private NumberVal Evaluate_Numeric_Binary_Expr(NumberVal lhs, NumberVal rhs, String op) {
            float result;

            if (op == "+")
            {
                result = lhs.value + rhs.value;
            }
            else if (op == "-")
            {
                result = lhs.value - rhs.value;
            }
            else if (op == "*")
            {
                result = lhs.value * rhs.value;
            }
            else if (op == "/")
            {
                result = lhs.value / rhs.value;
            }
            else {
                result = lhs.value % rhs.value;
            }

            return new NumberVal(result);
        }

        private RuntimeVal Evaluate_Identifier(Identifier ident, Enviorment env) {
            RuntimeVal val = env.LookupVar(ident.symbol);
            return val;
        }

        private RuntimeVal Evaluate_Binary_Expr(BinaryExpr binop, Enviorment env) {
            var lhs = Evaluate(binop.Left, env);
            var rhs = Evaluate(binop.Right, env);

            if (lhs.type == ValueType.Number && rhs.type == ValueType.Number) {
                return Evaluate_Numeric_Binary_Expr(lhs as NumberVal, rhs as NumberVal, binop.Operator);
            }

            return new NullVal();
        }

        private RuntimeVal Evaluate_Program(Program program, Enviorment env) {
            RuntimeVal last_Evaluated = new NullVal();

            foreach (var statement in program.Body) {
                last_Evaluated = Evaluate(statement, env);
            }

            return last_Evaluated;
        }
    }
}
