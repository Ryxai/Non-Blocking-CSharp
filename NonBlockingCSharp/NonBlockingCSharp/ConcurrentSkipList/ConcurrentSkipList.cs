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
        private volatile HeadIndex<K, V> head;

        private bool CompareAndSwapHead(HeadIndex<K, V> expectedHead, HeadIndex<K, V> newHead) 
        { 
            return (Interlocked.CompareExchange(ref head, newHead, expectedHead) == expectedHead);
        }

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
            public readonly Index<K, V> down;
            public volatile Index<K, V> right;

            public Index(Node<K, V> node, Index<K, V> down, Index<K, V> right)
            {
                this.node = node;
                this.key = node.key;
                this.down = down;
                this.right = right;
            }

            public bool CompareAndSwapRight(Index<K, V> expectedValue, Index<K, V> newValue) 
            {
                return (Interlocked.CompareExchange(ref right, newValue, expectedValue) == expectedValue);
            }

            /// <summary>
            /// Returns true if the node this indexes has been deleted.
            /// </summary>
            /// <returns></returns>
            public bool IndexesDeletedNode() 
            {
                return node.value == null;
            }

            public bool Link(Index<K, V> succ, Index<K, V> newSucc) 
            {
                Node<K, V> n = node;
                newSucc.right = succ;
                return n.value != null && CompareAndSwapRight(succ, newSucc);
            }

            public bool Unlink(Index<K, V> succ) 
            {
                return !IndexesDeletedNode() && CompareAndSwapRight(succ, succ.right);
            }
        }

        private sealed class HeadIndex<K, V> : Index<K, V>
        {
            public readonly int level;
            public HeadIndex(Node<K, V> node, Index<K, V> down, Index<K, V> right, int level) 
                : base(node, down, right)            
            {
                this.level = level;
            }
        }


    }
}
