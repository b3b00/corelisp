using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LispInterpreter.primitives;
using lispparser.core.lisp.model;

namespace LispInterpreter
{
    public class LispInterpreter
    {

        #region debugging 
        public static bool DebugLambda = false;
        public static bool DebugAll = false;
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
            if (lit is SymbolLiteral symb && symb != null && context.Scope.ContainsKey(symb.Value))
            {
                return context.Get(symb.Value);
            }

            return lit;
        }

        public static LispLiteral DebugAndCall(LispRuntimeFunction runtimeFunction, SExpr sexpr,
            Context context)
        {


            var args = sexpr.Tail;
            if (DebugLambda && runtimeFunction.IsLambda)
            {
                depth++;
                DebugCall(runtimeFunction, sexpr, context);
            }

            var result = runtimeFunction.Apply(context, args.ToArray());
            if (DebugLambda && runtimeFunction.IsLambda)
            {
                DebugResultCall(runtimeFunction, result);
                depth--;
            }

            return result;
        }

        public static void DebugCall(LispRuntimeFunction runtimeFunction, SExpr args, Context context)
        {
            if (runtimeFunction.IsLambda && DebugAll)
            {
                Console.WriteLine(
                    $"{getTab()}call {runtimeFunction.Name} ({string.Join(" , ", args.Tail.Select(x => DebugArg(x,context).ToString()))})");
                DebugScope(context.Scope);
            }
        }

        public static void DebugResultCall(LispRuntimeFunction function, LispLiteral result)
        {
            if (DebugAll)
            {
                Console.WriteLine($"{getTab()}{function.Name} returns {result}");
            }
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
        
        #endregion
        
        #region arguments evaluation

        public static LispLiteral EvalArg(Context context, LispLiteral arg)
        {
            return LispInterpreter.Interprete(context, arg);
        }

        public static List<LispLiteral> EvalArgs(Context context, List<LispLiteral> args)
        {
            List<LispLiteral> evaluated = new List<LispLiteral>();
            foreach (var arg in args)
            {
                var eval = EvalArg(context, arg);
                evaluated.Add(eval);
            }

            return evaluated;
        }
        
        
        #endregion


        #region  interprete

        

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
            if (literal is SExpr sexpr)
            {
                return InterpreteSExpr(context, sexpr);
            }

            if (literal is SymbolLiteral id)
            {
                var eval = context.Get(id.Value);
                return eval;
            }

            if (literal is LispRuntimeFunction f)
            {
                return f;
            }

            return literal;
        }

        public static LispLiteral InterpreteSExpr(Context context, SExpr sExpr)
        {
            if (sExpr.Head is SymbolLiteral id)
            {
                if (context.Get(id.Value) is LispRuntimeFunction runtimeFunction)
                {
                    return DebugAndCall(runtimeFunction, sExpr, context);
                }
            }
            if (sExpr.Head is LispRuntimeFunction function)
            {
                var args = sExpr.Tail;
                return DebugAndCall(function,sExpr,context);
            }
            if (sExpr.Head is SExpr subExpr && sExpr.MoreThanOne())
            {
                var evaluatedSubExpr = InterpreteSExpr(context, subExpr);
                var t = new SExpr(evaluatedSubExpr, sExpr.Tail.ToArray());
                return InterpreteSExpr(context,t);
            }

            // if (sExpr.Head is SExpr single && sExpr.Single())
            // {
            //     return InterpreteSExpr(context, single);
            // }
            return sExpr;
            
        }
        
        public static LispRuntimeFunction GetLambda(Context lambdaContext, Lambda lambda,string name)
        {
            LispFunction function = (Context context, LispLiteral[] args) =>
            {
                var evaluatedArgs = EvalArgs(context, args.ToList());
                if (DebugLambda)
                {
                    depth++;
                    Console.WriteLine(
                        $"{getTab()}call {name} ({string.Join(" , ", evaluatedArgs.Select(x => x.ToString()))})");
                }
                if (lambda.Parameters.Count > evaluatedArgs.Count)
                {
                    throw new Exception("ouch : not enough arguments for lambda " + lambda);
                }

                var scope = new Dictionary<string, LispLiteral>();
                int i = 0;
                foreach (SymbolLiteral parameter in lambda.Parameters)
                {
                    scope[parameter.Value] = evaluatedArgs[i];
                    i++;
                }

                var scopedContext = new Context(scope, context);
                var result = LispInterpreter.Interprete(scopedContext, lambda.Body);
                if (DebugLambda)
                {
                    Console.WriteLine($"{getTab()}{name} :>{result.ToString()}<");
                    depth--;
                }

                return result;
            };
            var lfunction = new LispRuntimeFunction(function);
            lfunction.IsLambda = true;
            return lfunction;
        }

        
        #endregion
    }
}