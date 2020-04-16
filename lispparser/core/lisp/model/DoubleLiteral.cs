using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class DoubleLiteral : LispLiteral
    {
        public override ListValueType Type => ListValueType.Double;

        private Token<LispLexer> Token;

        public double Value => Token.DoubleValue;

        public DoubleLiteral(Token<LispLexer> token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return $"double>{Value}<";
        }
    }
}