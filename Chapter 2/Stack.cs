using System;
using System.Collections;

public class Stack
{
    private ArrayList elements = new ArrayList();

    public bool IsEmpty
    {
        get
        {
            return (elements.Count == 0); 
        }
    }

    public void Push(object element)
    {
        elements.Insert(0, element);
    }

    public object Pop()
    {
        object top = Top();
        elements.RemoveAt(0);
        return top;
    }

    public object Top()
    {
        if(IsEmpty)
            throw new InvalidOperationException("Stack is Empty");

        return elements[0];
    }
}
