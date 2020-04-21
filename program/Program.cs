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

       

        static void LispInLisp()
        {
            string source = File.ReadAllText("lisp.lisp");
            //source = "(atom 't)";
            var r = coreLisp.Run("(atom 'ttt)");
            ;
        }


        


        
        static void TestProgram()
        {
            

            coreLisp.Run(@"
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
            LispInLisp();
        }
    }
}