﻿using static core.lisp.interpreter.LispInterpreter;
using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.primitives.PrimitiveLibrary;

namespace core.lisp.interpreter.primitives
{
    public class Defun  : Primitive
    {
        public static LispLiteral DEFUN(Context context, params LispLiteral[] args)
        {
            AssertArgNumber("defun",args,3);
            AssertArgType("defun",args,0,LispValueType.Symbol);
            AssertArgType("defun",args,1,LispValueType.Sexpr);
            AssertArgType("defun",args,2,LispValueType.Sexpr);

            string functionName = (args[0] as SymbolLiteral).Value;
            var lambda = new Lambda((args[1] as SExpr).Elements, args[2] as SExpr);
            var runtimeFunction = LispInterpreter.GetLambda(context,lambda,functionName);
            runtimeFunction.Name = functionName;
            context.Set(functionName, runtimeFunction);
            return runtimeFunction;
        }
    }
}