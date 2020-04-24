using System;
using static core.lisp.interpreter.LispInterpreter;
using System.Linq;
using core.lisp.model;

namespace core.lisp.interpreter.primitives
{
    public class Quote
    {
        public static LispLiteral QUOTE(Context context, params LispLiteral[] args)
        {
            
            if (LispInterpreter.DebugAll)
            {
                depth++;
                Console.WriteLine(
                    $"{LispInterpreter.getTab()}call quote ({string.Join(" , ", args.Select(x => DebugArg(x, context).ToString()))})");
            }

            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("QUOTE",1,args.Length);
            }

            if (LispInterpreter.DebugAll)
            {
                Console.WriteLine($"{LispInterpreter.getTab()}quote :>{args[0]}");
                depth--;
            }

            return args[0];
        }
    }
}