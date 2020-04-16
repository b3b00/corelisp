using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class SExpr : LispLiteral
    {
        public override ListValueType Type => ListValueType.Sexpr;

        public List<ILisp> Elements { get; set; }

        public ILisp Head => Elements.First();

        public List<ILisp> Tail => Elements.Skip(1).ToList();

      

        public SExpr(List<ILisp> elements)
        {
            Elements = elements;
        }
        
        public override string ToString()
        {
            return $"sexpr>[ {string.Join(" " ,Elements.Select(x => x.ToString()))} ]<";
        }
    }
}