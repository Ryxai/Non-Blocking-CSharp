using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NonBlockingCSharp;

namespace NonBlockingCSharp
{
    class AtomicReferenceArray<T> where T : class
    {
        private T[] items;

        public T this[int key] {
            get => items[key];
            set => Interlocked.Exchange(ref items[key], value);
        }

        public int Length => items.Length;

        public bool CompareAndSet(int index, T expectedReference, T newReference)
        {
            return 
                AtomicOperationsUtilities.CompareAndSwap(ref items[index],
                                             expectedReference, newReference);
        }

        public AtomicReferenceArray(int length)
        {
            items = new T[length];
            if (length > 0)
                Interlocked.Exchange(ref items[0], null);
        }

        public AtomicReferenceArray(T[] array)
        {
            if (array == null)
                throw new NullReferenceException();
            int length = array.Length;
            items = new T[length];
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                    Interlocked.Exchange(ref items[i], array[i]);
            }
        }
    }
}
