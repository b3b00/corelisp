using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class NilLiteral : LispLiteral
    {
        
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => "nil";
        public override bool BooleanValue => false;
        
        public const string Nil = "nil";
        public override LispValueType Type => LispValueType.Nil;

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