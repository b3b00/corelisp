using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using core.lisp.lexer;
using sly.lexer;

namespace lispparser.core.lisp.model
{
    public class Lambda : LispLiteral
    {
        public override ListValueType Type => ListValueType.Lambda;

        private SExpr Body { get; set; }

        private List<LispLiteral> Parameters { get; set; }
      

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