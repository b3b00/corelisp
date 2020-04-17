using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class SExpr : LispLiteral
    {
        public override LispValueType Type => LispValueType.Sexpr;

        public List<LispLiteral> Elements { get; set; }

        public LispLiteral Head => Elements.First();

        public List<LispLiteral> Tail => Elements.Skip(1).ToList();

      

        public SExpr(List<LispLiteral> elements)
        {
            Elements = elements;
        }
        
        public override string ToString()
        {
            return $"sexpr>[ {string.Join(" " ,Elements.Select(x => x.ToString()))} ]<";
        }
    }
}