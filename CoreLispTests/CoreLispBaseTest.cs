using core.lisp.lexer;
using core.lisp;
using core.lisp.interpreter;
using core.lisp.model;
using core.lisp.parser;
using NUnit.Framework;
using sly.parser;
using sly.parser.generator;

namespace core.lisp.tests
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