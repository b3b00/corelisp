using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class Car
    {
        public static LispLiteral CAR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            AssertArgNumber("car",evaluatedArgs.ToArray(),1);
            AssertArgType("atom",evaluatedArgs.ToArray(),0,LispValueType.Sexpr);

            return (evaluatedArgs[0] as SExpr).Head;

        }
    }
}