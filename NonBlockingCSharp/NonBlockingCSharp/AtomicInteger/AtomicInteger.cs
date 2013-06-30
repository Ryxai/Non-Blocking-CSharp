using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp.AtomicInteger
{
    [Serializable]
    public class AtomicInteger
    {
        private int item;

        public AtomicInteger(int i)
        {
            Interlocked.Exchange(ref item, i);
        }

        public static implicit operator int(AtomicInteger i)
        {
            return i.item;
        }
        
        public static AtomicInteger operator --(AtomicInteger i)
        {
            Interlocked.Decrement(ref i.item);
            return i;
        }

        public static AtomicInteger operator ++(AtomicInteger i)
        {
            Interlocked.Increment(ref i.item);
            return i;
        }

        public static AtomicInteger operator +(AtomicInteger int1, AtomicInteger int2)
        {
            throw new NotImplementedException();
        }

        public static AtomicInteger operator +(AtomicInteger int1, int int2)
        {
            throw new NotImplementedException();
        }

        public static AtomicInteger operator -(AtomicInteger int1, AtomicInteger int2)
        {
            throw new NotImplementedException();
        }

        public static AtomicInteger operator -(AtomicInteger int1, int int2)
        {
            throw new NotImplementedException();
        }
    }
}
