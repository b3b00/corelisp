using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class Cdr  : Primitive
    {
        public static LispLiteral CDR(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            AssertArgNumber("cdr",args,1);
            if (evaluatedArgs[0].Type == LispValueType.Nil)
            {
                return NilLiteral.Instance;
            }

            AssertArgType("cdr",evaluatedArgs.ToArray(),0,LispValueType.Sexpr);

            var sExpr = evaluatedArgs[0] as SExpr;
            if (sExpr.Elements.Count <= 1)
            {
                return NilLiteral.Instance;
            }  
            
            return new SExpr(sExpr.Tail);

        }
    }
}