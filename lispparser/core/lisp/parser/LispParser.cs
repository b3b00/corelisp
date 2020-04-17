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
        
        [Production("literal : ATOM")]
        public ILisp atomLiteral(Token<LispLexer> token)
        {
            var atom = new AtomLiteral(token);
            if (atom.Value == NilLiteral.Nil)
            {
                return NilLiteral.Instance;
            }
            return atom;
        }
        
        [Production("literal : IDENTIFIER")]
        public ILisp identifierLiteral(Token<LispLexer> token)
        {
            if (token.Value == NilLiteral.Nil)
            {
                return NilLiteral.Instance;
            }
            return new IdentifierLiteral(token); 
        }

        [Production("literal : sexpr")]
        public ILisp sexprLiteral(ILisp sexpr)
        {
            return sexpr;
        }

        [Production("literal : lambda")]
        public ILisp lambdaLiteral(Lambda lambda)
        {
            return lambda;
        }

        [Production("root: literal")]
        public ILisp root(ILisp literal)
        {
            return literal;
        }
        
        [Production("lambda:LPAREN[d] LAMBDA[d] LPAREN[d] IDENTIFIER* RPAREN[d] sexpr RPAREN[d]")]
        public ILisp lambda(List<Token<LispLexer>> parameters, SExpr sexpr)
        {
            return new Lambda(parameters.Select(x => new IdentifierLiteral(x)).ToList<LispLiteral>(), sexpr);
        }

        [Production("sexpr : LPAREN[d] [literal|operator]* RPAREN[d]")]
        public ILisp sexpr(List<LispLiteral> expr)
        {
            if (!expr.Any())
            {
                return NilLiteral.Instance;
            }
            return new SExpr(expr);
        }
        
        
    }
}