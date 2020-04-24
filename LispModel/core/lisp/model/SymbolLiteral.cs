using System.Collections.Generic;
using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class SymbolLiteral : LispLiteral
    {
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => Value.ToString();
        public override bool BooleanValue => Value == "t";
        public override LispValueType Type => LispValueType.Symbol;

        private Token<LispLexer> Token;
        
        public static SymbolLiteral True = new SymbolLiteral("t");

        public string Value {get; set; }

        public SymbolLiteral(Token<LispLexer> token)
        {
            Token = token;
            Value = Token.Value;
        }
        
        public SymbolLiteral(string value) {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}