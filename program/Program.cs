using System;
using System.IO;
using core.lisp.lexer;
using core.lisp.interpreter;
using core.lisp.model;
using core.lisp.parser;
using sly.parser;
using sly.parser.generator;
using core.lisp;
using lispparser.core.lisp.lexer;
using sly.lexer;

namespace program
{
    class Program
    {
        private static CoreLisp coreLisp = new CoreLisp();

        
        static void TestProgram(string lisFileName)
        {
            string source = File.ReadAllText(lisFileName);
            var r = coreLisp.Run(source);
            Console.WriteLine(r.ToString());
        }


        static void testCustomLexer()
        {
            string src = @"un deux trois
quatre  cinq";
            LispCustomLexer lexer = new LispCustomLexer();
            var source = new ReadOnlyMemory<char>(src.ToCharArray());
            
            LexerPosition pos = new LexerPosition();
            var tok = lexer.GetCurrentToken(source,pos);
            bool ended = false;
            while (!ended && !tok.IsEOS)
            {
                var (e,p) = lexer.GoToNextToken(source, pos);
                tok = lexer.GetCurrentToken(source,pos);
                Console.WriteLine(tok);
                var (e1, p1) = lexer.GoToNextSpace(source, p); 
                ended = e1;
                pos = p1;
            }
        }
      


        static void Main(string[] args)
        {
            //TestProgram("lisp.lisp");
            testCustomLexer();
            //TestProgram("map.lisp");
        }
    }
}