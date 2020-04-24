using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public class Primitive
    {
        public static void AssertArgType(string primitiveName, LispLiteral[] args, int position, LispValueType expectedType)
        {

            LispLiteral arg = args[position];
            
            if (arg.Type != expectedType)
            {
                throw new LispPrimitiveBadArgType(primitiveName,position,expectedType,arg.Type);
            }
            
        }

        public static void AssertArgNumber(string primitiveName, LispLiteral[] args, int expectedNumber)
        {
            if (args.Length != expectedNumber)
            {
                throw new LispPrimitiveBadArgNumber(primitiveName,expectedNumber,args.Length);
            }
        }
    }
}