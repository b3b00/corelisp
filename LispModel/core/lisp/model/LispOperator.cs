using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class LispOperator : LispLiteral
    {
        
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => null;
        public override bool BooleanValue => false;

        private Token<LispLexer> Token;

        public LispLexer Operator => Token.TokenID;

        public string Value => Token.Value;

        public LispOperator(Token<LispLexer> token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return $"operator>{Operator}<";
        }
    }
}