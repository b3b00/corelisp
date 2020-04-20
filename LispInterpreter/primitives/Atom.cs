using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Atom
    {
        public static LispLiteral ATOM(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("ATOM",1,args.Length);
            }

            if (args[0].Type == LispValueType.Sexpr)
            {
                return NilLiteral.Instance;
            }
            return SymbolLiteral.True;

        }
    }
}