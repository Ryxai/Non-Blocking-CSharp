using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NonBlockingCSharp.Utilities;

namespace NonBlockingCSharp.ConcurrentStack
{
    [Serializable]
    public class ConcurrentStack<T>
    {
        private Node<T> head;

        public ConcurrentStack()
        {
            head = new Node<T>(default(T));
        }

        public bool TryPop(out T result)
        {
            Node<T> node;
            do
            {
                node = head.next;
                if (node == null)
                {
                    result = default(T);
                    return false;
                }
            } while (!AtomicOperationsUtilities.CompareAndSwap(ref head.next, node, node.next));
            result = node.item;
            return true;
        }

        public void Push(T item)
        {
            Node<T> node = new Node<T>(item);
            do
            {
                node.next = head.next;
            } while (!AtomicOperationsUtilities.CompareAndSwap(ref head.next, node.next, node));
        }

        [Serializable]
        private class Node<T>
        {
            public Node<T> next;
            public T item;

            public Node(T item) 
            {
                this.item = item;
            }
        }
    }
}
