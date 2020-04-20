using System;
using core.lisp.lexer;
using LispInterpreter;
using lispparser.core.lisp.model;
using lispparser.core.lisp.parser;
using NUnit.Framework;
using sly.parser;
using sly.parser.generator;

namespace CoreLispTests
{
    public class AtomTests : CoreLispBaseTest

    {



    [Test]
    public void IntTest()
    {
        var r = Test("1");
        Assert.IsInstanceOf<IntLiteral>(r);
        Assert.AreEqual(1, (r as IntLiteral).Value);
    }

    [Test]
    public void DoubleTest()
    {
        var r = Test("1.28");
        Assert.IsInstanceOf<DoubleLiteral>(r);
        Assert.AreEqual(1.28, (r as DoubleLiteral).Value);
    }

    [Test]
    public void StringTest()
    {
        var r = Test("\"string\"");
        Assert.IsInstanceOf<StringLiteral>(r);
        Assert.AreEqual("string", (r as StringLiteral).Value);
    }

    [Test]
    public void QuotedSymbolTest()
    {
        var r = Test("'symbol");
        Assert.IsInstanceOf<SymbolLiteral>(r);
        Assert.AreEqual("symbol", (r as SymbolLiteral).Value);
    }

    [Test]
    public void EmptySExprTest()
    {
        var r = Test("()");
        Assert.IsInstanceOf<NilLiteral>(r);
        Assert.False((r as NilLiteral).BooleanValue);
    }

    [Test]
    public void NilTest()
    {
        var r = Test("nil");
        Assert.IsInstanceOf<NilLiteral>(r);
        Assert.False((r as NilLiteral).BooleanValue);
    }

    [Test]
    public void QuotedTrueTest()
    {
        var r = Test("'t");
        Assert.IsInstanceOf<SymbolLiteral>(r);
        Assert.True((r as SymbolLiteral).BooleanValue);
    }
    }
}