using System;
using System.IO;
using core.lisp.lexer;
using core.lisp.interpreter;
using core.lisp.model;
using core.lisp.parser;
using sly.parser;
using sly.parser.generator;
using core.lisp;

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

      


        static void Main(string[] args)
        {
            TestProgram("lisp.lisp");
           // TestProgram("factorial.lisp");
        }
    }
}