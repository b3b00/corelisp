using System.Collections.Generic;
using lispparser.core.lisp.model;

namespace LispInterpreter
{

    public class LispRuntimeFunction : LispLiteral
    {
        public LispFunction Function { get; set; }

        public LispRuntimeFunction(LispFunction function)
        {
            Function = function;
        }

        public LispLiteral Apply(Context context, params LispLiteral[] args)
        {
            return Function(context, args);
        }

    }
    
    public class Context
    {
        public Context Parent { get; set; }

        public Dictionary<string, LispLiteral> Scope { get; set; }


        public Context()
        {
            Scope = new Dictionary<string, LispLiteral>();
            foreach (var primitive in PrimitiveLibrary.Primitives)
            {
                Scope[primitive.Key] = primitive.Value;
            }
        }
        public Context(Dictionary<string, LispLiteral> scope, Context parent)
        {
            Scope = scope;
            Parent = parent;
        }

        public LispLiteral Get(string name)
        {
            if (Scope.ContainsKey(name))
            {
                return Scope[name];
            }
            else if (Parent != null)
            {
                return Parent.Get(name);
            }
            return NilLiteral.Instance;
        }

    }
}