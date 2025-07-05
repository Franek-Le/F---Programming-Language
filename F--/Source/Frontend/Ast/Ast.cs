using FMM.Imports;

namespace FMM.Frontend.Ast
{
    public enum NodeType {
        Program,
        VarDeclaration,

        NumericLiteral,
        Identifier,
        BinaryExpr,
    }

    public interface Stmt {
        NodeType kind { get; }
    }

    public class Program : Stmt
    {
        public NodeType kind => NodeType.Program;
        public List<Stmt> Body { get; set; }
        public Program()
        {
            Body = new List<Stmt>();
        }
    }

    public class VarDeclaration : Stmt
    {
        public NodeType kind => NodeType.VarDeclaration;
        public bool constant { get; set; }
        public string identifier { get; set; }
        public Expr? value;

        public VarDeclaration(bool isConstant, string id, Expr? val = null)
        {
            constant = isConstant;
            identifier = id;
            value = val;
        }
    }

    public interface Expr : Stmt { }

    public class BinaryExpr : Expr
    {
        public NodeType kind => NodeType.BinaryExpr;
        public Expr Left { get; set; }
        public Expr Right { get; set; }
        public string Operator { get; set; }

        public BinaryExpr(Expr left, Expr right, string op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }
    }

    public class Identifier : Expr {
        public NodeType kind => NodeType.Identifier;
        public string symbol;

        public Identifier(string s) {
            symbol = s;
        }
    }

    public class NumericLiteral : Expr
    {
        public NodeType kind => NodeType.NumericLiteral;
        public float value;

        public NumericLiteral(float v)
        {
            value = v;
        }
    }
}
