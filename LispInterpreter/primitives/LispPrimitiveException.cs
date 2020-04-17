using System;

namespace LispInterpreter.primitives
{
    public class LispPrimitiveException : Exception
    {
        public LispPrimitiveException(string message) : base(message)
        {

        }
    }
}
