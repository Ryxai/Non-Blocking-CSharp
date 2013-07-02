using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp.ConcurrentSkipList
{
    [Serializable]
    public class ConcurrentSkipList<K, V>
        where K : class, IComparable<K>
        where V : class
    {


        private class Node<K, V>
            where K : class
            where V : class
        {
            public readonly K key;
            public volatile V value;
            public volatile Node<K, V> next;

            public Node(Node<K, V> next)
                : this (default(K), default(V), next)
            { }

            public Node(K key, V value, Node<K, V> next) 
            {
                this.key = key;
                this.value = value;
                this.next = next;
            }

            public bool CompareAndSwapNext(Node<K, V> newValue, Node<K, V> updatedValue)
            {
                return (Interlocked.CompareExchange(ref next, updatedValue, newValue) == newValue);
            }

            public bool CompareAndSwapValue(V expectedValue, V newValue)
            {
                return (Interlocked.CompareExchange(ref value, newValue, expectedValue) == expectedValue);
            }
        }

        private class Index<K, V>
            where K : class
            where V : class
        {
            public readonly K key;
            public readonly Node<K, V> node;
            public readonly Node<K, V> down;
            public volatile Index<K, V> right;

            sealed bool CompareAndSwapRight(Index<K, V> expectedValue, Index<K, V> newValue) 
            {
                return (Interlocked.CompareExchange(ref right, newValue, expectedValue) == expectedValue);
            }

            /// <summary>
            /// Returns true if the node this indexes has been deleted.
            /// </summary>
            /// <returns></returns>
            sealed bool IndexesDeletedNode() 
            {
                return node.value == null;
            }

            sealed bool Link(Index<K, V> succ, Index<K, V> newSucc) 
            {
                Node<K, V> n = node;
                newSucc.right = succ;
                return n.value != null && CompareAndSwapRight(succ, newSucc);
            }

            sealed bool Unlink(Index<K, V> succ) 
            {
                return !IndexesDeletedNode() && CompareAndSwapRight(succ, succ.right);
            }
        }
    }
}
