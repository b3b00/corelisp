using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public abstract class NumericLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Double;
    }
}