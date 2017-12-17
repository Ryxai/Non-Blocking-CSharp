using System;
using System.Threading;

namespace NonBlockingCSharp
{
    class AtomicIntegerArray
    {
        private int[] items;

        public int this[int key]
        {
            get => items[key];
            set => Interlocked.Exchange(ref items[key], value);
        }

        public int Length => items.Length;

        public int GetAndSet(int key, int newValue)
        {
            int res = items[key];
            Interlocked.Exchange(ref items[key], newValue);
            return res;
        }

        public bool CompareAndSet(int key, int expectedValue, int newValue)
        {
            return newValue == Interlocked.CompareExchange(ref items[key], expectedValue, newValue);
        }

        public int GetAndAdd(int key, int delta)
        {
            var res = items[key];
            Interlocked.Add(ref items[key], delta);
            return res;
        }

        public int AddAndGet(int key, int delta)
        {
            Interlocked.Add(ref items[key], delta);
            return items[key];
        }

        public int GetAndIncrement(int key)
        {
            int res = items[key];
            Interlocked.Increment(ref items[key]);
            return res;
        }

        public int GetAndDecrement(int key)
        {
            int res = items[key];
            Interlocked.Decrement(ref items[key]);
            return res;
        }

        public int DecrementAndGet(int key)
        {
            return Interlocked.Decrement(ref items[key]);
        }

        public int IncrementAndGet(int key)
        {
            return Interlocked.Increment(ref items[key]);
        }

        public AtomicIntegerArray(int length)
        {
            items = new int[length];
        }

        public AtomicIntegerArray(int[] array)
        {
            if (array == null)
                throw new NullReferenceException();
            int length = array.Length;
            items = new int[Length];
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                    Interlocked.Exchange(ref items[i], array[i]);
            }
        }
    }
}
