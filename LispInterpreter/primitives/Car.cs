using static core.lisp.interpreter.LispInterpreter;
using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.primitives.PrimitiveLibrary;

namespace core.lisp.interpreter.primitives
{
    public class Car  : Primitive
    {
        public static LispLiteral CAR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            AssertArgNumber("car",evaluatedArgs.ToArray(),1);

            if (evaluatedArgs.First().Type == LispValueType.Nil)
            {
                return NilLiteral.Instance;
            }


            AssertArgType("car",evaluatedArgs.ToArray(),0,LispValueType.Sexpr);

            if (evaluatedArgs.Any())
            {
                var expr = evaluatedArgs[0] as SExpr;
                if (expr.BooleanValue)
                    return (evaluatedArgs[0] as SExpr).Head;
                return NilLiteral.Instance;
                ;
            }
            return NilLiteral.Instance;

        }
    }
}