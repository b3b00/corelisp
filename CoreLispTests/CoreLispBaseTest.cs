using core.lisp.lexer;
using coreLisp;
using LispInterpreter;
using lispparser.core.lisp.lexer;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using NUnit.Framework;
using sly.parser;
using sly.parser.generator;

namespace CoreLispTests
{
    public class CoreLispBaseTest
    {
        //public Parser<LispLexer, ILisp> Parser = null;
        private CoreLisp coreLisp = null;
        
        [SetUp]
        public void Setup()
        {
            coreLisp = new CoreLisp();
        }


        public LispLiteral Test(string source)
        {
            return coreLisp.Run(source);
        }
        
        
    }
}