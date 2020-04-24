using System;

namespace core.lisp.interpreter.primitives
{
    public class LispPrimitiveException : Exception
    {
        public LispPrimitiveException(string message) : base(message)
        {

        }
    }
}
