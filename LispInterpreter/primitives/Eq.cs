using System;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;
using static LispInterpreter.primitives.PrimitiveLibrary;

namespace LispInterpreter.primitives
{
    public class Eq
    {
        public static LispLiteral EQ(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs =  EvalArgs(context, args.ToList());
            if (LispInterpreter.DebugAll)
            {
                depth++;
                Console.WriteLine(
                    $"{getTab()}call eq ({string.Join(" , ", evaluatedArgs.Select(x => DebugArg(x, context).ToString()))})");
            }

            AssertArgNumber("eq",args,2);

            var v1 = evaluatedArgs[0];
            var v2 = evaluatedArgs[1];
            
            if (v1.Type != LispValueType.Sexpr && v2.Type != LispValueType.Sexpr)
            {
                if (v1.Type == v2.Type)
                {
                    if (v1.ToString() == v2.ToString())
                    {
                        if (LispInterpreter.DebugAll)
                        {
                            Console.WriteLine($"{getTab()}eq :>True<");
                            depth--;
                        }
                        return SymbolLiteral.True;
                    }
                }
            }

            if (LispInterpreter.DebugAll)
            {
                Console.WriteLine($"{getTab()}eq :>False<");
                depth--;
            }

            return NilLiteral.Instance;
        }
    }
}