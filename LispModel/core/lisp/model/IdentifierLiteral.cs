using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class IdentifierLiteral : LispLiteral
    {
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => Value.ToString();
        public override bool BooleanValue => false;
        public override LispValueType Type => LispValueType.Identifier;

        private Token<LispLexer> Token;

        public string Value => Token.Value;

        public IdentifierLiteral(Token<LispLexer> token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return $"id>{Value}<";
        }
    }
}