using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.LispInterpreter;

namespace core.lisp.interpreter.primitives
{
    public class Cond  : Primitive
    {
        public static LispLiteral COND(Context context, params LispLiteral[] args)
        {
            if (args.Length < 1)
            {
                throw new LispPrimitiveBadArgNumber("COND",1,args.Length);
            }

            LispLiteral result = null;
            int i = 0;
            while (i < args.Length && result == null)
            {
                if (args[i] is SExpr cond)
                {
                    var condition = EvalArg(context, cond.Head);
                    if (condition.BooleanValue)
                    {
                        result = EvalArg(context, cond.Tail.First());
                    }
                }

                i++;
            }

            if (result == null)
                result = NilLiteral.Instance;
            return result;
        }
    }
}