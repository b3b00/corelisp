using System;
using System.IO;
using core.lisp.lexer;
using core.lisp.interpreter;
using core.lisp.model;
using core.lisp.parser;
using sly.parser;
using sly.parser.generator;

namespace core.lisp
{
    public class CoreLisp
    {
        private Parser<LispLexer, ILisp> Parser = null;

        public CoreLisp()
        {
            BuildParser();
            Context = new Context();
        }
        
        
        
        private void BuildParser()
        {
            ParserBuilder<LispLexer, ILisp> builder = new ParserBuilder<LispLexer, ILisp>();
            var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root",SymbolLexerExtension.SymbolExtension);
            if (ParserResult.IsOk)
            {
                Parser = ParserResult.Result;
            }
        }

        public  Context Context { get; private set; }

        public void Load(string filename)
        {
            string source = File.ReadAllText(filename);
            Run(source);
        }
        
        public LispLiteral Run(string source)
        {
            var r = Parser.Parse(source);
            if (r.IsError)
            {
                r.Errors.ForEach(e => Console.WriteLine(e.ErrorMessage));
                return null;
            }
            var x = interpreter.LispInterpreter.Interprete(Context, r.Result as LispProgram);
            return x;
        }
        
    }
}