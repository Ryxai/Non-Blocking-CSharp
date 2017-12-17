using System;
using System.Threading;

namespace NonBlockingCSharp
{
    [Serializable]
    public class AtomicCounter
    {
        private int item;

        public AtomicCounter(int initialValue = 0)
        {
            Interlocked.Exchange(ref item, initialValue);
        }

        public void Reset() 
        {
            Interlocked.Exchange(ref item, 0);
        }

        public static implicit operator int(AtomicCounter i)
        {
            return i.item;
        }

        public static AtomicCounter operator --(AtomicCounter i)
        {
            Interlocked.Decrement(ref i.item);
            return i;
        }

        public static AtomicCounter operator ++(AtomicCounter i)
        {
            Interlocked.Increment(ref i.item);
            return i;
        }
    }
}
