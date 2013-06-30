using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NonBlockingCSharp.Utilities;

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


        public int GetAndSet(int ) 
        {
 
        } 

        public static int operator --(AtomicInteger i)
        {
            Interlocked.Decrement(ref i.item);
            return i.item;
        }

        public static int operator ++(AtomicInteger i)
        {
            Interlocked.Increment(ref i.item);
            return i.item;
        }

        public static int operator +(AtomicInteger int1, AtomicInteger int2)
        {
            int result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, int2.item);
            return result;
        }

        public static int operator +(AtomicInteger int1, int int2)
        {
            int result = int2;
            Interlocked.Add(ref result, int1.item);
            return result;
        }

        public static int operator -(AtomicInteger int1, AtomicInteger int2)
        {
            int result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, -(int2.item));
            return result;
        }

        public static int operator -(AtomicInteger int1, int int2)
        {
            int result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, -int2);
            return result;
        }
    }
}
