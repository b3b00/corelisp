using System;
using static LispInterpreter.LispInterpreter;
using System.Collections.Generic;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;


namespace LispInterpreter.primitives
{
    public class LambdaPrimitive : Primitive
    {
        public static LispLiteral LAMBDA(Context context, params LispLiteral[] args)
        {
            AssertArgNumber("lambda",args,2);
            

            List<LispLiteral> cons = new List<LispLiteral>();


            AssertArgType("lambda",args,0,LispValueType.Sexpr);
            AssertArgType("lambda",args,1,LispValueType.Sexpr);

            // TODO : check if arg1 is only symbols
            var parameters = args[0] as SExpr;
            foreach (var parameter in parameters.Elements)
            {
                if (parameter.Type != LispValueType.Symbol)
                {
                    throw new Exception($"lambda : parameters can only be symbols");
                } 
            }
           
            var lambda = new Lambda((args[0] as SExpr).Elements, args[1] as SExpr);
            return GetLambda(context, lambda,"anon");
        }
    }
}