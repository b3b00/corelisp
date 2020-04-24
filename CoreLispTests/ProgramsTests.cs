using System.Collections.Generic;
using System.Linq;
using core.lisp.model;
using NUnit.Framework;
using System.IO;

namespace core.lisp.tests
{
    public class ProgramsTests : CoreLispBaseTest
    {
        
        [Test]
        public void SetQCarTest()
        {
            var r = Test(@"
(setq variable (""a"" ""b"" ""c"" ))
(car variable)
");
            Assert.IsInstanceOf<StringLiteral>(r);
            Assert.AreEqual("a", (r as StringLiteral).Value);
        }

        [Test]
        public void SetCdrTest()
        {
            var r = Test(@"
(set 'variable (""a"" ""b"" ""c"" ))
(cdr variable)
");
            Assert.IsInstanceOf<SExpr>(r);
            SExpr lst = r as SExpr;
            Assert.AreEqual(2,lst.Elements.Count);
            Assert.AreEqual(new List<string>() {"b","c"}, lst.Elements.Select(x => (x as StringLiteral).Value).ToList());
        }
        
        
        
        [Test]
        public void TestNamedListLambda()
        {
            var r = Test( @"
(setq variable (""b"" ""c"" ))
(setq first (lambda (x) (car x)))
(set 'constructed (cons ""a"" variable))
(first constructed)
");
            Assert.IsInstanceOf<StringLiteral>(r);
            Assert.AreEqual("a", (r as StringLiteral).Value);
            
        }
        
        
        
        [Test]
        public void TestIf()
        {
            var r = Test( @"
(setq test 't)
(cond (test ""vrai"" ) ('t ""faux""))");
            Assert.IsInstanceOf<StringLiteral>(r);
            Assert.AreEqual("vrai", (r as StringLiteral).Value);
            
        }
        
 
        [Test]
        public void TestRecursionFactorial()
        {
            var r = Test(        @"
( setq facto (lambda (x) 
    (cond 
        ((eq 0 x) 1)
        ('t ( * x (facto  ( - x  1))) ) 
    )
    )
)
(setq factoun (facto 1))
(print factoun)
( setq factocinq ( facto 5 ) )
factocinq
");
            Assert.IsInstanceOf<IntLiteral>(r);
            Assert.AreEqual(120, (r as IntLiteral).Value);
            
        }
        
        [Test]
        public void TestDefun()
        {
            var r = Test(        @"
( defun plusThirty (x) (+ 30 x) )
( plusThirty 12)
");
            Assert.IsInstanceOf<IntLiteral>(r);
            Assert.AreEqual(42, (r as IntLiteral).Value);
            
        }
        
        [Test]
        public void TestAssoc()
        {
string assocLisp = File.ReadAllText("assoc.lisp");
            var r = Test(assocLisp);
            Assert.IsInstanceOf<SExpr>(r);
            SExpr result = r as SExpr;
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3,result.Head.IntValue);
            Assert.AreEqual(4,result.Tail.First().IntValue);
            Assert.AreEqual(5,result.Tail.Last().IntValue);
            
        }
        
        [Test]
        public void TestLispInLisp()
        {
            string assocLisp = File.ReadAllText("lisp.lisp");
            var r = Test(assocLisp);
            Assert.IsInstanceOf<SExpr>(r);
            SExpr result = r as SExpr;
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(13,result.Head.IntValue);
            Assert.AreEqual(14,result.Tail.First().IntValue);
        }
        
        [Test]
        public void TestMap()
        {
            string assocLisp = File.ReadAllText("map.lisp");
            var r = Test(assocLisp);
            Assert.IsInstanceOf<SExpr>(r);
            SExpr result = r as SExpr;
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2,result.Head.IntValue);
            Assert.AreEqual(3,result.Tail.First().IntValue);
            Assert.AreEqual(4,result.Tail.Last().IntValue);
        }
        
        

    }
}