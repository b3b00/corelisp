using core.lisp.lexer;
using LispInterpreter;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using NUnit.Framework;
using sly.parser;
using sly.parser.generator;

namespace CoreLispTests
{
    public class CoreLispBaseTest
    {
        public Parser<LispLexer, ILisp> Parser = null;
        
        [SetUp]
        public void Setup()
        {
            ParserBuilder<LispLexer, ILisp> builder = new ParserBuilder<LispLexer, ILisp>();
            var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root");
            if (ParserResult.IsOk)
            {
                Parser = ParserResult.Result;
            }
        }


        public LispLiteral Test(string source)
        {
            var r = Parser.Parse(source);
            Assert.True(r.IsOk);
            Assert.NotNull(r.Result);
            var x = LispInterpreter.LispInterpreter.Interprete(new Context(), r.Result as LispProgram);
            return x;
        }
        
        
    }
}