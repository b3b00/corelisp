namespace lispparser.core.lisp.model
{
    public abstract class LispLiteral : ILisp
    {
        public virtual LispValueType Type { get; set; }
    }
}