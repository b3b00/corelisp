namespace core.lisp.interpreter.primitives
{
    public class LispPrimitiveBadArgNumber : LispPrimitiveException
    {
        public LispPrimitiveBadArgNumber(string primitiveName, int expected, int actual) : base(
            $"Lisp {primitiveName} : bad number of arguments. expected {expected}, found {actual}")
        {
                
        }
    }
}