using System;
using System.Linq;
using System.Runtime.InteropServices;
using core.lisp.model;
using static core.lisp.interpreter.LispInterpreter;

namespace core.lisp.interpreter.primitives
{
    public class Debug  : Primitive
    {
        public static bool DebugIt = false;
        
        public static LispLiteral DEBUG(Context context, params LispLiteral[] args)
        {

            var debugArgs = args.Take(args.Length - 1).Select(x => DebugArg(x,context)).ToList();

            if (DebugIt)
                Console.WriteLine("debugging "+string.Join(" ",debugArgs.Select(x => x.ToString())));
            
            var result = EvalArg(context, args.Last()); 
            
            if (DebugIt)
                Console.WriteLine("  =>"+result.ToString());
            
            
            return result;
        }
    }
}