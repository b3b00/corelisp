using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class Cdr
    {
        public static LispLiteral CDR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            AssertArgNumber("cdr",args,1);
            AssertArgType("cdr",evaluatedArgs.ToArray(),0,LispValueType.Sexpr);

            return new SExpr((evaluatedArgs[0] as SExpr).Tail);

        }
    }
}