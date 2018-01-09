using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T> {
    readonly int size;
    private int headPointer = 0;
    private int tailPointer = 0;

    T[] items;

    public Queue(int queueSize)
    {
        size = queueSize;
        items = new T[size];
    }

    public void Enqueue(T item)
    {
        if(headPointer < size && headPointer + 1 != tailPointer)
        {
            headPointer++;
            items[headPointer - 1] = item;
        }
        else if(headPointer == size  && tailPointer > 1)
        {
            headPointer = 0;
            items[headPointer] = item;
        }
        else
        {
            Debug.LogError("Queue is Full");
        }
    }

    public T Dequeue()
    {
        if(tailPointer != headPointer && tailPointer != size)
        {
            tailPointer++;
            return items[tailPointer - 1];
        }
        else if (tailPointer == size)
        {
            tailPointer = 0;
            return items[tailPointer];
        }
        else
        {
            Debug.LogError("Queue is Empty");
            return items[tailPointer];
            
        }  
    }

    public T Peek()
    {
        return items[tailPointer];
    }
	
}
