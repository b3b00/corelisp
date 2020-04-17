using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class DoubleLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Double;

        private Token<LispLexer> Token;

        public double Value {get; private set; }


        public DoubleLiteral(double value)
        {
            Value = value;
        }
        public DoubleLiteral(Token<LispLexer> token)
        {
            Token = token;
            Value = token.DoubleValue;
        }
        
        public override string ToString()
        {
            return $"double>{Value}<";
        }
    }
}