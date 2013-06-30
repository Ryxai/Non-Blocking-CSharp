using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp.AtomicDouble
{
    [Serializable]
    public class AtomicDouble
    {
        private double item;

        public AtomicDouble(double i = 0)
        {
            item = i;
        }

        /// <summary>
        /// Sets new value, returns old value
        /// </summary>
        /// <param name="newValue">Value to be set</param>
        /// <returns>Old value</returns>
        public double GetAndSet(double newValue) 
        {
            double temp = 0;
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
        public bool CompareAndSet(double expectedValue, double updatedValue) 
        {
            return (Interlocked.CompareExchange(ref item, updatedValue, expectedValue) == expectedValue);
        }

        public static implicit operator double(AtomicDouble i)
        {
            return i.item;
        }

        public static AtomicDouble operator --(AtomicDouble i)
        {
            Interlocked.Decrement(ref i.item);
            return i;
        }

        public static AtomicDouble operator ++(AtomicDouble i)
        {
            Interlocked.Increment(ref i.item);
            return i;
        }

        public static double operator +(AtomicDouble d1, AtomicDouble d2)
        {
            double result = 0;
            result = d1.item;
            Interlocked.Add(ref result, d2.item);
            return result;
        }

        public static double operator +(AtomicDouble d1, double d2)
        {
            double result = 0;
            Interlocked.Exchange(ref result, d2.item);
            Interlocked.Add(ref result, d1.item);
            return result;
        }

        public static double operator -(AtomicDouble d1, AtomicDouble d2)
        {
            double result = 0;
            Interlocked.Exchange(ref result, d1.item);
            Interlocked.Add(ref result, -(d2.item));
            return result;
        }

        public static double operator -(AtomicDouble d1, double d2)
        {
            double result = 0;
            Interlocked.Exchange(ref result, d1.item);
            Interlocked.Add(ref result, -d2);
            return result;
        }
    }
}
