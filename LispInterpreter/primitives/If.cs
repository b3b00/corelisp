using static LispInterpreter.LispInterpreter;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class If
    {
        public static LispLiteral IF(Context context, params LispLiteral[] args)
        {
            AssertArgNumber("if",args,3);
            

            var condition = EvalArg(context, args[0]);
            if (condition.BooleanValue)
            {
                return EvalArg(context,args[1]);
            }
            return EvalArg(context,args[2]);
        }
    }
}