using System.Collections.Generic;
using core.lisp.interpreter.primitives;
using core.lisp.model;

namespace core.lisp.interpreter
{
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
            if (name == null)
            {
                return NilLiteral.Instance;
            }
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

        public void Set(string name, LispLiteral value)
        {
            Scope.Add(name,value);
        }
    }
}