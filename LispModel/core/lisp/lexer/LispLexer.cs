using System;
using sly.lexer;
using sly.lexer.fsm;


namespace core.lisp.lexer
{
    

    public enum LispLexer
    {
        // 1 -> 10 : sugar
        [Lexeme(GenericToken.SugarToken, "(")] LPAREN = 1,
        [Lexeme(GenericToken.SugarToken, ")")] RPAREN = 2,

        [Lexeme(GenericToken.SugarToken, "+")] PLUS = 3,
        [Lexeme(GenericToken.SugarToken, "-")] MINUS = 4,
        [Lexeme(GenericToken.SugarToken, "/")] DIVIDE = 5,
        [Lexeme(GenericToken.SugarToken, "*")] TIMES = 6,
        [Lexeme(GenericToken.SugarToken, "'")] QUOTE = 7,


        // 11 -> 20 : literals
        [Lexeme(GenericToken.Double)] DOUBLE = 11,

        [Lexeme(GenericToken.Identifier,IdentifierType.Alpha)] SYMBOL = 12,

        [Lexeme(GenericToken.KeyWord,"lambda")]  LAMBDA = 13 ,

        [Lexeme(GenericToken.String)]  STRING = 14 ,
        
        [Lexeme(GenericToken.Int)]  INT = 16 ,
        
        [MultiLineComment("{*","*}")]
        COMMENT=100


    }
}
