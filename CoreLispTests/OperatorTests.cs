using lispparser.core.lisp.model;
using NUnit.Framework;

namespace CoreLispTests
{
    public class OperatorTests : CoreLispBaseTest
    {
        
        [Test]
        public void DoubleAddTest()
        {
            var r = Test("( + 1.0 2.0 )");
            Assert.IsInstanceOf<DoubleLiteral>(r);
            Assert.AreEqual(3.0, (r as DoubleLiteral).Value);
        }

        [Test]
        public void DoubleLambdaTest()
        {
            var r = Test("(( lambda (x) (+ x 11.0 )) 22.0)");
            Assert.IsInstanceOf<DoubleLiteral>(r);
            Assert.AreEqual(33.0, (r as DoubleLiteral).Value);
        }
        
        
            
            
        [Test]
        public void IntLambdaTest()
        {
            var r = Test(@"
{* this is Lisp comment *}
( ( lambda (x) (* x 11 )) 2)");
            Assert.IsInstanceOf<IntLiteral>(r);
            Assert.AreEqual(22, (r as IntLiteral).Value);
        }
    }
}