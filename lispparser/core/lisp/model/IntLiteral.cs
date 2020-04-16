using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class IntLiteral : LispLiteral
    {
        public override ListValueType Type => ListValueType.Int;

        private Token<LispLexer> Token;

        public int Value => Token.IntValue;

        public IntLiteral(Token<LispLexer> token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return $"int>{Value}<";
        }
    }
}