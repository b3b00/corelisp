using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class StringLiteral : LispLiteral
    {
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