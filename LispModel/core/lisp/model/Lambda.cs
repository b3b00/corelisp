﻿﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class Lambda : LispLiteral
    {
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => null;
        public override bool BooleanValue => true;
        public override LispValueType Type => LispValueType.Lambda;

        public SExpr Body { get; set; }

        public List<LispLiteral> Parameters { get; set; }
      

        public Lambda(List<LispLiteral> parameters, SExpr body)
        {
            Parameters = parameters;
            Body = body;
        }
        
        public override string ToString()
        {
            var param = string.Join(" ", Parameters.Select(x => x.ToString()));
            var body = string.Join(" ", Body.Elements.Select(x => x.ToString()));
            return $"({param}) =>{Body}";
        }
    }
}