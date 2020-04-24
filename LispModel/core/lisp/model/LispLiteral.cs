namespace core.lisp.model
{
    public abstract class LispLiteral : ILisp
    {
        public virtual LispValueType Type { get; set; }

        public bool IsNumericType => Type == LispValueType.Double || Type == LispValueType.Int;
        
        public abstract double DoubleValue { get; }
        
        public abstract int IntValue { get; }
        
        public abstract string StringValue { get; }
        
        public abstract bool BooleanValue { get; }
        
        
    }
}