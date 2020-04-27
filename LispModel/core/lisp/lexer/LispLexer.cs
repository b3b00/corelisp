using System;
using sly.lexer;
using sly.lexer.fsm;


namespace core.lisp.lexer
{
    

    public enum LispLexer
    {
        EOS = 0,
        
        // 1 -> 10 : sugar
        [Lexeme(GenericToken.SugarToken, "(")] LPAREN = 1,
        [Lexeme(GenericToken.SugarToken, ")")] RPAREN = 2,

       
        [Lexeme(GenericToken.SugarToken, "'")] QUOTE = 7,


        // 11 -> 20 : literals
        [Lexeme(GenericToken.Double)] DOUBLE = 11,

        //[Lexeme(GenericToken.Identifier,IdentifierType.Alpha)] 
        [Lexeme(GenericToken.Extension)]SYMBOL = 12,

        // [Lexeme(GenericToken.KeyWord,"lambda")]  LAMBDA = 13 ,

        [Lexeme(GenericToken.String)]  STRING = 14 ,
        
        [Lexeme(GenericToken.Int)]  INT = 16 ,
        
        [SingleLineComment("#")]
        COMMENT=100


    }
}
