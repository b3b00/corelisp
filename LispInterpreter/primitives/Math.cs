using System.Linq;
using core.lisp.model;
using static core.lisp.interpreter.LispInterpreter;

namespace core.lisp.interpreter.primitives
{
    public class Math
    {
         public static LispLiteral Add(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("ADD", 1, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0] as LispLiteral;
            var v2 = evaluatedArgs[1] as LispLiteral;
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
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("SUB", 1, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0] as LispLiteral;
            var v2 = evaluatedArgs[1] as LispLiteral;
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
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("TIMES", 1, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0] as LispLiteral;
            var v2 = evaluatedArgs[1] as LispLiteral;
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
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("DIV", 1, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0] as LispLiteral;
            var v2 = evaluatedArgs[1] as LispLiteral;
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
        
    }
}