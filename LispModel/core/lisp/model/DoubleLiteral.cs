using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class DoubleLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Double;

        private Token<LispLexer> Token;
        
        public override double DoubleValue => (double) Value;
        public override int IntValue => (int)Value;
        public override string StringValue => Value.ToString();
        public override bool BooleanValue => Value != 0;

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