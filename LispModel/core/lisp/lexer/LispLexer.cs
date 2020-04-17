using System;
using sly.lexer;
using sly.lexer.fsm;


namespace core.lisp.lexer
{
    public static class LexerExtension
    {
        public static void AtomExtension(LispLexer token, LexemeAttribute lexem, GenericLexer<LispLexer> lexer)
        {
            if (token == LispLexer.ATOM)
            {

                var fsmBuilder = lexer.FSMBuilder;
                
                // callback on end_atom node 
                NodeCallback<GenericToken> atomCallback = (FSMMatch<GenericToken> match) => 
                {
                    // this store the token id the the FSMMatch object to be later returned by GenericLexer.Tokenize 
                    match.Properties[GenericLexer<LispLexer>.DerivedToken] = LispLexer.ATOM;
                    return match;
                };

                fsmBuilder.GoTo(GenericLexer<LispLexer>.start) // start a in_double node
                    .Transition('\'') // add a transition on '.' with precondition
                    .Mark("start_atom") // set the node name
                    .RangeTransitionTo('a', 'z', "start_atom") // first year digit
                    .RangeTransitionTo('A', 'Z', "start_atom") // first year digit
                    .End(GenericToken.Extension)
                    .CallBack(atomCallback);
            }
        }
    }

    public enum LispLexer
    {
        // 1 -> 10 : sugar
        [Lexeme(GenericToken.SugarToken, "(")] LPAREN = 1,
        [Lexeme(GenericToken.SugarToken, ")")] RPAREN = 2,

        [Lexeme(GenericToken.SugarToken, "+")] PLUS = 3,
        [Lexeme(GenericToken.SugarToken, "-")] MINUS = 4,
        [Lexeme(GenericToken.SugarToken, "/")] DIVIDE = 5,
        [Lexeme(GenericToken.SugarToken, "*")] TIMES = 6,


        // 11 -> 20 : literals
        [Lexeme(GenericToken.Double)] DOUBLE = 11,

        [Lexeme(GenericToken.Identifier,IdentifierType.Alpha)] IDENTIFIER = 12,

        [Lexeme(GenericToken.KeyWord,"lambda")]  LAMBDA = 13 ,

        [Lexeme(GenericToken.String)]  STRING = 14 ,

        [Lexeme(GenericToken.Extension)] ATOM = 15,
        
        [Lexeme(GenericToken.Int)]  INT = 16 ,
        
        [MultiLineComment("{*","*}")]
        COMMENT=100


    }
}
