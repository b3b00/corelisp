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
            var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root");
            if (ParserResult.IsOk)
            {
                Parser = ParserResult.Result;
            }
        }


        static void Run(string source,bool debug = false)
        {
            Console.WriteLine();
            Console.WriteLine(source);
            
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
            TestProgram();
        }
    }
}