using System.Collections.Generic;
using System.Linq;
using core.lisp.model;
using NUnit.Framework;

namespace core.lisp.tests
{
    public class ListPrimitivesTests : CoreLispBaseTest
    {
        
        [Test]
        public void CarTest()
        {
            var r = Test(@"(car (""a"" ""b"" ""c"" ))");
            Assert.IsInstanceOf<StringLiteral>(r);
            Assert.AreEqual("a", (r as StringLiteral).Value);
        }

        [Test]
        public void CdrTest()
        {
            var r = Test(@"(cdr (""a"" ""b"" ""c"" ))");
            Assert.IsInstanceOf<SExpr>(r);
            SExpr lst = r as SExpr;
            Assert.AreEqual(2,lst.Elements.Count);
            Assert.AreEqual("b", (lst.Elements[0] as StringLiteral).Value);
            Assert.AreEqual("c", (lst.Elements[1] as StringLiteral).Value);
        }
        
        
        
        [Test]
        public void ConsTest()
        {
            var r = Test(@"(cons ""x"" (""a"" ""b"" ""c"" ))");
            Assert.IsInstanceOf<SExpr>(r);
            SExpr lst = r as SExpr;
            Assert.AreEqual(4,lst.Elements.Count);
            Assert.AreEqual(new List<string>() {"x","a","b","c"}, lst.Elements.Select(x => (x as StringLiteral).Value).ToList());
            
        }
    }
}