using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.LispInterpreter;

namespace core.lisp.interpreter.primitives
{
    public class Set
    {
        public static LispLiteral SETQ(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SETQ", 2, args.Length);
            }
            
            LispLiteral v1 = args[0];
            if (args[1].Type == LispValueType.Sexpr)
            {
                //Console.WriteLine($"will call function {args[1]}");
            }
            LispLiteral v2 = EvalArg(context, args[1]);
            if (v2 is LispRuntimeFunction r)
            {
                r.Name = v1.StringValue;
            }
           

            if (v1.Type != LispValueType.Symbol)
            {
                throw new LispPrimitiveBadArgType("SETQ",1,LispValueType.Symbol,args[0].Type);
            }

            if (v1 is SymbolLiteral symbol)
                context.Set(symbol.Value, v2);

            return NilLiteral.Instance;
        }
        
        public static LispLiteral SET(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SET", 2, args.Length);
            }
            var evaluatedArgs = EvalArgs(context, args.ToList());
            
            LispLiteral v1 = evaluatedArgs[0];
            LispLiteral v2 = evaluatedArgs[1];
            

            if (v1.Type != LispValueType.Symbol )
            {
                throw new LispPrimitiveBadArgType("SET",1,LispValueType.Symbol,args[0].Type);
            }

            
            if (v1 is SymbolLiteral symbol)
                context.Set(symbol.Value, v2);
            
            return NilLiteral.Instance;
        }
    }
}