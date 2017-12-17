using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NonBlockingCSharp;

namespace NonBlockingCSharp
{
    public class AtomicReference<T> where T : class
    {
        private T item;

        public T reference
        {
            get => item;
            set => Interlocked.Exchange(ref item, value);
        }

        public AtomicReference(T initialRef)
        {
            Interlocked.Exchange(ref item, initialRef);
        }

        public Boolean CompareAndSet(T expectedReference, T newReference)
        {
            return AtomicOperationsUtilities.CompareAndSwap(ref item, expectedReference, newReference);
        }
    }
}
