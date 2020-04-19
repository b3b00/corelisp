using System;
using System.Collections.Generic;
using System.Linq;
using lispparser.core.lisp.model;
using static LispInterpreter.LispInterpreter;

namespace LispInterpreter.primitives
{
    public delegate LispLiteral LispFunction(Context context, params LispLiteral[] args); 
    
    public class PrimitiveLibrary
    {

        
        
        
        
        public static LispLiteral Car(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("CAR",1,args.Length);
            }

            if (evaluatedArgs[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CAR",1,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }

            return (evaluatedArgs[0] as SExpr).Head;

        }
        
        public static LispLiteral Cdr(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("CDR",1,evaluatedArgs.Count);
            }

            if (evaluatedArgs[0].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("CDR",1,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }

            return new SExpr((evaluatedArgs[0] as SExpr).Tail);

        }
        
        public static LispLiteral Cons(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("CONS",2,evaluatedArgs.Count);
            }

            List<LispLiteral> cons = new List<LispLiteral>();
            cons.Add(evaluatedArgs[0]);
            LispLiteral tail = evaluatedArgs[1];
            if (evaluatedArgs[1].Type == LispValueType.Sexpr )
            {
                cons.AddRange((evaluatedArgs[1] as SExpr).Elements);
            }
            else
            {
                if (evaluatedArgs[1] != NilLiteral.Instance)
                {
                    cons.Add(evaluatedArgs[1]);
                }
            }

            return new SExpr(cons);
        }
        
        public static LispLiteral Map(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("MAP",2,evaluatedArgs.Count);
            }
            
            if (evaluatedArgs[1].Type != LispValueType.Sexpr)
            {
                throw new LispPrimitiveBadArgType("MAP",2,LispValueType.Sexpr,evaluatedArgs[0].Type);
            }
            if (evaluatedArgs[0].Type != LispValueType.Function)
            {
                throw new LispPrimitiveBadArgType("MAP",1,LispValueType.Function,evaluatedArgs[0].Type);
            }

            var list = evaluatedArgs[1] as SExpr;
            var function = evaluatedArgs[0] as LispRuntimeFunction;

            var applied=  list.Elements.Select(x =>
            {
                return Interprete(context, new SExpr(function, x));
            }).ToList();
            
           return new SExpr(applied);
        }
        
        public static LispLiteral If(Context context, params LispLiteral[] args)
        {
            if (args.Length != 3)
            {
                throw new LispPrimitiveBadArgNumber("IF",3,args.Length);
            }

            var condition = EvalArg(context, args[0]);
            if (condition.BooleanValue)
            {
                //Console.WriteLine("\tif return then : "+evaluatedArgs[1]);
                return EvalArg(context,args[1]);
            }
            //Console.WriteLine("\tif return else : "+evaluatedArgs[2]);
            return EvalArg(context,args[2]);
        }

        public static LispLiteral Cond(Context context, params LispLiteral[] args)
        {
            if (args.Length < 1)
            {
                throw new LispPrimitiveBadArgNumber("COND",1,args.Length);
            }

            LispLiteral result = null;
            int i = 0;
            while (i < args.Length && result == null)
            {
                if (args[i] is SExpr cond)
                {
                    var condition = EvalArg(context, cond.Head);
                    if (condition.BooleanValue)
                    {
                        result = EvalArg(context, cond.Tail.First());
                    }
                }

                i++;
            }

            if (result == null)
                result = NilLiteral.Instance;
            return result;
        }


        public static LispLiteral Eq(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs =  EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 2)
            {
                throw new LispPrimitiveBadArgNumber("EQ", 2, evaluatedArgs.Count);
            }

            var v1 = evaluatedArgs[0];
            var v2 = evaluatedArgs[1];
            if (v1.Type != LispValueType.Sexpr && v2.Type != LispValueType.Sexpr)
            {
                if (v1.Type == v2.Type)
                {
                    if (v1.ToString() == v2.ToString())
                    {
                        return new AtomLiteral("t");
                    }
                }
            }
            return NilLiteral.Instance;
        }
        
        
        public static LispLiteral Print(Context context, params LispLiteral[] args)
        {
            var evaluatedArgs = EvalArgs(context, args.ToList());
            if (evaluatedArgs.Count != 1)
            {
                throw new LispPrimitiveBadArgNumber("PRINT", 1, evaluatedArgs.Count);
            }
            Console.WriteLine("PRINT :: "+evaluatedArgs[0].ToString());
            
            return NilLiteral.Instance;
        }
        
        public static LispLiteral SetQ(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SET", 2, args.Length);
            }
            
            LispLiteral v1 = args[0];
            if (args[1].Type == LispValueType.Sexpr)
            {
                //Console.WriteLine($"will call function {args[1]}");
            }
            LispLiteral v2 = EvalArg(context, args[1]);
            if (v2 is LispRuntimeFunction r)
            {
                r.Name = v1.StringValue;
            }
           

            if (v1.Type != LispValueType.Atom && v1.Type != LispValueType.Identifier)
            {
                throw new LispPrimitiveBadArgType("SET",1,LispValueType.Atom,args[0].Type);
            }

            if (v1 is AtomLiteral atom)
                context.Set(atom.Value, v2);
            if (v1 is IdentifierLiteral id)
                context.Set(id.Value, v2);
            
            return NilLiteral.Instance;
        }
        
        public static LispLiteral Set(Context context, params LispLiteral[] args)
        {
            if (args.Length != 2)
            {
                throw new LispPrimitiveBadArgNumber("SET", 2, args.Length);
            }
            var evaluatedArgs = EvalArgs(context, args.ToList());
            
            LispLiteral v1 = evaluatedArgs[0];
            LispLiteral v2 = evaluatedArgs[1];
            

            if (v1.Type != LispValueType.Atom && v1.Type != LispValueType.Identifier)
            {
                throw new LispPrimitiveBadArgType("SET",1,LispValueType.Atom,args[0].Type);
            }

            if (v1 is AtomLiteral atom)
                context.Set(atom.Value, v2);
            if (v1 is IdentifierLiteral id)
                context.Set(id.Value, v2);
            
            return NilLiteral.Instance;
        }
        
        

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
        

        public static Dictionary<string, LispRuntimeFunction> Primitives = new Dictionary<string, LispRuntimeFunction>()
        {
            {"car", new LispRuntimeFunction(PrimitiveLibrary.Car,"car")},
            {"cdr",  new LispRuntimeFunction(PrimitiveLibrary.Cdr,"cdr")},
            {"cons",  new LispRuntimeFunction(PrimitiveLibrary.Cons,"cons")},
            {"print",  new LispRuntimeFunction(PrimitiveLibrary.Print,"print")},
            {"setq",  new LispRuntimeFunction(PrimitiveLibrary.SetQ,"setq")},
            {"set",  new LispRuntimeFunction(PrimitiveLibrary.Set,"set")},
            {"map",  new LispRuntimeFunction(PrimitiveLibrary.Map,"map")},
            {"+",new LispRuntimeFunction(PrimitiveLibrary.Add,"add")},
            {"-",new LispRuntimeFunction(PrimitiveLibrary.Substract,"sub")},
            {"*",new LispRuntimeFunction(PrimitiveLibrary.Multiply,"mul")},
            {"/",new LispRuntimeFunction(PrimitiveLibrary.Divide,"div")},
            {"if",new LispRuntimeFunction(PrimitiveLibrary.If,"if")},
            {"eq",new LispRuntimeFunction(PrimitiveLibrary.Eq,"eq")},
            {"cond",new LispRuntimeFunction(PrimitiveLibrary.Cond,"cond")}
        };

    }
}