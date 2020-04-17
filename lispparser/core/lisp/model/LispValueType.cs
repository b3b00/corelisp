namespace lispparser.core.lisp.model
{
    public enum LispValueType
    {
        Int = 0,
        Double = 1,
        String = 2,
        Atom = 3,
        Sexpr = 4,
        Lambda = 5,
        Identifier = 6,
        Numeric = 8,

        Nil = 7
    }
}