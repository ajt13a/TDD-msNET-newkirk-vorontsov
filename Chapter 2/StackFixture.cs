using System;
using NUnit.Framework;

[TestFixture]
public class StackFixture
{
    private Stack stack; 

    [SetUp]
    public void Init()
    {
        stack = new Stack();
    }

    [Test]
    public void Empty()
    {
        Assert.IsTrue(stack.IsEmpty);
    }

    [Test]
    public void PushOne()
    {
        stack.Push("first element");
        Assert.IsFalse(stack.IsEmpty, "After Push, IsEmpty should be false");
    }

    [Test]
    public void Pop()
    {
        stack.Push("first element");
        stack.Pop();
        Assert.IsTrue(stack.IsEmpty,
            "After Push - Pop, IsEmpty should be true");
    }

    [Test]
    public void PushPopContentCheck()
    {
        int expected = 1234; 
        stack.Push(expected);
        int actual = (int)stack.Pop();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PushPopMultipleElements()
    {
        string pushed1 = "1";
        stack.Push(pushed1);
        string pushed2 = "2";
        stack.Push(pushed2);
        string pushed3 = "3";
        stack.Push(pushed3);

        string popped = (string)stack.Pop();
        Assert.AreEqual(pushed3, popped);
        popped = (string)stack.Pop();
        Assert.AreEqual(pushed2, popped);
        popped = (string)stack.Pop();
        Assert.AreEqual(pushed1, popped);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void PopEmptyStack()
    {
        stack.Pop();
    }

    [Test]
    public void PushTop()
    {
        stack.Push("42");
        stack.Top();
        Assert.IsFalse(stack.IsEmpty);
    }

    [Test]
    public void PushTopContentCheckOneElement()
    {
        string pushed = "42";
        stack.Push(pushed);
        string topped = (string)stack.Top();
        Assert.AreEqual(pushed, topped);
    }

    [Test]
    public void PushTopContentCheckMultiples()
    {
        string pushed3 = "3";
        stack.Push(pushed3);
        string pushed4 = "4";
        stack.Push(pushed4);
        string pushed5 = "5";
        stack.Push(pushed5);
      
        string topped = (string)stack.Top();
        Assert.AreEqual(pushed5, topped);
    }

    [Test]
    public void PushTopNoStackStateChange()
    {
        string pushed = "44";
        stack.Push(pushed);

        for(int index = 0; index < 10; index++)
        {
            string topped = (string)stack.Top();
            Assert.AreEqual(pushed, topped);
        }
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TopEmptyStack()
    {
        stack.Top();
    }

    [Test]
    public void PushNull()
    {
        stack.Push(null);
        Assert.IsFalse(stack.IsEmpty);
    }

    [Test]
    public void PushNullCheckPop()
    {
        stack.Push(null);
        Assert.IsNull(stack.Pop());
        Assert.IsTrue(stack.IsEmpty);
    }

    [Test]
    public void PushNullCheckTop()
    {
        stack.Push(null);
        Assert.IsNull(stack.Top());
        Assert.IsFalse(stack.IsEmpty);
    }
}
