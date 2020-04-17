using System;
using System.Collections.Generic;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter
{
    public class LispInterpreter
    {

        public static LispRuntimeFunction GetLambda(Context lambdaContext, Lambda lambda)
        {
            LispFunction function = (Context context, LispLiteral[] args) =>
            {
                if (lambda.Parameters.Count > args.Length)
                {
                    throw new Exception("ouch : not enough arguments for lambda " + lambda);
                }

                var scope = new Dictionary<string, LispLiteral>();
                int i = 0;
                foreach (IdentifierLiteral parameter in lambda.Parameters)
                {
                    scope[parameter.Value] = args[i];
                    i++;
                }

                var scopedContext = new Context(scope, context);
                return LispInterpreter.Interprete(scopedContext, lambda.Body);
            };

            return new LispRuntimeFunction(function);
        }

        public static LispLiteral Interprete(Context context, LispLiteral literal)
        {
            if (literal is SExpr sexpr)
            {
                return InterpreteSExpr(context, sexpr);
            }

            if (literal is IdentifierLiteral id)
            {
                var eval = context.Get(id.Value);
                if (eval is LispRuntimeFunction)
                {
                    eval = NilLiteral.Instance;
                }
                return eval;
            }

            if (literal is LispRuntimeFunction f)
            {
                return NilLiteral.Instance;
            }

            return literal;
        }

        public static LispLiteral InterpreteSExpr(Context context, SExpr sexpr)
        {
            if (sexpr.Head is IdentifierLiteral id)
            {
                if (context.Get(id.Value) is LispRuntimeFunction runtimeFunction)
                {
                    var args = sexpr.Tail.Select(x => LispInterpreter.Interprete(context, x)).ToList();
                    return runtimeFunction.Apply(context, args.ToArray());
                }
            }

            if (sexpr.Head is LispOperator oper)
            {
                if (context.Get(oper.Value) is LispRuntimeFunction runtimeFunction)
                {
                    var args = sexpr.Tail.Select(x => LispInterpreter.Interprete(context, x)).ToList();
                    return runtimeFunction.Apply(context, args.ToArray());
                }
            }

            if (sexpr.Head is Lambda lambda)
            {
                var l = GetLambda(context, lambda);
                var args = sexpr.Tail.Select(x => LispInterpreter.Interprete(context, x)).ToList();
                return l.Apply(context, args.ToArray());
            }
            return sexpr;
        }
    }
}