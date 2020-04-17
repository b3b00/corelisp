﻿using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class LispOperator : LispLiteral
    {

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