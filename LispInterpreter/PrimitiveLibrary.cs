using System;
using System.Collections.Generic;
using lispparser.core.lisp.model;

namespace LispInterpreter
{
    public class LispPrimitiveException : Exception
    {
        public LispPrimitiveException(string message) : base(message)
        {
            
        }

        public class LispPrimitiveBadArgNumber : LispPrimitiveException
        {
            public LispPrimitiveBadArgNumber(string primitiveName, int expected, int actual) : base(
                $"Lisp {primitiveName} : bad number of arguments. expected {expected}, found {actual}")
            {
                
            }
        }
        
        public class LispPrimitiveBadArgType : LispPrimitiveException
        {
            public LispPrimitiveBadArgType(string primitiveName,int position, LispValueType expected, LispValueType actual) : base(
                $"Lisp {primitiveName} : bad argument #{position} type. expected {expected}, found {actual}")
            {
                
            }
        }
    }
    
    public delegate LispLiteral LispFunction(Context context, params LispLiteral[] args); 
    
    public class PrimitiveLibrary
    {
        public static LispLiteral Car(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("CAR",1,args.Length);
            }

            if (args[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CAR",1,LispValueType.Sexpr,args[0].Type);
            }

            return (args[0] as SExpr).Head;

        }
        
        public static LispLiteral Cdr(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("CDR",1,args.Length);
            }

            if (args[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Sexpr,args[0].Type);
            }

            return new SExpr((args[0] as SExpr).Tail);

        }
        
        public static LispLiteral Cons(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("CONS",2,args.Length);
            }

            List<LispLiteral> cons = new List<LispLiteral>();
            cons.Add(args[0]);
            if (args[1].Type == LispValueType.Sexpr)
            {
                cons.AddRange((args[1] as SExpr).Elements);
            }
            else
            {
                cons.Add(args[1]);
            }

            return new SExpr(cons);
        }

        public static LispLiteral Print(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }
            Console.WriteLine(args[0].ToString());
            
            return NilLiteral.Instance;
        }

        public static LispLiteral Add(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }

            if (args[0].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[0].Type);
            }
            
            if (args[1].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[1].Type);
            }

            var d1 = args[0] as DoubleLiteral;
            var d2 = args[1] as DoubleLiteral;
            
            return new DoubleLiteral(d1.Value+d2.Value);
        }
        
        public static LispLiteral Substract(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }

            if (args[0].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[0].Type);
            }
            
            if (args[1].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[1].Type);
            }

            var d1 = args[0] as DoubleLiteral;
            var d2 = args[1] as DoubleLiteral;
            
            return new DoubleLiteral(d1.Value-d2.Value);
        }
        
        public static LispLiteral Multiply(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }

            if (args[0].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[0].Type);
            }
            
            if (args[1].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[1].Type);
            }

            var d1 = args[0] as DoubleLiteral;
            var d2 = args[1] as DoubleLiteral;
            
            return new DoubleLiteral(d1.Value*d2.Value);
        }

        
        public static LispLiteral Divide(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }

            if (args[0].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[0].Type);
            }
            
            if (args[1].Type != LispValueType.Double)
            {
                throw new LispPrimitiveException.LispPrimitiveBadArgType("CDR",1,LispValueType.Double,args[1].Type);
            }

            var d1 = args[0] as DoubleLiteral;
            var d2 = args[1] as DoubleLiteral;
            
            return new DoubleLiteral(d1.Value/d2.Value);
        }
        

        public static Dictionary<string, LispRuntimeFunction> Primitives = new Dictionary<string, LispRuntimeFunction>()
        {
            {"car", new LispRuntimeFunction(PrimitiveLibrary.Car)},
            {"cdr",  new LispRuntimeFunction(PrimitiveLibrary.Cdr)},
            {"cons",  new LispRuntimeFunction(PrimitiveLibrary.Cons)},
            {"print",  new LispRuntimeFunction(PrimitiveLibrary.Print)},
            {"+",new LispRuntimeFunction(PrimitiveLibrary.Add)},
            {"-",new LispRuntimeFunction(PrimitiveLibrary.Substract)},
            {"*",new LispRuntimeFunction(PrimitiveLibrary.Multiply)},
            {"/",new LispRuntimeFunction(PrimitiveLibrary.Divide)}
        };

    }
}