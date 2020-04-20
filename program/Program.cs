using System;
using System.IO;
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
            var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root");
            if (ParserResult.IsOk)
            {
                Parser = ParserResult.Result;
            }
        }

        static void LispInLisp()
        {
            string source = File.ReadAllText("lisp.lisp");
            BuildParser();
            var r = Run(source);
            ;
        }


        static LispLiteral Run(string source,bool debug = false)
        {
            var r = Parser.Parse(source);
            if (r.IsError)
            {
                r.Errors.ForEach(e => Console.WriteLine(e.ErrorMessage));
                return null;
            }
            var x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispProgram);
            return x;
        }


        
        static void TestProgram()
        {
            
            
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
            LispInLisp();
        }
    }
}