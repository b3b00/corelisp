using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class IdentifierLiteral : LispLiteral
    {
        public override ListValueType Type => ListValueType.Identifier;

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