using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Car
    {
        public static LispLiteral CAR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("CAR",1,args.Length);
            }

            if (evaluatedArgs[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CAR",1,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }

            return (evaluatedArgs[0] as SExpr).Head;

        }
    }
}