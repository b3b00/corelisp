using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class StringLiteral : LispLiteral
    {
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => Value;
        public override bool BooleanValue => Value == "t";
        public override LispValueType Type => LispValueType.String;

        private Token<LispLexer> Token;

        public string Value => Token.StringWithoutQuotes;

        public StringLiteral(Token<LispLexer> token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return $"string>{Value}<";
        }
    }
}