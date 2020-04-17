using System.Collections.Generic;
using System.Text;

namespace lispparser.core.lisp.model
{
    public class LispProgram : ILisp
    {
        public List<LispLiteral> Statements { get; set; }
        public LispProgram(List<LispLiteral> statements)
        {
            Statements = statements;
        }

        public string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("(");
            foreach (var statement in Statements)
            {
                builder.AppendLine("\t"+statement.ToString());
            }
            builder.AppendLine(")");
            return builder.ToString();
        } 
        

       
    }
}