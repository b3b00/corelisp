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
            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("QUOTE",1,args.Length);
            }

            return args[0];
        }
    }
}