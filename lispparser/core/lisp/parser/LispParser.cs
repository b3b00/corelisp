using System.Collections.Generic;
using System.Linq;
using core.lisp.lexer;
using lispparser.core.lisp.model;
using sly.lexer;
using sly.parser.generator;

namespace lispparser.core.lisp.parser
{
    public class LispParser
    {

        [Production("operator : [PLUS|MINUS|TIMES|DIVIDE]") ]
        public LispLiteral lispOperator(Token<LispLexer> token)
        {
            return new LispOperator(token); 
        }
        
        [Production("literal : INT")]
        public ILisp intLiteral(Token<LispLexer> token)
        {
            return new IntLiteral(token); 
        }
        [Production("literal : DOUBLE")]
        public ILisp doubleLiteral(Token<LispLexer> token)
        {
            return new DoubleLiteral(token); 
        }
        [Production("literal : STRING")]
        public ILisp stringLiteral(Token<LispLexer> token)
        {
            return new StringLiteral(token); 
        }
       
        
        [Production("literal : SYMBOL")]
        public ILisp symbolLiteral(Token<LispLexer> token)
        {
            if (token.Value == NilLiteral.Nil)
            {
                return NilLiteral.Instance;
            }
            return new SymbolLiteral(token); 
        }

        [Production("literal : sexpr")]
        public ILisp sexprLiteral(ILisp sexpr)
        {
            return sexpr;
        }

        [Production("root: literal*")]
        public ILisp root(List<ILisp> statements)
        {
            return new LispProgram(statements.Cast<LispLiteral>().ToList());
        }
        
        [Production("sexpr : LPAREN[d] [literal|operator]* RPAREN[d]")]
        public ILisp sexpr(List<ILisp> expr)
        {
            if (!expr.Any())
            {
                return NilLiteral.Instance;
            }
            return new SExpr(expr.Cast<LispLiteral>().ToList());
        }

        [Production("literal : QUOTE[d] literal")]
        public ILisp Quote(ILisp literal)
        {
            return new SExpr(new SymbolLiteral("quote"), literal as LispLiteral);
        }
        
        
    }
}