using System.Collections.Generic;
using System.Text;

namespace core.lisp.model
{
    public class LispProgram : ILisp
    {
        public List<LispLiteral> Statements { get; set; }
        
        public bool IsCompiled { get; set; }
        public LispProgram(List<LispLiteral> statements)
        {
            Statements = statements;
        }

        public LispProgram()
        {
            Statements = new List<LispLiteral>();
        }

        public void Add(LispLiteral statement)
        {
            Statements.Add(statement);
        }

        public override string ToString()
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