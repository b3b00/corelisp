using System;
using core.lisp.lexer;
using sly.lexer;
using sly.lexer.fsm;

namespace core.lisp.lexer
{
    public class SymbolLexerExtension
    {
        public static void SymbolExtension(LispLexer token, LexemeAttribute lexem, GenericLexer<LispLexer> lexer) {
            if (token == LispLexer.SYMBOL) {

                // callback on end_date node 
                NodeCallback<GenericToken> callback = (FSMMatch<GenericToken> match) => 
                {
                    // this store the token id the the FSMMatch object to be later returned by GenericLexer.Tokenize 
                    match.Properties[GenericLexer<LispLexer>.DerivedToken] = LispLexer.SYMBOL;
                    return match;
                };
   	
                var fsmBuilder = lexer.FSMBuilder;

                var symbolCharExclusions = new char[]
                    {'(', ')', '|', '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.',' ','\'','\r','\n'};
                fsmBuilder.GoTo(GenericLexer<LispLexer>.start) 
                    .ExceptTransition(symbolCharExclusions)
                    .Mark("end_symbol") 
                    .ExceptTransitionTo(symbolCharExclusions,"end_symbol")
                    .End(GenericToken.Extension)  
                    .CallBack(callback);

                var graph = fsmBuilder.Fsm.ToGraphViz();
                ;
            }
        }
    }
}