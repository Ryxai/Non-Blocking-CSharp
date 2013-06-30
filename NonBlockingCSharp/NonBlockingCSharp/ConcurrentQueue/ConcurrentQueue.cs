using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonBlockingCSharp.ConcurrentQueue
{
    [Serializable]
    public class ConcurrentQueue<T>
    {
        private Node<T> head = new Node<T>(default(T), null);
        private Node<T> tail;

        public ConcurrentQueue()
        {
            tail = head;
        }

        [Serializable]
        private class Node<T>
        {
            private readonly T item;
            private readonly Node<T> next;

            public Node(T item, Node<T> next)
            {
                this.item = item;
                this.next = next;
            }
        }
    }
}
