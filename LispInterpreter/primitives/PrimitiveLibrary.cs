using System;
using System.Collections.Generic;
using lispparser.core.lisp.model;

namespace LispInterpreter.primitives
{
    public delegate LispLiteral LispFunction(Context context, params LispLiteral[] args); 
    
    public class PrimitiveLibrary
    {
        public static LispLiteral Car(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("CAR",1,args.Length);
            }

            if (args[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CAR",1,LispValueType.Sexpr,args[0].Type);
            }

            return (args[0] as SExpr).Head;

        }
        
        public static LispLiteral Cdr(Context context, params LispLiteral[] args)
        {
            if (args.Length != 1)
            {
                throw new LispPrimitiveBadArgNumber("CDR",1,args.Length);
            }

            if (args[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CDR",1,LispValueType.Sexpr,args[0].Type);
            }

            return new SExpr((args[0] as SExpr).Tail);

        }
        
        public static LispLiteral Cons(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("CONS",2,args.Length);
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
                throw new LispPrimitiveBadArgNumber("PRINT", 1, args.Length);
            }
            Console.WriteLine("PRINT :: "+args[0].ToString());
            
            return NilLiteral.Instance;
        }
        
        public static LispLiteral Set(Context context, params LispLiteral[] args)
        {
            LispLiteral v1 = args[0];
            LispLiteral v2 = args[1];
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SET", 2, args.Length);
            }

            if (v1.Type != LispValueType.Atom)
            {
                throw new LispPrimitiveBadArgType("SET",1,LispValueType.Atom,args[0].Type);
            }

            context.Set((v1 as AtomLiteral).Value, v2);
            
            return NilLiteral.Instance;
        }

        public static LispLiteral Add(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("ADD", 1, args.Length);
            }

            var v1 = args[0] as LispLiteral;
            var v2 = args[1] as LispLiteral;
            var t1 = v1.Type;
            var t2 = v2.Type;
            if (!v1.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("ADD",1,LispValueType.Numeric,t1);
            }
            
            if (!v2.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("ADD",1,LispValueType.Numeric,t2);
            }

            var resultType = t1 == LispValueType.Double || t2 == LispValueType.Double
                ? LispValueType.Double
                : LispValueType.Int;

            LispLiteral result = null;
            if (resultType == LispValueType.Double)
            {
                result = new DoubleLiteral(v1.DoubleValue + v2.DoubleValue);
            }
            else if (resultType == LispValueType.Int)
            {    
                    result = new IntLiteral(v1.IntValue + v2.IntValue);
            }
            else
            {
                result = NilLiteral.Instance;
            }
            
            return result;
        }
        
        public static LispLiteral Substract(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SUB", 1, args.Length);
            }

            var v1 = args[0] as LispLiteral;
            var v2 = args[1] as LispLiteral;
            var t1 = v1.Type;
            var t2 = v2.Type;
            if (!v1.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("SUB",1,LispValueType.Numeric,t1);
            }
            
            if (!v2.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("SUB",1,LispValueType.Numeric,t2);
            }

            var resultType = t1 == LispValueType.Double || t2 == LispValueType.Double
                ? LispValueType.Double
                : LispValueType.Int;

            LispLiteral result = null;
            if (resultType == LispValueType.Double)
            {
                result = new DoubleLiteral(v1.DoubleValue - v2.DoubleValue);
            }
            else if (resultType == LispValueType.Int)
            {    
                result = new IntLiteral(v1.IntValue - v2.IntValue);
            }
            else
            {
                result = NilLiteral.Instance;
            }
            
            return result;
        }
        
        public static LispLiteral Multiply(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("TIMES", 1, args.Length);
            }

            var v1 = args[0] as LispLiteral;
            var v2 = args[1] as LispLiteral;
            var t1 = v1.Type;
            var t2 = v2.Type;
            if (!v1.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("TIMES",1,LispValueType.Numeric,t1);
            }
            
            if (!v2.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("TIMES",1,LispValueType.Numeric,t2);
            }

            var resultType = t1 == LispValueType.Double || t2 == LispValueType.Double
                ? LispValueType.Double
                : LispValueType.Int;

            LispLiteral result = null;
            if (resultType == LispValueType.Double)
            {
                result = new DoubleLiteral(v1.DoubleValue * v2.DoubleValue);
            }
            else if (resultType == LispValueType.Int)
            {    
                result = new IntLiteral(v1.IntValue * v2.IntValue);
            }
            else
            {
                result = NilLiteral.Instance;
            }
            
            return result;
        }

        
        public static LispLiteral Divide(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("DIV", 1, args.Length);
            }

            var v1 = args[0] as LispLiteral;
            var v2 = args[1] as LispLiteral;
            var t1 = v1.Type;
            var t2 = v2.Type;
            if (!v1.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("DIV",1,LispValueType.Numeric,t1);
            }
            
            if (!v2.IsNumericType)
            {
                throw new LispPrimitiveBadArgType("DIV",1,LispValueType.Numeric,t2);
            }

            var resultType = t1 == LispValueType.Double || t2 == LispValueType.Double
                ? LispValueType.Double
                : LispValueType.Int;

            LispLiteral result = null;
            if (resultType == LispValueType.Double)
            {
                result = new DoubleLiteral(v1.DoubleValue / v2.DoubleValue);
            }
            else if (resultType == LispValueType.Int)
            {    
                result = new IntLiteral(v1.IntValue / v2.IntValue);
            }
            else
            {
                result = NilLiteral.Instance;
            }
            
            return result;
        }
        

        public static Dictionary<string, LispRuntimeFunction> Primitives = new Dictionary<string, LispRuntimeFunction>()
        {
            {"car", new LispRuntimeFunction(PrimitiveLibrary.Car)},
            {"cdr",  new LispRuntimeFunction(PrimitiveLibrary.Cdr)},
            {"cons",  new LispRuntimeFunction(PrimitiveLibrary.Cons)},
            {"print",  new LispRuntimeFunction(PrimitiveLibrary.Print)},
            {"set",  new LispRuntimeFunction(PrimitiveLibrary.Set)},
            {"+",new LispRuntimeFunction(PrimitiveLibrary.Add)},
            {"-",new LispRuntimeFunction(PrimitiveLibrary.Substract)},
            {"*",new LispRuntimeFunction(PrimitiveLibrary.Multiply)},
            {"/",new LispRuntimeFunction(PrimitiveLibrary.Divide)}
        };

    }
}