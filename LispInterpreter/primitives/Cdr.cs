using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Cdr
    {
        public static LispLiteral CDR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("CDR",1,evaluatedArgs.Count);
            }

            if (evaluatedArgs[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CDR",1,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }

            return new SExpr((evaluatedArgs[0] as SExpr).Tail);

        }
    }
}