﻿using System;
using System.Threading;

namespace NonBlockingCSharp
{
    [Serializable]
    public class AtomicLong
    {
        private long item;

        public AtomicLong(long i = 0)
        {
            Interlocked.Exchange(ref item, i);
        }

        /// <summary>
        /// Sets new value, returns old value
        /// </summary>
        /// <param name="newValue">Value to be set</param>
        /// <returns>Old value</returns>
        public long GetAndSet(long newValue) 
        {
            long temp = 0;
            Interlocked.Exchange(ref temp, item);
            Interlocked.Exchange(ref item, newValue);
            return temp;
        }

        /// <summary>
        /// Compare expected value with actual value, if equal set to the updated value
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns>Whether update of new value is successful</returns>
        public bool CompareAndSet(long expectedValue, long updatedValue) 
        {
            return (Interlocked.CompareExchange(ref item, updatedValue, expectedValue) == expectedValue);
        }

        public static implicit operator long(AtomicLong i)
        {
            return i.item;
        }

        public static implicit operator AtomicLong(long l)
        {
            return new AtomicLong(l);
        }

        public static AtomicLong operator --(AtomicLong i)
        {
            Interlocked.Decrement(ref i.item);
            return i;
        }

        public static AtomicLong operator ++(AtomicLong i)
        {
            Interlocked.Increment(ref i.item);
            return i;
        }

        public static long operator +(AtomicLong int1, AtomicLong int2)
        {
            long result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, int2.item);
            return result;
        }

        public static long operator +(AtomicLong int1, long int2)
        {
            long result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, int2);
            return result;
        }

        public static long operator -(AtomicLong int1, AtomicLong int2)
        {
            long result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, -(int2.item));
            return result;
        }

        public static long operator -(AtomicLong int1, long int2)
        {
            long result = 0;
            Interlocked.Exchange(ref result, int1.item);
            Interlocked.Add(ref result, -int2);
            return result;
        }
    }
}
