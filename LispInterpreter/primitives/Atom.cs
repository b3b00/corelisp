using static LispInterpreter.LispInterpreter;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class Atom
    {
        public static LispLiteral ATOM(Context context, params LispLiteral[] args)
        {
            AssertArgNumber("atom",args,1);
            
            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("ATOM",1,args.Length);
            }

            var arg0 = EvalArg(context, args[0]);
            if (arg0.Type == LispValueType.Sexpr)
            {
                return NilLiteral.Instance;
            }
            return SymbolLiteral.True;

        }
    }
}