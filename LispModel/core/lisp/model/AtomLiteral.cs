using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class AtomLiteral : LispLiteral
    {
        public override LispValueType Type => LispValueType.Atom;

        private Token<LispLexer> Token;
        
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => Value;
        public override bool BooleanValue => Value == "t";

        public string Value { get; private set; }

        public AtomLiteral(string value)
        {
            Value = value;
        }
        
        public AtomLiteral(Token<LispLexer> token)
        {
            Token = token;
            Value = Token.Value.Substring(1);
        }

        public override string ToString()
        {
            return $"atom>{Value}<";
        }
    }
}