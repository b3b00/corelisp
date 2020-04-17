using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class NilLiteral : LispLiteral
    {
        
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => Value.ToString();
        public override bool BooleanValue => false;
        
        public const string Nil = "nil";
        public override LispValueType Type => LispValueType.Nil;

        private Token<LispLexer> Token;

        public string Value { get; private set; }
        
        
        public static NilLiteral Instance = new NilLiteral(); 
        protected NilLiteral()
        {
            
        }

        public override string ToString()
        {
            return "nil";
        }
    }
}