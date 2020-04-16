using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class AtomLiteral : LispLiteral
    {
        public override ListValueType Type => ListValueType.Atom;

        private Token<LispLexer> Token;

        public string Value { get; private set; }

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