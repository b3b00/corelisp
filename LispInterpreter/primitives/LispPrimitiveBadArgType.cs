using core.lisp.model;

namespace core.lisp.interpreter.primitives
{
    public class LispPrimitiveBadArgType : LispPrimitiveException
    {
        public LispPrimitiveBadArgType(string primitiveName,int position, LispValueType expected, LispValueType actual) : base(
            $"Lisp {primitiveName} : bad argument #{position} type. expected {expected}, found {actual}")
        {
                
        }
    }
}