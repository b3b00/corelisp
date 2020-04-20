using System.Net.Mime;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;

namespace LispInterpreter
{
    public class LambdaCompiler
    {

        public static LispProgram CompileLambdas(Context context, LispProgram program)
        {
            LispProgram compiled = new LispProgram();
            foreach (var statement in program.Statements)
            {
                var compiledStatement = CompileLambdas(context, statement);
                compiled.Add(compiledStatement);
            }

            compiled.IsCompiled = true;
            return compiled;
        }
        public static LispLiteral CompileLambdas(Context context,LispLiteral literal)
        {
            switch (literal)
            {
                case Lambda lambda :
                {
                    return GetLambda(context, lambda);
                }
                case SExpr sExpr:
                {
                    return CompileLambdasInSExpr(context,sExpr);
                }
                default :
                {
                    return literal;
                }
            }
        }

        public static SExpr CompileLambdasInSExpr(Context context, SExpr sExpr)
        {
            SExpr compiled = new SExpr();
            foreach (var literal in sExpr.Elements)
            {
                LispLiteral compiledItem = CompileLambdas(context, literal);
                compiled.Add(compiledItem);
            }
            
            return compiled;
        }
        
    }
}