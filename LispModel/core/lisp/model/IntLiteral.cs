using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class IntLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Int;
        public override double DoubleValue => (double) Value;
        public override int IntValue => Value;
        public override string StringValue => Value.ToString();
        public override bool BooleanValue => Value != 0;

        private Token<LispLexer> Token;

        public int Value { get; set; }


        public IntLiteral(int value)
        {
            Value = value;
        }
        public IntLiteral(Token<LispLexer> token)
        {
            Token = token;
            Value = token.IntValue;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}