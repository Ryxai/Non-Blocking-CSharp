using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NonBlockingCSharp.AtomicBool;
using System.Threading;

namespace NonBlockingCSharp.AtomicMarkableReference
{
    public class AtomicMarkableReference<T> 
        where T : class
    {
        private T item;
        private AtomicBool.AtomicBool b;

        public AtomicMarkableReference(T initialRef, bool initialMark = false)
        {
            Interlocked.Exchange(ref item, initialRef);
            Interlocked.Exchange(ref b, (AtomicBool.AtomicBool)initialMark);
        }
    }
}
