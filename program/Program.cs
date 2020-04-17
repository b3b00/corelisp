using System;
using core.lisp.lexer;
using LispInterpreter;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using sly.parser;
using sly.parser.generator;


namespace program
{
    class Program
    {
        private static Parser<LispLexer, ILisp> Parser = null;

        static void BuildParser()
        {
            ParserBuilder<LispLexer, ILisp> builder = new ParserBuilder<LispLexer, ILisp>();
            var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root",
                LexerExtension.AtomExtension);
            if (ParserResult.IsOk)
            {
                Parser = ParserResult.Result;
            }
        }


        static void Run(string source,bool debug = false)
        {
            Console.WriteLine();
            Console.WriteLine(source);
            var r = Parser.Parse(source);
            if (debug)
                Console.WriteLine(r.IsOk ? (r.Result as LispProgram).ToString() : "KO");
            if (r.IsOk)
            {
                var x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispProgram);
                Console.WriteLine("\t :> " + x?.ToString() + "<");
            }
            else
            {
                Console.WriteLine("parse error :");
                r.Errors.ForEach(x =>
                {
                    Console.WriteLine($"\t{x}");
                });
            }
        }


        static void TestInterpreteBasic()
        {
            Run("1");
            Run("2.1");
            Run("\"string\"");
            Run("'atom");
            Run("id");
            Run("()");
            Run("nil");
            Run("'nil");
        }

        static void TestInterpretFunctions()
        {
            Run("( + 1.0 2.0 )");

            Run("(( lambda (x) (+ x 11.0 )) 22.0)");


            Run(@"
{* this is Lisp comment *}
( ( lambda (x) (+ x 1 )) 2)");
            Run(@"
{* testing print primitive *}
( print ""hello core.lisp world"")");
        }


        static void TestList()
        {
            Run(@"(car (""a"" ""b"" ""c"" ))");
            Run(@"(cdr (""a"" ""b"" ""c"" ))");
            Run(@"(cons ""x"" (""a"" ""b"" ""c"" ))");
        }
        
        static void TestProgram()
        {
            Run(@"
(setq 'variable (""a"" ""b"" ""c"" ))
(car variable)
");
            Run(@"
(setq 'variable (""a"" ""b"" ""c"" ))
(cdr variable)
");
            Run(@"
(setq 'variable (""b"" ""c"" ))
(cons ""a"" variable)
");
            Run(@"
(setq 'variable (""b"" ""c"" ))
(setq 'first (lambda (x) (car x)))
(setq 'constructed (cons ""a"" variable))
(first constructed)
");
            
            Run(@"
(setq 'variable (1 2 3))
(setq addone (lambda (x) (* 183 x)))
(map addone variable)
");
            Run(@"
(setq test 't)
(if test ""vrai"" ""faux"")");
            
            Run(@"
(setq vname 'x )
(set vname ""vautour"")
(print ""X"") 
(print  x)
(print ""VNAME"")
(print vanme)
");

        }


        static void Main(string[] args)
        {
            BuildParser();
            //TestInterpretFunctions();
            // TestList();
            TestProgram();
        }
    }
}