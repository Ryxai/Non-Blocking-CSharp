using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonBlockingCSharp.ConcurrentStack
{
    [Serializable]
    public class ConcurrentStack<T>
    {
        private Node<T> head;

        public ConcurrentStack()
        {
            head = new Node<T>();
        }

        [Serializable]
        private class Node<T>
        {
            private Node<T> next;
            private T item;
        }
    }
}
