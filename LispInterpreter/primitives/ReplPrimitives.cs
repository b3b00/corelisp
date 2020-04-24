using System;
using System.IO;
using core.lisp.lexer;
using core.lisp.model;
using core.lisp.parser;
using sly.parser;
using sly.parser.generator;

namespace core.lisp.interpreter.primitives
{
    public class ReplPrimitives : Primitive
    {
        
        
        private static Parser<LispLexer, ILisp> Parser = null;

        
        private static void BuildParser()
        {
            if (Parser == null)
            {
                ParserBuilder<LispLexer, ILisp> builder = new ParserBuilder<LispLexer, ILisp>();
                var ParserResult = builder.BuildParser(new LispParser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, "root",
                    SymbolLexerExtension.SymbolExtension);
                if (ParserResult.IsOk)
                {
                    Parser = ParserResult.Result;
                }
            }
        }

        public static LispLiteral CONTEXT(Context context, params LispLiteral[] args)
        {
            SExpr c = new SExpr();
            foreach (var ctx in context.Scope)
            {
                c.Add(new SymbolLiteral(ctx.Key));
            }

            return c;
        }

        public static LispLiteral LOAD(Context context, params LispLiteral[] args)
        {
            foreach (var file in args)
            {
                string filename = (file as StringLiteral).StringValue;
                if (File.Exists(filename))
                {
                    string source = File.ReadAllText(filename);
                    BuildParser();
                    var r = Parser.Parse(source);
                    if (r.IsError)
                    {
                        r.Errors.ForEach(e => Console.WriteLine(e.ErrorMessage));
                        return null;
                    }
                    var x = LispInterpreter.Interprete(context, r.Result as LispProgram);
                    return x;
                }
                else
                {
                    Console.WriteLine("file not found");
                }
            }
            return NilLiteral.Instance;
        } 
        
    }
}