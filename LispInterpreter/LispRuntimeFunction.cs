using core.lisp.interpreter.primitives;
using core.lisp.model;

namespace core.lisp.interpreter
{
    public class LispRuntimeFunction : LispLiteral
    {
        public string Name { get; set; } = "anonymous";
        public override LispValueType Type { get; set; } = LispValueType.Function;
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => null;
        public override bool BooleanValue => false;
        
        public LispFunction Function { get; set; }

        public bool IsLambda { get; set; }

        public LispRuntimeFunction(LispFunction function, string name = "anonymous")
        {
            Name = name;
            Function = function;
        }

        public LispLiteral Apply(Context context, params LispLiteral[] args)
        {
            return Function(context, args);
        }

    }
}