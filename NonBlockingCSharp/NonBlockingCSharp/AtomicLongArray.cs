using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp
{
    class AtomicLongArray
    {

        private long[] items;

        public long this[int key]
        {
            get => items[key];
            set => Interlocked.Exchange(ref items[key], value);
        }

        public int Length => items.Length;

        public long GetAndSet(int key, long newValue)
        {
            var res = items[key];
            Interlocked.Exchange(ref items[key], newValue);
            return res;
        }

        public bool CompareAndSet(int key, long expectedValue, long newValue)
        {
            return newValue == Interlocked.CompareExchange(ref items[key], expectedValue, newValue);
        }

        public long GetAndAdd(int key, long delta)
        {
            var res = items[key];
            Interlocked.Add(ref items[key], delta);
            return res;
        }

        public long AddAndGet(int key, long delta)
        {
            return Interlocked.Add(ref items[key], delta);
        }

        public long GetAndIncrement(int key)
        {
            var res = items[key];
            Interlocked.Increment(ref items[key]);
            return res;
        }

        public long GetAndDecrement(int key)
        {
            var res = items[key];
            Interlocked.Decrement(ref items[key]);
            return res;
        }

        public long DecrementAndGet(int key)
        {
            return Interlocked.Decrement(ref items[key]);
        }

        public long IncrementAndGet(int key)
        {
            return Interlocked.Increment(ref items[key]);
        }

        public AtomicLongArray(int length)
        {
            items = new long[length];
        }

        public AtomicLongArray(long[] array)
        {
            if (array == null)
                throw new NullReferenceException();
            int length = array.Length;
            items = new long[Length];
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                    Interlocked.Exchange(ref items[i], array[i]);
            }
        }
    }
}