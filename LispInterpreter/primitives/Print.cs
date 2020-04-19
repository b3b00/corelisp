using System;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;

namespace LispInterpreter.primitives
{
    public class Print
    {
        public static LispLiteral PRINT(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("PRINT", 1, evaluatedArgs.Count);
            }
            Console.WriteLine("PRINT :: "+evaluatedArgs[0].ToString());
            
            return NilLiteral.Instance;
        }
    }
}