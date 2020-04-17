using System;
using System.Collections.Generic;
using System.Linq;
using LispInterpreter.primitives;
using lispparser.core.lisp.model;

namespace LispInterpreter
{
    public class LispInterpreter
    {

        public static LispLiteral EvalArg(Context context, LispLiteral arg)
        {
            return LispInterpreter.Interprete(context, arg);
        }

        public static List<LispLiteral> EvalArgs(Context context, List<LispLiteral> args)
        {
            return args.Select(x => EvalArg(context,x)).ToList();
        }
        
        public static LispRuntimeFunction GetLambda(Context lambdaContext, Lambda lambda)
        {
            LispFunction function = (Context context, LispLiteral[] args) =>
            {
                var evaluatedArgs = EvalArgs(context, args.ToList());
                if (lambda.Parameters.Count > evaluatedArgs.Count)
                {
                    throw new Exception("ouch : not enough arguments for lambda " + lambda);
                }

                var scope = new Dictionary<string, LispLiteral>();
                int i = 0;
                foreach (IdentifierLiteral parameter in lambda.Parameters)
                {
                    scope[parameter.Value] = evaluatedArgs[i];
                    i++;
                }

                var scopedContext = new Context(scope, context);
                return LispInterpreter.Interprete(scopedContext, lambda.Body);
            };

            return new LispRuntimeFunction(function);
        }


        public static LispLiteral Interprete(Context context, LispProgram program)
        {
            if (program.Statements.Any())
            {
                LispLiteral t = null; 
                foreach (var statement in program.Statements)
                {
                    t = Interprete(context, statement);
                }

                return t??NilLiteral.Instance;
            }

            return NilLiteral.Instance;
        }
        
        public static LispLiteral Interprete(Context context, LispLiteral literal)
        {
            if (literal is Lambda lambda)
            {
                return GetLambda(context, lambda);
            }
            
            if (literal is SExpr sexpr)
            {
                return InterpreteSExpr(context, sexpr);
            }

            if (literal is IdentifierLiteral id)
            {
                var eval = context.Get(id.Value);
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
                    var args = sexpr.Tail;
                    return runtimeFunction.Apply(context, args.ToArray());
                }
            }

            if (sexpr.Head is LispOperator oper)
            {
                if (context.Get(oper.Value) is LispRuntimeFunction runtimeFunction)
                {
                    var args = sexpr.Tail;
                    return runtimeFunction.Apply(context, args.ToArray());
                }
            }

            if (sexpr.Head is Lambda lambda)
            {
                var l = GetLambda(context, lambda);
                var args = sexpr.Tail;
                return l.Apply(context, args.ToArray());
            }
            if (sexpr.Head is LispRuntimeFunction function)
            {
                var args = sexpr.Tail;
                return function.Apply(context, args.ToArray());
            }
            return sexpr;
        }
    }
}