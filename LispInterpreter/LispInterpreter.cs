using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LispInterpreter.primitives;
using lispparser.core.lisp.model;
using sly.parser.generator;

namespace LispInterpreter
{
    public class LispInterpreter
    {

        public static bool Debug = false;
        public static string getTab()
        {
            string tab = "";
            for (int i = 0; i < depth; i++)
            {
                tab += "   ";
            }

            return tab;
        }

        public static LispLiteral DebugArg(LispLiteral lit, Context context)
        {
            var val = context.Get(lit.StringValue);
            if ((lit.Type == LispValueType.Identifier || lit.Type == LispValueType.Atom) && val != null)
            {
                return val;
            }

            return lit;
        }

        public static LispLiteral DebugAndCall(string prefix, LispRuntimeFunction runtimeFunction, SExpr sexpr,
            Context context)
        {


            var args = sexpr.Tail;
            if (Debug && runtimeFunction.IsLambda)
            {
                depth++;
                DebugCall(prefix, runtimeFunction, sexpr, context);
            }

            var result = runtimeFunction.Apply(context, args.ToArray());
            if (Debug && runtimeFunction.IsLambda)
            {
                DebugResultCall(prefix, runtimeFunction, result);
                depth--;
            }

            return result;
        }

        public static void DebugCall(string prefix, LispRuntimeFunction runtimeFunction, SExpr args, Context context)
        {
            if (runtimeFunction.IsLambda)
            {
                Console.WriteLine(
                    $"{getTab()}[1] call function {runtimeFunction.Name} ({string.Join(" , ", args.Tail.Select(x => DebugArg(x,context).ToString()))})");
                DebugScope(context.Scope);
            }
        }

        public static void DebugResultCall(string prefix, LispRuntimeFunction function, LispLiteral result)
        {
            Console.WriteLine($"{getTab()}{prefix} function {function.Name} returns {result}");
        }

        public static int depth = 0;

        public static void DebugScope(Dictionary<string, LispLiteral> scope)
        { Console.Write($"{getTab()}\t{{");
            foreach (var v in scope)
            {
               
                if (v.Value?.StringValue != null && !PrimitiveLibrary.Primitives.ContainsKey(v.Value.StringValue))
                {
                    Console.Write(v.Key+"="+v.Value);
                }
                
            }
            Console.WriteLine("}");
        }
        

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
                var result = LispInterpreter.Interprete(scopedContext, lambda.Body);
                return result;
            };
            var lfunction = new LispRuntimeFunction(function);
            lfunction.IsLambda = true;
            return lfunction;
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
                    return DebugAndCall("[1]", runtimeFunction, sexpr, context);
                }
            }

            if (sexpr.Head is LispOperator oper)
            {
                if (context.Get(oper.Value) is LispRuntimeFunction runtimeFunction)
                {
                   return DebugAndCall("[2]", runtimeFunction, sexpr, context);
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
                return DebugAndCall("[3]",function,sexpr,context);
            }
            return sexpr;
        }
    }
}