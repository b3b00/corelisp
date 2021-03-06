﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using core.lisp.lexer;
using sly.lexer;

namespace core.lisp.model
{
    public class SExpr : LispLiteral
    {
        
        public override double DoubleValue => 0.0;
        public override int IntValue => 0;
        public override string StringValue => null;
        public override bool BooleanValue => Elements.Count > 0;
        public override LispValueType Type => LispValueType.Sexpr;

        public List<LispLiteral> Elements { get; set; }

        public int Count => Elements.Count;

        public bool Any() => Elements.Any();

        public bool Single() => Elements.Count == 1 ;

        public bool MoreThanOne() => Elements.Count > 1;
        
        

        public LispLiteral Head => Elements.First();

        public List<LispLiteral> Tail => Elements.Skip(1).ToList();


        public SExpr(LispLiteral head, params LispLiteral[] tail)
        {
            Elements = new List<LispLiteral>();
            Elements.Add(head);
            Elements.AddRange(tail);
        }
        public SExpr(List<LispLiteral> elements)
        {
            Elements = elements;
        }

        public SExpr()
        {
            Elements = new List<LispLiteral>();
        }

        public void Add(LispLiteral literal)
        {
            Elements.Add(literal);
        }
        
        public override string ToString()
        {
            return $"(\n\t{string.Join("\n\t" ,Elements.Select(x => x.ToString()))}\n)";
        }
    }
}