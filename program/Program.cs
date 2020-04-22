using System;
using System.IO;
using core.lisp.lexer;
using LispInterpreter;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using sly.parser;
using sly.parser.generator;
using coreLisp;

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
            //TestProgram("lisp.lisp");
            TestProgram("assoc.lisp");
        }
    }
}