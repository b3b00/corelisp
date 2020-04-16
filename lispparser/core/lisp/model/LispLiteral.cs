namespace lispparser.core.lisp.model
{
    public abstract class LispLiteral : ILisp
    {
        public virtual ListValueType Type { get; set; }
    }
}