using static LispInterpreter.LispInterpreter;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class If
    {
        public static LispLiteral IF(Context context, params LispLiteral[] args)
        {
            if (args.Length != 3)
            {
                throw new LispPrimitiveBadArgNumber("IF",3,args.Length);
            }

            var condition = EvalArg(context, args[0]);
            if (condition.BooleanValue)
            {
                //Console.WriteLine("\tif return then : "+evaluatedArgs[1]);
                return EvalArg(context,args[1]);
            }
            //Console.WriteLine("\tif return else : "+evaluatedArgs[2]);
            return EvalArg(context,args[2]);
        }
    }
}