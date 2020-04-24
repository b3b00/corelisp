using System;
using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.LispInterpreter;

namespace core.lisp.interpreter.primitives
{
    public class Print
    {
        public static LispLiteral PRINT(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count < 1)
            {
                throw new LispPrimitiveBadArgNumber("PRINT", 1, evaluatedArgs.Count);
            }
            Console.WriteLine("\nPRINT :: "+string.Join(" ",evaluatedArgs.Select(ev => ev.ToString()))+"\n");
            
            return evaluatedArgs.Last();
        }
    }
}