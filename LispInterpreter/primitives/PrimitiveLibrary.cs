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

        public static void AssertArgType(string primitiveName, LispLiteral[] args, int position, LispValueType expectedType)
        {

            LispLiteral arg = args[position];
            
            if (arg.Type != expectedType)
            {
                throw new LispPrimitiveBadArgType(primitiveName,position,expectedType,arg.Type);
            }
            
        }

        public static void AssertArgNumber(string primitiveName, LispLiteral[] args, int expectedNumber)
        {
            if (args.Length != expectedNumber)
            {
                throw new LispPrimitiveBadArgNumber(primitiveName,expectedNumber,args.Length);
            }
        }
        
        
        public static LispLiteral Map(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("MAP",2,evaluatedArgs.Count);
            }
            
            if (evaluatedArgs[1].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("MAP",2,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }
            if (evaluatedArgs[0].Type != LispValueType.Function)
            {
                throw new LispPrimitiveBadArgType("MAP",1,LispValueType.Function,evaluatedArgs[0].Type);
            }

            var list = evaluatedArgs[1] as SExpr;
            var function = evaluatedArgs[0] as LispRuntimeFunction;

            var applied=  list.Elements.Select(x =>
            {
                return Interprete(context, new SExpr(function, x));
            }).ToList();
            
           return new SExpr(applied);
        }
        
       

        


       
        
        
        
        
       
        
        

       

        public static Dictionary<string, LispRuntimeFunction> Primitives = new Dictionary<string, LispRuntimeFunction>()
        {
            {"car", new LispRuntimeFunction(Car.CAR,"car")},
            {"cdr",  new LispRuntimeFunction(Cdr.CDR,"cdr")},
            {"cons",  new LispRuntimeFunction(Cons.CONS,"cons")},
            {"print",  new LispRuntimeFunction(Print.PRINT,"print")},
            {"setq",  new LispRuntimeFunction(Set.SETQ,"setq")},
            {"set",  new LispRuntimeFunction(Set.SET,"set")},
            {"map",  new LispRuntimeFunction(PrimitiveLibrary.Map,"map")},
            {"+",new LispRuntimeFunction(Math.Add,"add")},
            {"-",new LispRuntimeFunction(Math.Substract,"sub")},
            {"*",new LispRuntimeFunction(Math.Multiply,"mul")},
            {"/",new LispRuntimeFunction(Math.Divide,"div")},
            {"if",new LispRuntimeFunction(If.IF,"if")},
            {"eq",new LispRuntimeFunction(Eq.EQ,"eq")},
            {"cond",new LispRuntimeFunction(Cond.COND,"cond")},
            {"quote",new LispRuntimeFunction(Quote.QUOTE,"quote")},
            {"atom",new LispRuntimeFunction(Atom.ATOM,"atom")},
            {"lambda",new LispRuntimeFunction(LambdaPrimitive.LAMBDA,"lambda")},
            {"defun",new LispRuntimeFunction(Defun.DEFUN,"defun")}
        };

    }
}