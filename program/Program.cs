using System;
using core.lisp.lexer;
using LispInterpreter;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using sly.lexer;
using sly.parser.generator;


namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            // var source = "(* comment *)( lambda (x) (write x 'ato))";
            // var lexerResult = LexerBuilder.BuildLexer<LispLexer>(LexerExtension.AtomExtension);
            //
            // if (lexerResult.IsOk)
            // {
            //     var graph = (lexerResult.Result as GenericLexer<LispLexer>).FSMBuilder.Fsm.ToGraphViz();
            //     var tokenization = lexerResult.Result.Tokenize(source);
            //     if (tokenization.IsOk)
            //     {
            //         foreach (var token in tokenization.Tokens)
            //         {
            //             Console.WriteLine(token);
            //         }
            //     }
            // }
            
            ParserBuilder<LispLexer,LispLiteral> builder = new ParserBuilder<LispLexer, LispLiteral>();
            var parserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT,"literal",LexerExtension.AtomExtension);
            if (parserResult.IsOk)
            {
                var parser = parserResult.Result;
                var r = parser.Parse("1");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                r = parser.Parse("2.1");
                var x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                r = parser.Parse("\"string\"");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("'atom");
                LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("id");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);                
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("( + 1.0 2.0 )");
                
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("(( lambda (x) (+ x 11.0 )) 22.0)");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("()");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse("nil");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                
                r = parser.Parse("'nil");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse(@"
(* this is Lisp comment *)
( ( lambda (x) (+ x 1 )) 2)");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
                r = parser.Parse(@"
(* testing print primitive *)
( print ""hello core.lisp world"")");
                Console.WriteLine(r.IsOk ? r.Result.ToString() : "KO");
                x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispLiteral);
                Console.WriteLine("=> "+x?.ToString());
                
            }
            
        }
    }
}
