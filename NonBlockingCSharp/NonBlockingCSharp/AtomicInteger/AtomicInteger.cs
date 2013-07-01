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

        public AtomicInteger(int i = 0)
        {
            item = i;
        }

        /// <summary>
        /// Sets new value, returns old value
        /// </summary>
        /// <param name="newValue">Value to be set</param>
        /// <returns>Old value</returns>
        public int GetAndSet(int newValue) 
        {
            int temp = item;
            item = newValue;
            return temp;
        }

        /// <summary>
        /// Compare expected value with actual value, if equal set to the updated value
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns>Whether update of new value is successful</returns>
        public bool CompareAndSet(int expectedValue, int updatedValue) 
        {
            return (Interlocked.CompareExchange(ref item, updatedValue, expectedValue) == expectedValue);
        }

        public static implicit operator int(AtomicInteger i)
        {
            return i.item;
        }
        
        public static implicit operator AtomicInteger(int i)
        {
            return new AtomicInteger(i);
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

        public static int operator +(AtomicInteger int1, AtomicInteger int2)
        {
            int result = 0;
            result = int1.item;
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
            result = int1.item;
            Interlocked.Add(ref result, -(int2.item));
            return result;
        }

        public static int operator -(AtomicInteger int1, int int2)
        {
            int result = 0;
            result = int1.item;
            Interlocked.Add(ref result, -int2);
            return result;
        }
    }
}
