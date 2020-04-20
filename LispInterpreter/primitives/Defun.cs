using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Defun
    {
        public static LispLiteral DEFUN(Context context, params LispLiteral[] args)
        {
            if (args.Length != 3)
            {
                throw new LispPrimitiveBadArgNumber("DEFUN",3,args.Length);
            }

            if (args[0].Type != LispValueType.Symbol)
            {
                throw new LispPrimitiveBadArgType("DEFUN",0,LispValueType.Symbol,args[0].Type);
            }
            if (args[1].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("DEFUN",1,LispValueType.Sexpr,args[1].Type);
            }
            if (args[2].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("DEFUN",2,LispValueType.Sexpr,args[2].Type);
            }

            var lambda = new Lambda((args[1] as SExpr).Elements, args[2] as SExpr);
            var runtimeFunction = LispInterpreter.GetLambda(context,lambda);
            context.Set((args[0] as SymbolLiteral).Value, runtimeFunction);
            return lambda;
        }
    }
}