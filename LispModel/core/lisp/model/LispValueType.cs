namespace lispparser.core.lisp.model
{
    public enum LispValueType
    {
        Int = 0,
        Double = 1,
        String = 2,
        Sexpr = 4,
        Lambda = 5,
        Symbol = 6,
        Nil = 7,
        Numeric = 8,

        
        Function = 9
    }
}