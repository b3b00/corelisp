using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;

namespace LispInterpreter.primitives
{
    public class Eq
    {
        public static LispLiteral EQ(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs =  EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("EQ", 2, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0];
            var v2 = evaluatedArgs[1];
            if (v1.Type != LispValueType.Sexpr && v2.Type != LispValueType.Sexpr)
            {
                if (v1.Type == v2.Type)
                {
                    if (v1.ToString() == v2.ToString())
                    {
                        return SymbolLiteral.True;
                    }
                }
            }
            return NilLiteral.Instance;
        }
    }
}