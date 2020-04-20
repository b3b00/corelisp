using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
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