using System;
using System.Collections.Generic;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;

namespace LispInterpreter.primitives
{
    public delegate LispLiteral LispFunction(Context context, params LispLiteral[] args);

    public class PrimitiveLibrary
    {
        public static Dictionary<string, LispRuntimeFunction> Primitives = new Dictionary<string, LispRuntimeFunction>()
        {
            {"car", new LispRuntimeFunction(Car.CAR,"car")},
            {"cdr",  new LispRuntimeFunction(Cdr.CDR,"cdr")},
            {"cons",  new LispRuntimeFunction(Cons.CONS,"cons")},
            {"print",  new LispRuntimeFunction(Print.PRINT,"print")},
            {"setq",  new LispRuntimeFunction(Set.SETQ,"setq")},
            {"set",  new LispRuntimeFunction(Set.SET,"set")},
            {"+",new LispRuntimeFunction(Math.Add,"add")},
            {"-",new LispRuntimeFunction(Math.Substract,"sub")},
            {"*",new LispRuntimeFunction(Math.Multiply,"mul")},
            {"/",new LispRuntimeFunction(Math.Divide,"div")},
            {"eq",new LispRuntimeFunction(Eq.EQ,"eq")},
            {"cond",new LispRuntimeFunction(Cond.COND,"cond")},
            {"quote",new LispRuntimeFunction(Quote.QUOTE,"quote")},
            {"atom",new LispRuntimeFunction(Atom.ATOM,"atom")},
            {"lambda",new LispRuntimeFunction(LambdaPrimitive.LAMBDA,"lambda")},
            {"defun",new LispRuntimeFunction(Defun.DEFUN,"defun")},
            {"debug",new LispRuntimeFunction(Debug.DEBUG,"debug")}
        };

    }
}