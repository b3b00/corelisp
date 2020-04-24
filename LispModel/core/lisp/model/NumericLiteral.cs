using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public abstract class NumericLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Double;
    }
}