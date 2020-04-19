using static LispInterpreter.LispInterpreter;
using System.Collections.Generic;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Cons
    {
        public static LispLiteral CONS(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("CONS",2,evaluatedArgs.Count);
            }

            List<LispLiteral> cons = new List<LispLiteral>();
            cons.Add(evaluatedArgs[0]);
            LispLiteral tail = evaluatedArgs[1];
            if (evaluatedArgs[1].Type == LispValueType.Sexpr )
            {
                cons.AddRange((evaluatedArgs[1] as SExpr).Elements);
            }
            else
            {
                if (evaluatedArgs[1] != NilLiteral.Instance)
                {
                    cons.Add(evaluatedArgs[1]);
                }
            }

            return new SExpr(cons);
        }
    }
}